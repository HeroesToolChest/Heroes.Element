namespace Heroes.Element.Models.ObjectProperties;

internal interface IImagePath
{
    /// <summary>
    /// Gets or sets the relative path of the image that resides in CASC or on file.
    /// </summary>
    RelativeFilePath? ImagePath { get; set; }
}
