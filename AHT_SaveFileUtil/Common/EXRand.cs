using System;

namespace AHT_SaveFileUtil.Common
{
    /// <summary>
    /// <para>
    /// Provides methods for generating random <see cref="uint"/> and <see cref="float"/> values.
    /// </para>
    /// <para>
    /// <i>Recreation of EXRandClass from the EngineX source code.</i>
    /// </para>
    /// </summary>
    public static class EXRand
    {
        /// <summary>
        /// The current seed value.
        /// This is updated every time a random value is generated with
        /// the <see cref="Rand32"/> or <see cref="Randf"/> methods.
        /// </summary>
        public static uint Seed { get; set; } = 0;

        //Constants
        private const uint C1 = 0x343FD;
        private const uint C2 = 0x269EC3;
        private const uint C3 = 0x30000000;

        /// <summary>
        /// Generate a random <see cref="uint"/> value.
        /// </summary>
        /// <returns>A new randomly generated <see cref="uint"/> value.</returns>
        public static uint Rand32()
        {
            uint n = Seed * C1 + C2;
            Seed = n * C1 + C2;

            return (n & 0xFFFF0000) | (Seed >> 16);
        }

        /// <summary>
        /// Generate a random <see cref="float"/> value between 0 and 1.
        /// </summary>
        /// <returns>A new randomly generated <see cref="float"/> value between 0 and 1.</returns>
        public static float Randf()
        {
            uint n = Rand32();

            byte[] bytes = BitConverter.GetBytes(C3);
            float f = BitConverter.ToSingle(bytes, 0);

            return (n >> 1) * f;
        }
    }
}
