namespace AHT_SaveFileUtil.Common
{
    public class EXVector3
    {
        public EXVector3() { }

        public EXVector3(EXVector3 o)
        {
            X = o.X; Y = o.Y; Z = o.Z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
