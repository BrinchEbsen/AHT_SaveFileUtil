﻿using AHT_SaveFileUtil.Save.Slot;
using System;

namespace AHT_SaveFileUtil.Save.MiniMap
{
    public enum InfoType
    {
        Background = 0,
        Mapped = 1,
        Reveal = 2
    }

    public class MiniMapInfo
    {
        public uint MapFile { get; set; }

        public uint Map { get; set; }

        public InfoType Type { get; set; }

        public uint TextureFile { get; set; }

        public uint Texture { get; set; }

        /// <summary>
        /// Values pertaining to the world-coordinates of a minimap.
        /// 
        /// <para>
        /// <b>Values for type "Mapped":</b>
        /// <list type="list">
        /// <item>[0] West edge (-X)</item>
        /// <item>[1] North edge (+Z)</item>
        /// <item>[2] East edge (+X)</item>
        /// <item>[3] South edge (-Z)</item>
        /// </list>
        /// </para>
        /// 
        /// <para>
        /// <b>Values for type "Background":</b>
        /// <list type="list">
        /// <item>[0] Unknown</item>
        /// <item>[1] Unknown</item>
        /// <item>[2] Unknown</item>
        /// <item>[3] Unknown</item>
        /// </list>
        /// </para>
        /// </summary>
        public float[] WorldEdge { get; set; } = new float[4];

        /// <summary>
        /// Values pertaining to the pixel-coordinates of a minimap.
        /// 
        /// <para>
        /// <b>Values for type "Mapped":</b>
        /// <list type="list">
        /// <item>[0] Blob sprite scale (always 32)</item>
        /// <item>[1] Unknown (always 0)</item>
        /// <item>[2] X-axis scale (units per pixel)</item>
        /// <item>[3] Z-axis scale (units per pixel)</item>
        /// </list>
        /// </para>
        /// 
        /// <para>
        /// <b>Values for type "Background":</b>
        /// <list type="list">
        /// <item>[0] Unknown</item>
        /// <item>[1] Unknown</item>
        /// <item>[2] Unknown</item>
        /// <item>[3] Unknown</item>
        /// </list>
        /// </para>
        /// </summary>
        public float[] PixelEdge { get; set; } = new float[4];

        /// <summary>
        /// The dimensions of the mapped area of the minimap, along the X [0] axis and the Z [1] axis.
        /// </summary>
        public int[] MappedGridSize
        {
            get
            {
                //Div by 0 guard
                if (PixelEdge[2] == 0 || PixelEdge[3] == 0)
                    return [0, 0];

                return
                [
                    (int)Math.Floor(Math.Abs((WorldEdge[2] - WorldEdge[0]) / PixelEdge[2])) + 1,
                    (int)Math.Floor(Math.Abs((WorldEdge[1] - WorldEdge[3]) / PixelEdge[3])) + 1,
                ];
            }
        }

        public int BitHeapSize
        {
            get
            {
                if (Type != InfoType.Mapped) return 0;

                int[] size = MappedGridSize;

                return size[0] * size[1];
            }
        }

        public MiniMapInfo() { }

        public int GetBitHeapOffset(float x, float z)
        {
            if (!WorldPosWithinWorldEdges(x, z))
                return -1;

            //Div by 0 guard
            if (PixelEdge[2] == 0 || PixelEdge[3] == 0)
                return -1;

            return (MappedGridSize[0] * (int)Math.Floor((z - WorldEdge[3]) / PixelEdge[3]))
                + (int)Math.Floor((x - WorldEdge[0]) / PixelEdge[2]);
        }

        public bool WorldPosWithinWorldEdges(float x, float z)
        {
            return
                x >= WorldEdge[0] &&
                z <  WorldEdge[1] &&
                x <  WorldEdge[2] &&
                z >= WorldEdge[3];
        }

        //reference: 0x002f7c80 in prototype
        /// <summary>
        /// Get the world positions for the edges of a texture with a given width/height.
        /// </summary>
        public bool GetTextureWorldEdges(int texWidth, int texHeight, out float xLeft, out float xRight, out float zUp, out float zBottom)
        {
            float worldXSpan = WorldEdge[2] - WorldEdge[0];
            float worldZSpan = WorldEdge[1] - WorldEdge[3];
            float pixelXSpan = PixelEdge[2] - PixelEdge[0];
            float pixelZSpan = PixelEdge[3] - PixelEdge[1];

            //Div by 0 guard + other checks
            if (pixelXSpan == 0 || pixelZSpan == 0 || texWidth <= 0 || texHeight <= 0)
            {
                xLeft = 0;
                xRight = 0;
                zUp = 0;
                zBottom = 0;
                return false;
            }

            float xRatio = worldXSpan / pixelXSpan;
            float zRatio = worldZSpan / pixelZSpan;

            zUp     = WorldEdge[1] + PixelEdge[1] * zRatio;
            xRight  = WorldEdge[2] + (( (float)texWidth  * 2) - PixelEdge[2]) * xRatio;
            zBottom = WorldEdge[3] - (( (float)texHeight * 2) - PixelEdge[3]) * zRatio;
            xLeft   = WorldEdge[0] - PixelEdge[0] * xRatio;

            return true;
        }

        public bool[][] GetArrayFromBitHeap(BitHeap bitHeap, int bitHeapOffset)
        {
            ArgumentNullException.ThrowIfNull(bitHeap);

            if (!bitHeap.IsValid)
                throw new ArgumentException("BitHeap's data is invalid.");

            int size = BitHeapSize;
            if (size == 0)
                return [];

            int[] dim = MappedGridSize;

            //generate array
            bool[][] map = new bool[dim[1]][];
            for(int i = 0; i < dim[1]; i++)
                map[i] = new bool[dim[0]];

            byte[] bits = bitHeap.ReadBits(size, bitHeapOffset);

            int readByte = 0;
            int readBit = 0;

            for(int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[i].Length; j++)
                {
                    bool bit = ((bits[readByte] >> readBit) & 1) != 0;

                    readBit++;
                    //Check for rollover
                    if (readBit > 7)
                    {
                        readByte++;
                        readBit = 0;
                    }

                    map[i][j] = bit;
                }

            return map;
        }
    }
}
