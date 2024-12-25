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

        public float[] WorldEdge { get; set; } = new float[4];

        public float[] PixelEdge { get; set; } = new float[4];

        public MiniMapInfo() { }
    }
}
