using AHT_SaveFileUtil.Save.Slot;
using System;

namespace AHT_SaveFileUtil.Save.MiniMap
{
    /// <summary>
    /// The type of a <see cref="MiniMapInfo"/> object,
    /// defining how its data is interpreted.
    /// </summary>
    public enum InfoType
    {
        /// <summary>
        /// Contains definitions for the background texture on a minimap.
        /// </summary>
        Background = 0,
        /// <summary>
        /// Contains definitions for the "revealed" area of a minimap,
        /// a grid of cells with their states stored in the bitheap.
        /// </summary>
        Mapped = 1,
        /// <summary>
        /// Unused type.
        /// </summary>
        Reveal = 2
    }

    /// <summary>
    /// A set of definitions for a minimap.
    /// Interpretations of the data depends on the <see cref="Type"/>.
    /// </summary>
    public class MiniMapInfo
    {
        /// <summary>
        /// The texture dimensions used by calculations.
        /// </summary>
        public const int TEXTURE_DIM = 256;

        /// <summary>
        /// Hashcode for the map geofile.
        /// </summary>
        public uint MapFile { get; set; }

        /// <summary>
        /// Hashcode for the map.
        /// If distinguishing a map with a hashcode is
        /// unnecessary, this value will be 0xFFFFFFFF.
        /// </summary>
        public uint Map { get; set; }

        /// <summary>
        /// The type of this info.
        /// </summary>
        public InfoType Type { get; set; }

        /// <summary>
        /// The geofile containing the texture for the texture for this minimap.
        /// </summary>
        public uint TextureFile { get; set; }

        /// <summary>
        /// The texture hashcode for this minimap.
        /// </summary>
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
        /// <item>[2] X-axis scale (units per cell)</item>
        /// <item>[3] Z-axis scale (units per cell)</item>
        /// </list>
        /// </para>
        /// </summary>
        public float[] PixelEdge { get; set; } = new float[4];

        /// <summary>
        /// For info of type "Mapped".
        /// The dimensions of the mapped area of the minimap, along the X [0] axis and the Z [1] axis.
        /// </summary>
        public int[] MappedGridSize
        {
            get
            {
                if (Type != InfoType.Mapped) return [0, 0];

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

        /// <summary>
        /// For info of type "Mapped".
        /// Number of bits allocated by this minimap.
        /// </summary>
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

        /// <summary>
        /// For info of type "Mapped".
        /// Get the bitheap offset of the cell at the given world position.
        /// </summary>
        /// <param name="x">World x-coordinate.</param>
        /// <param name="z">World z-coordinate.</param>
        /// <returns>The bitheap offset of the cell at world coordinates
        /// <paramref name="x"/> and <paramref name="z"/>.</returns>
        public int GetBitHeapOffset(float x, float z)
        {
            if (Type != InfoType.Mapped) return 0;

            //Div by 0 guard
            if (PixelEdge[2] == 0 || PixelEdge[3] == 0)
                return -1;

            return (MappedGridSize[0] * (int)Math.Floor((z - WorldEdge[3]) / PixelEdge[3]))
                + (int)Math.Floor((x - WorldEdge[0]) / PixelEdge[2]);
        }

        /// <summary>
        /// For info type "Background".
        /// </summary>
        public int[] GetPixelCoordsFromWorldPosition(float x, float z, int textureSize)
        {
            if (Type != InfoType.Background) return [0, 0];

            bool success = GetTextureWorldEdges(
                out float xLeft, out float xRight, out float zUp, out float zBottom);

            if (!success) return [0, 0];

            //Get the world measurements of the background
            float xSpan = Math.Abs(xRight - xLeft);
            float zSpan = Math.Abs(zUp - zBottom);

            //Get the relative coordinates of the given position
            float localX = Math.Abs(x - xLeft);
            float localZ = Math.Abs(z - zUp);

            //Get the ratios of how far the position is along the minimap
            float xRatio = localX / xSpan;
            float zRatio = localZ / zSpan;

            //Multiply by texture size to get final position
            return [
                (int)(textureSize * xRatio),
                (int)(textureSize * zRatio)
                ];
        }

        /// <summary>
        /// For info type "Background".
        /// </summary>
        public float[] GetWorldPositionFromPixelCoords(int x, int z, int textureSize)
        {
            if (Type != InfoType.Background) return [0, 0];

            if (textureSize <= 0) return [0, 0];

            bool success = GetTextureWorldEdges(
                out float xLeft, out float xRight, out float zUp, out float zBottom);

            if (!success) return [0, 0];

            //Get the world measurements of the background
            float xSpan = Math.Abs(xRight - xLeft);
            float zSpan = Math.Abs(zUp - zBottom);

            //Get the ratios of how far the position is along the minimap
            float xRatio = (float)x / (float)textureSize;
            float zRatio = (float)z / (float)textureSize;

            //Get the local position compared to the minimap's top-left
            float localX = xSpan * xRatio;
            float localZ = zSpan * zRatio;

            //Get absolute position
            return [
                xLeft + localX,
                zBottom + localZ,
                ];
        }

        //reference: 0x002f7c80 in prototype
        /// <summary>
        /// For info type "Background".
        /// Get the world positions for the edges of the background texture.
        /// </summary>
        public bool GetTextureWorldEdges(out float xLeft, out float xRight, out float zUp, out float zBottom)
        {
            xLeft = 0;
            xRight = 0;
            zUp = 0;
            zBottom = 0;

            if (Type != InfoType.Background) return false;

            float worldXSpan = WorldEdge[2] - WorldEdge[0];
            float worldZSpan = WorldEdge[1] - WorldEdge[3];
            float pixelXSpan = PixelEdge[2] - PixelEdge[0];
            float pixelZSpan = PixelEdge[3] - PixelEdge[1];

            //Div by 0 guard + other checks
            if (pixelXSpan == 0 || pixelZSpan == 0)
                return false;

            float xRatio = worldXSpan / pixelXSpan;
            float zRatio = worldZSpan / pixelZSpan;

            zUp     = WorldEdge[1] + PixelEdge[1] * zRatio;
            xRight  = WorldEdge[2] + (( (float)TEXTURE_DIM * 2) - PixelEdge[2]) * xRatio;
            zBottom = WorldEdge[3] - (( (float)TEXTURE_DIM * 2) - PixelEdge[3]) * zRatio;
            xLeft   = WorldEdge[0] - PixelEdge[0] * xRatio;

            return true;
        }

        /// <summary>
        /// For info of type "Mapped".
        /// Get a 2D-array of minimap reveal cells.
        /// </summary>
        /// <param name="bitHeap"></param>
        /// <param name="bitHeapOffset"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool[][] GetArrayFromBitHeap(BitHeap bitHeap, int bitHeapOffset)
        {
            if (Type != InfoType.Mapped) return [];

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
