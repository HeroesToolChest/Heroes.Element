# Heroes Element
[![Build](https://github.com/HeroesToolChest/Heroes.Element/actions/workflows/build.yml/badge.svg)](https://github.com/HeroesToolChest/Heroes.Element/actions/workflows/build.yml)
[![Release](https://img.shields.io/github/release/HeroesToolChest/Heroes.Element.svg)](https://github.com/HeroesToolChest/Heroes.Element/releases/latest) [![NuGet](https://img.shields.io/nuget/v/Heroes.Element.svg)](https://www.nuget.org/packages/Heroes.Element/)

Heroes Element is a .NET library to read the JSON files from [Heroes Data Parser](https://github.com/HeroesToolChest/HeroesDataParser) and provides an API to access the data with multi-localization support.

This library only supports JSON files generated from Heroes Data Parser `v5.0.0` or later.

The JSON files are also available in the [Heroes Data](https://github.com/HeroesToolChest/heroes-data2) repository.

## Usage
### Data documents
Each JSON data file has a matching data document class with a `Load` method that accepts a `JsonDocument` of the file. Each item in the file is returned as a model object.

Data files are named `<data type>_<build>_<locale>.json`, such as `herodata_97039_enus.json`. If the localized text was extracted into separate gamestrings files, the locale is left off, such as `herodata_97039.json`.

| Data file | Data document | Model |
| --- | --- | --- |
| `announcerpackdata` | `AnnouncerPackDataDocument` | `AnnouncerPack` |
| `bannerdata` | `BannerDataDocument` | `Banner` |
| `boostdata` | `BoostDataDocument` | `Boost` |
| `bundledata` | `BundleDataDocument` | `Bundle` |
| `emoticondata` | `EmoticonDataDocument` | `Emoticon` |
| `emoticonpackdata` | `EmoticonPackDataDocument` | `EmoticonPack` |
| `herodata` | `HeroDataDocument` | `Hero` |
| `lootchestdata` | `LootChestDataDocument` | `LootChest` |
| `mapdata` | `MapDataDocument` | `Map` |
| `matchawarddata` | `MatchAwardDataDocument` | `MatchAward` |
| `mountdata` | `MountDataDocument` | `Mount` |
| `portraitpackdata` | `PortraitPackDataDocument` | `PortraitPack` |
| `rewardlicensedata` | `RewardLicenseDataDocument` | `RewardLicense` |
| `rewardportraitdata` | `RewardPortraitDataDocument` | `RewardPortrait` |
| `skindata` | `SkinDataDocument` | `Skin` |
| `spraydata` | `SprayDataDocument` | `Spray` |
| `typedescriptiondata` | `TypeDescriptionDataDocument` | `TypeDescription` |
| `unitdata` | `UnitDataDocument` | `Unit` |
| `veterancydata` | `VeterancyDataDocument` | `Veterancy` |
| `voicelinedata` | `VoiceLineDataDocument` | `VoiceLine` |

A data document takes ownership of the `JsonDocument` passed to `Load` and disposes it when the data document is disposed, so the `JsonDocument` should not be used or disposed separately.

### Loading a data file
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
GameStringText? name = heroData.Name;
GameStringText? description = heroData.Description;
IDictionary<AbilityTier, IList<Ability>> abilities = heroData.Abilities;
IDictionary<TalentTier, IList<Talent>> talents = heroData.Talents;
```

### Finding items
`GetElementById` throws a `KeyNotFoundException` if the id is not in the file. The other lookups are:
```C#
// returns false instead of throwing when the id is not found
bool found = dataDocument.TryGetElementById("Alarak", out Hero? hero);

// find an item by its hyperlinkId or attributeId instead of its id
Hero byHyperlinkId = dataDocument.GetElementByHyperlinkId("Alarak");
Hero byAttributeId = dataDocument.GetElementByAttributeId("Alar");

// get every item in the file
IEnumerable<Hero> heroes = dataDocument.GetElements();
```

`GetElementByHyperlinkId` and `GetElementByAttributeId` also have `TryGet...` versions.

### Loading any data file
If the type of data file isn't known ahead of time, `DataDocument.Load` reads the `meta.dataType` property and returns the matching data document.
```C#
using FileStream dataFile = File.OpenRead("skindata_97039_enus.json");
using IElementDocument document = DataDocument.Load(JsonDocument.Parse(dataFile));

if (document is SkinDataDocument skinDocument)
{
    Skin skin = skinDocument.GetElementById("AbathurMechaVar1");
}

// or work with the items without knowing their type
Type elementType = document.ElementType;
IEnumerable<IElementObject> elements = document.GetElementObjects();
```

It throws a `JsonException` if `meta.dataType` is missing or not a recognized value, and an `ArgumentException` if it is not a data file type, such as `GameStrings`.

### Reading localized text
Localized text is returned as a `GameStringText` from the [Heroes Locale Text](https://github.com/HeroesToolChest/Heroes.LocaleText) library, which provides the text in several forms, such as raw, colored, and plain text.

### Using a separate gamestrings file
If the data file's `meta.localizedText` property is set to `Extracted`, a gamestrings file may be loaded in.
```C#
using FileStream dataFile = File.OpenRead("herodata_97039.json");
using FileStream gamestringsFile = File.OpenRead("gamestrings_97039_eses.json");
using GameStringsDocument gameStringsDocument = GameStringsDocument.Load(JsonDocument.Parse(gamestringsFile));

// pass in the gameStringsDocument as the second argument
using HeroDataDocument dataDocument = HeroDataDocument.Load(JsonDocument.Parse(dataFile), gameStringsDocument);
```

Every data document except `VeterancyDataDocument` accepts a gamestrings file. The data document does not dispose the `GameStringsDocument`.

Loading files that don't match does not throw. These properties show whether the two files go together:
```C#
// both files are from the same game version
bool isMatchedHeroesVersion = dataDocument.IsMatchedHeroesVersion;

// both files were generated by the same Heroes Data Parser version
bool isMatchedHdpVersion = dataDocument.IsMatchedHdpVersion;

// the gamestrings file contains the text for this data type
bool isMatchedDataType = dataDocument.IsMatchedDataType;

// both files are for the same map, or both are not for a map
bool isMatchedMapName = dataDocument.IsMatchedMapName;
```

Each property is `true` when no gamestrings file is loaded.

### Updating the gamestring texts
A data object's gamestrings can be updated from a different gamestrings file, such as one for a different locale, by loading that file with the `GameStringsDocument` class.

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

### Reward licenses
Store items, such as heroes, skins, and mounts, list the rewards that unlock them in `RewardLicenseIds`. Each id is the id of a `RewardLicense` in the `rewardlicensedata` file, which has the license id.
```C#
using FileStream skinFile = File.OpenRead("skindata_97039_enus.json");
using FileStream rewardLicenseFile = File.OpenRead("rewardlicensedata_97039_enus.json");
using SkinDataDocument skinDocument = SkinDataDocument.Load(JsonDocument.Parse(skinFile));
using RewardLicenseDataDocument rewardLicenseDocument = RewardLicenseDataDocument.Load(JsonDocument.Parse(rewardLicenseFile));

Skin skin = skinDocument.GetElementById("AbathurMechaVar1");

foreach (string rewardLicenseId in skin.RewardLicenseIds)
{
    if (rewardLicenseDocument.TryGetElementById(rewardLicenseId, out RewardLicense? rewardLicense))
    {
        long licenseId = rewardLicense.LicenseId;
    }
}
```

## Developing
To build and compile the code, it is recommended to use the latest version of [Visual Studio 2026 or Visual Studio Code](https://visualstudio.microsoft.com/downloads/).

Another option is to use the dotnet CLI tools from the latest [.NET SDK](https://dotnet.microsoft.com/download).

## License
[MIT license](/LICENSE)
