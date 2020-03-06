using Playnite.SDK;
using System;
using System.Collections.Generic;

namespace RAWGMetadata
{
    public class RawgMetadataSettings : ObservableObject, ISettings
    {
        private RawgMetadataSettings editingClone;
        private readonly RawgMetadataPlugin plugin;

        public RawgMetadataSettings()
        {
        }

        public Dictionary<string, int> PlatformList { get; set; } = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"PC", 4},
            {"Xbox One", 1},
            {"PlayStation 4", 18},
            {"iOS", 3},
            {"Android", 21},
            {"macOS", 5},
            {"Linux", 6},
            {"Nintendo Switch", 7},
            {"Nintendo 3DS", 8},
            {"Nintendo DS", 9},
            {"Nintendo DSi", 13},
            {"Xbox 360", 14},
            {"Xbox", 80},
            {"PlayStation 3", 16},
            {"PlayStation 2", 15},
            {"PlayStation", 27},
            {"PS Vita", 19},
            {"PSP", 17},
            {"Wii U", 10},
            {"Wii", 11},
            {"GameCube", 105},
            {"Nintendo 64", 83},
            {"Game Boy Advance", 24},
            {"Game Boy Color", 43},
            {"Game Boy", 26},
            {"SNES", 79},
            {"NES", 49},
            {"Classic Macintosh", 55},
            {"Apple II", 41},
            {"Commodore / Amiga", 166},
            {"Atari 7800", 28},
            {"Atari 5200", 31},
            {"Atari 2600", 23},
            {"Atari Flashback", 22},
            {"Atari 8-bit", 25},
            {"Atari ST", 34},
            {"Atari Lynx", 46},
            {"Atari XEGS", 50},
            {"Genesis", 167},
            {"SEGA Saturn", 107},
            {"SEGA CD", 119},
            {"SEGA 32X", 117},
            {"SEGA Master System", 74},
            {"Dreamcast", 106},
            {"3DO", 111},
            {"Jaguar", 112},
            {"Game Gear", 77},
            {"Neo Geo", 12},
            {"Web", 171}
        };

        public RawgMetadataSettings(RawgMetadataPlugin plugin)
        {
            this.plugin = plugin;

            var settings = plugin.LoadPluginSettings<RawgMetadataSettings>();
            if (settings != null)
            {
                LoadValues(settings);
            }

        }

        public void BeginEdit()
        {
            editingClone = this.GetClone();
        }

        public void EndEdit()
        {
            plugin.SavePluginSettings(this);
        }

        public void CancelEdit()
        {
            LoadValues(editingClone);
        }

        public bool VerifySettings(out List<string> errors)
        {
            errors = null;
            return true;
        }

        private void LoadValues(RawgMetadataSettings source)
        {
            source.CopyProperties(this, false, null, true);
        }
    }
}
