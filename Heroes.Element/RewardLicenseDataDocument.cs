namespace Heroes.Element;

/// <summary>
/// Represents a data source for managing and retrieving <see cref="RewardLicense"/> objects from a JSON document.
/// </summary>
public class RewardLicenseDataDocument : ElementDocument<RewardLicense>
{
    private RewardLicenseDataDocument(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
        : base(DataType.RewardLicenseData, dataDocument, gameStringsDocument)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="RewardLicenseDataDocument"/> from the specified JSON document.
    /// </summary>
    /// <param name="dataDocument">The JSON document containing the data.</param>
    /// <param name="gameStringsDocument">The optional JSON document containing the gamestrings.</param>
    /// <returns>A <see cref="RewardLicenseDataDocument"/> object initialized with the data from the provided JSON document.</returns>
    /// <exception cref="JsonException">Thrown when the JSON document is invalid or cannot be parsed.</exception>
    public static RewardLicenseDataDocument Load(JsonDocument dataDocument, GameStringsDocument? gameStringsDocument = null)
    {
        return new RewardLicenseDataDocument(dataDocument, gameStringsDocument);
    }

    /// <inheritdoc/>
    protected override void UpdateGameStringTexts(RewardLicense element)
    {
        return;
    }
}
