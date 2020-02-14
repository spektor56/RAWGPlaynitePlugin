using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAWGMetadata
{
    public class RawgMetadataSettings : ObservableObject, ISettings
    {
        private RawgMetadataSettings editingClone;
        private readonly RawgMetadataPlugin plugin;

        public RawgMetadataSettings()
        {
        }

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
