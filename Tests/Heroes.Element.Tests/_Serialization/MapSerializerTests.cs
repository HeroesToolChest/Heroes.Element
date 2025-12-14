namespace Heroes.Element.Serialization.Tests;

[TestClass]
public class MapSerializerTests
{
    [TestMethod]
    public void Serialize_AllPropertiesSet_ReturnsJson()
    {
        // arrange
        Map map = new("map_id")
        {
            // Map properties
            Name = new GameStringText("Map Name"),
            MapId = "map_replay_id",
            MapLink = "map_link_id",
            MapSize = new MapSize(200.0, 180.0),
            ReplayPreviewImage = "replay_preview.dds",
            LoadingScreenImage = "loading_screen.dds",
            MapObjectives =
            [
                new MapObjective
                {
                    Title = new GameStringText("Primary Objective"),
                    Description = new GameStringText("Capture the points to gain control"),
                    Icons =
                    [
                        new MapObjectiveIcon
                        {
                            Image = "objective_icon1.dds",
                            Height = 64,
                            ScaleWidth = true,
                        },
                        new MapObjectiveIcon
                        {
                            Image = "objective_icon2.dds",
                            Height = 128,
                            ScaleWidth = false,
                        },
                    ],
                },
                new MapObjective
                {
                    Title = new GameStringText("Secondary Objective"),
                    Description = new GameStringText("Collect mercenary camps for additional support"),
                    Icons =
                    [
                        new MapObjectiveIcon
                        {
                            Image = "objective_icon3.dds",
                            Height = 96,
                            ScaleWidth = true,
                        },
                    ],
                },
            ],
        };

        // act
        string json = JsonSerializer.Serialize(map, SerializerSettings.SetJsonSerializerDataOptions());

        // assert
        json.Should().Be(
            """
            {
              "name": "Map Name",
              "mapId": "map_replay_id",
              "mapLink": "map_link_id",
              "mapSize": {
                "x": 200,
                "y": 180
              },
              "replayPreviewImage": "replay_preview.dds",
              "loadingScreenImage": "loading_screen.dds",
              "mapObjectives": [
                {
                  "title": "Primary Objective",
                  "description": "Capture the points to gain control",
                  "icons": [
                    {
                      "image": "objective_icon1.dds",
                      "height": 64,
                      "scaleWidth": true
                    },
                    {
                      "image": "objective_icon2.dds",
                      "height": 128,
                      "scaleWidth": false
                    }
                  ]
                },
                {
                  "title": "Secondary Objective",
                  "description": "Collect mercenary camps for additional support",
                  "icons": [
                    {
                      "image": "objective_icon3.dds",
                      "height": 96,
                      "scaleWidth": true
                    }
                  ]
                }
              ]
            }
            """);
    }
}
