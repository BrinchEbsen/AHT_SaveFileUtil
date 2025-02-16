namespace AHT_SaveFileUtil.Save.Triggers
{
    /// <summary>
    /// The type of a <see cref="TriggerDataUnit"/>.
    /// </summary>
    public enum TriggerDataType
    {
        /// <summary>
        /// An unused string of bits.
        /// </summary>
        Unused,
        /// <summary>
        /// A single bit representing a flag.
        /// </summary>
        SingleFlag,
        /// <summary>
        /// A set of flags masked from a unsigned 32-bit integer.
        /// </summary>
        Flags,
        /// <summary>
        /// A signed 32-bit integer.
        /// </summary>
        Int32,
        /// <summary>
        /// A single-precision floating point number.
        /// </summary>
        Float,
        /// <summary>
        /// A vector with four float components.
        /// </summary>
        EXVector
    }
}
