﻿using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using Rawg.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RAWGMetadata
{
    public class RawgMetadataPlugin : MetadataPlugin
    {
        internal readonly RawgMetadataSettings Settings;
        public bool initializing { get; private set; } = true;
        public Dictionary<string, int> PlatformList { get; private set; }

        public Dictionary<string, string> PlatformTranslationTable = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
            { "3DO Interactive Multiplayer","3DO"},
            { "Adobe Flash","Web"},
            { "Apple II","Apple II"},
            { "Atari 2600","Atari 2600"},
            { "Atari 5200","Atari 5200"},
            { "Atari 7800","Atari 7800"},
            { "Atari Jaguar","Jaguar"},
            { "Atari Lynx","Atari Lynx"},
            { "Atari ST/STE/TT/Falcon","Atari ST"},
            { "Commodore 64","Commodore / Amiga"},
            { "Commodore Amiga","Commodore / Amiga"},
            { "Commodore Amiga CD32","Commodore / Amiga"},
            { "Commodore C128","Commodore / Amiga"},
            { "Commodore CBM2","Commodore / Amiga"},
            { "Commodore PET","Commodore / Amiga"},
            { "Commodore PLUS4","Commodore / Amiga"},
            { "Commodore VIC20","Commodore / Amiga"},
            { "Microsoft Xbox","Xbox"},
            { "Microsoft Xbox 360","Xbox 360"},
            { "Nintendo 3DS","Nintendo 3DS"},
            { "Nintendo 64","Nintendo 64"},
            { "Nintendo DS","Nintendo DS"},
            { "Nintendo Entertainment System","NES"},
            { "Nintendo Game Boy","Game Boy"},
            { "Nintendo Game Boy Advance","Game Boy Advance"},
            { "Nintendo Game Boy Color","Game Boy Color"},
            { "Nintendo GameCube","GameCube"},
            { "Nintendo Switch","Nintendo Switch"},
            { "Nintendo Wii","Wii"},
            { "Nintendo Wii U","Wii U"},
            { "PC (Windows)","PC"},
            { "Sega 32X","SEGA 32X"},
            { "Sega CD","SEGA CD"},
            { "Sega Dreamcast","Dreamcast"},
            { "Sega Game Gear","Game Gear"},
            { "Sega Genesis","Genesis"},
            { "Sega Master System","SEGA Master System"},
            { "Sega Saturn","SEGA Saturn"},
            { "SNK Neo Geo","Neo Geo"},
            { "Sony PlayStation","PlayStation"},
            { "Sony PlayStation 2","PlayStation 2"},
            { "Sony PlayStation 3","PlayStation 3"},
            { "Sony PlayStation Vita","PS Vita"},
            { "Sony PSP","PSP"},
            { "Super Nintendo Entertainment System","SNES"},

        };

        public RawgMetadataPlugin(IPlayniteAPI playniteAPI) : base(playniteAPI)
        {
            Settings = new RawgMetadataSettings(this);
            Properties = new MetadataPluginProperties
            {
                HasSettings = false
            };
            PlatformList = Settings.PlatformList;
            Task.Run(() => {
                try
                {
                    var platformApi = new PlatformsApi();
                    var platforms = platformApi.PlatformsList();
                    PlatformList = platforms.Results.ToDictionary(result => result.Name, result => (int)result.Id, StringComparer.OrdinalIgnoreCase);
                    Settings.PlatformList = PlatformList;
                    Settings.EndEdit();
                }
                finally
                {
                    initializing = false;
                }
            });
        }

        public override ISettings GetSettings(bool firstRunSettings)
        {
            return Settings;
        }

        public override UserControl GetSettingsView(bool firstRunView)
        {
            return new RawgMetadataSettingsView(this);
        }

        public override Guid Id => Guid.Parse("000001D9-DBD1-46C6-B5D0-B1BA557D10E4");
        public override OnDemandMetadataProvider GetMetadataProvider(MetadataRequestOptions options)
        {
            return new RawgLazyMetadataProvider(options, this);
        }

        public override string Name { get; } = "RAWG";

        public override List<MetadataField> SupportedFields { get; } = new List<MetadataField>
        {
            MetadataField.Name,
            MetadataField.Genres,
            MetadataField.ReleaseDate,
            MetadataField.Developers,
            MetadataField.Publishers,
            MetadataField.Tags,
            MetadataField.Description,
            MetadataField.Links,
            MetadataField.CriticScore,
            MetadataField.CommunityScore,
            //MetadataField.Icon,
            //MetadataField.CoverImage,
            MetadataField.BackgroundImage

        };

    }
}
