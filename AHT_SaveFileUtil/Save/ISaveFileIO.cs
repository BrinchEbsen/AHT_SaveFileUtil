using AHT_SaveFileUtil.Common;
using System.IO;

namespace AHT_SaveFileUtil.Save
{
    /// <summary>
    /// Defines an object to be read from and/or written to a savefile via a <see cref="FileStream"/>.
    /// </summary>
    /// <typeparam name="T">The type of the implementing class itself.</typeparam>
    public interface ISaveFileIO<T>
    {
        /// <summary>
        /// Read the object's data from the stream contained in a <see cref="BinaryReader"/>.
        /// The stream's position must be at the starting position of the relevant data,
        /// and the position will be after the relevant data when done.
        /// </summary>
        /// <param name="reader">The reader through which to read data from the underlying stream.</param>
        /// <param name="platform">The platform the savefile is for.</param>
        /// <returns>An instance of the object with the data read from the reader's underlying stream.</returns>
        public abstract static T FromReader(BinaryReader reader, GamePlatform platform);

        /// <summary>
        /// Write the object instance's data to a stream contained in a <see cref="BinaryWriter"/>.
        /// The stream's position must be at the starting position of the data's intended range,
        /// and the position will be after this range when done.
        /// </summary>
        /// <param name="writer">The writer through which to write data to the underlying stream.</param>
        /// <param name="platform">The platform the savefile is for.</param>
        public abstract void ToWriter(BinaryWriter writer, GamePlatform platform);
    }
}
