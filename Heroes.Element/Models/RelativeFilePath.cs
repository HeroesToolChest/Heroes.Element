namespace Heroes.Element.Models;

internal class RelativeFilePath
{
    /// <summary>
    /// Gets the relative file path (including the file name) of the image that is to the original image file.
    /// </summary>
    public required string FilePath { get; init; }

    /// <summary>
    /// Gets the relative file path of the mpq file that contains the <see cref="FilePath"/>.
    /// </summary>
    public string? MpqFilePath { get; init; }
}
