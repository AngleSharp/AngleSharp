namespace AngleSharp.Text;

using System;
using Common;

/// <summary>
/// A text source whose characters live in a buffer that is pooled or overwritten as it reads, so
/// its public <see cref="IReadOnlyTextSource.ReadMemory"/> returns a copy.
/// </summary>
internal interface IPooledTextSource
{
    /// <summary>
    /// Reads like <see cref="IReadOnlyTextSource.ReadMemory"/>, but returns a slice of the buffer.
    /// The slice is valid only until the source reads on, changes its encoding or is disposed.
    /// </summary>
    /// <param name="characters">The number of characters to read.</param>
    /// <returns>The characters read.</returns>
    StringOrMemory ReadPooledMemory(Int32 characters);
}
