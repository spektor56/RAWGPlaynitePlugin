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
            throw new NotImplementedException();
        }

        public void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public void EndEdit()
        {
            throw new NotImplementedException();
        }

        public bool VerifySettings(out List<string> errors)
        {
            throw new NotImplementedException();
        }

        private void LoadValues(RawgMetadataSettings source)
        {
            source.CopyProperties(this, false, null, true);
        }
    }
}
