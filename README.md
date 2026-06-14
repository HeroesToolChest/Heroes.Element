# Heroes Element
[![Build](https://github.com/HeroesToolChest/Heroes.Element/actions/workflows/build.yml/badge.svg)](https://github.com/HeroesToolChest/Heroes.Element/actions/workflows/build.yml)
[![Release](https://img.shields.io/github/release/HeroesToolChest/Heroes.Element.svg)](https://github.com/HeroesToolChest/Heroes.Element/releases/latest) [![NuGet](https://img.shields.io/nuget/v/Heroes.Element.svg)](https://www.nuget.org/packages/Heroes.Element/)

Heroes Element is a .NET library to read the JSON files from [Heroes Data Parser](https://github.com/HeroesToolChest/HeroesDataParser) and provides an API to access the data with multi-localization support.

The json files are also available in the  [Heroes Data](https://github.com/HeroesToolChest/heroes-data2) repository.

## Usage
There is a `<data-file-name>DataDocument` class for each json data file. Each provides a `Load` method that accepts a `JsonDocument`.

Example of loading the `herodata` file.
```C#
// load the data file
using FileStream dataFile = File.OpenRead("herodata_97039_enus.json");
using HeroDataDocument dataDocument = HeroDataDocument.Load(JsonDocument.Parse(dataFile));

// get the meta properties
MetaDataProperties metaDataProperties = dataDocument.Meta;

// get hero data for a specific hero
Hero heroData = dataDocument.GetElementById("Alarak");

// get some properties
string? attributeId = heroData.AttributeId;
GameStringText? name =  heroData.Name;
GameStringText? description = heroData.Description;
IDictionary<AbilityTier, IList<Ability>> abilities = heroData.Abilities;
IDictionary<TalentTier, IList<Talent>> talents = heroData.Talents;
```

Or if the data file's `localizedText` property is set to `Extracted`, a gamestrings file may be loaded in.
```C#
using FileStream dataFile = File.OpenRead("herodata_97039_enus.json");
using FileStream gamestringsFile = File.OpenRead("gamestrings_97039_eses.json");
using GameStringsDocument gameStringsDocument = GameStringsDocument.Load(JsonDocument.Parse(gamestringsFile));

// pass in the gameStringsDocument as the second argument
using HeroDataDocument dataDocument = HeroDataDocument.Load(JsonDocument.Parse(dataFile), gameStringsDocument);
```

### Updating the GameStringsTexts
To update a data object's gamestrings to a different gamestrings file, such as a different locale, load the gamestrings file via the `GameStringsDocument` class.

```C#
// load a gamestrings file
using FileStream gamestringsFile = File.OpenRead("gamestrings_97039_frfr.json");
using GameStringsDocument gameStringsDocument = GameStringsDocument.Load(JsonDocument.Parse(gamestringsFile));

// get the meta properties
MetaGameStringProperties metaGameStringProperties = gameStringsDocument.Meta;

// update an existing heroData instance with the new gamestrings
gameStringsDocument.UpdateGameStringTexts(heroData);

// Or, use the extension method
heroData.UpdateGameStringTexts(gameStringsDocument);
```

## Developing
To build and compile the code, it is recommended to use the latest version of [Visual Studio 2026 or Visual Studio Code](https://visualstudio.microsoft.com/downloads/).

Another option is to use the dotnet CLI tools from the latest [.NET SDK](https://dotnet.microsoft.com/download).

## License
[MIT license](/LICENSE)
