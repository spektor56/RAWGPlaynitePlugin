using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAWGMetadata
{
    public class RawgLazyMetadataProvider : OnDemandMetadataProvider
    {
        private readonly MetadataRequestOptions options;
        private readonly RawgMetadataPlugin plugin;
        private readonly ulong gameId = 0;
        private List<MetadataField> availableFields;

        public override List<MetadataField> AvailableFields
        {
            get
            {
                return plugin.SupportedFields;
            }
        }

        public RawgLazyMetadataProvider(MetadataRequestOptions options, RawgMetadataPlugin plugin)
        {
            //Game object is in the options class
            //This class will search for the game once (name + platform), then use gameid on subsequent lookups to load each metadata field.
            this.options = options;
            this.plugin = plugin;
        }

        public RawgLazyMetadataProvider(ulong gameId, RawgMetadataPlugin plugin)
        {
            this.gameId = gameId;
            this.plugin = plugin;
        }
    }
}
