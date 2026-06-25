namespace Heroes.Element.Models;

internal class ImagePath
{
    /// <summary>
    /// Gets the relative file path where the image exists at. This may be an mpq file path.
    /// </summary>
    public required string FilePath { get; init; }

    /// <summary>
    /// Gets the file path of the image that exists in the mpq file.
    /// </summary>
    public string? MpqEntryPath { get; init; }

    /// <summary>
    /// Gets a value indicating whether the image path is an mpq entry.
    /// </summary>
    [MemberNotNullWhen(true, nameof(MpqEntryPath))]
    public bool IsMpqEntry => !string.IsNullOrWhiteSpace(MpqEntryPath);
}
