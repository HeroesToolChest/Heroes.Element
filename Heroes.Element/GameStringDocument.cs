namespace Heroes.Element;

public class GameStringDocument
{
    private GameStringDocument(JsonDocument jsonDocument)
    {
        JsonDocument = jsonDocument;
    }

    public static GameStringDocument Load(JsonDocument jsonDocument)
    {
        return new GameStringDocument(jsonDocument);
    }

    public JsonDocument JsonDocument { get; }
}
