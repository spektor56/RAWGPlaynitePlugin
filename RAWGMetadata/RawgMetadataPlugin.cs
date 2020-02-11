using Playnite.SDK;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAWGMetadata
{
    public class RawgMetadataPlugin : MetadataPlugin
    {
        
        internal readonly RawgMetadataSettings Settings;

        public RawgMetadataPlugin(IPlayniteAPI playniteAPI) : base(playniteAPI)
        {
            Settings = new RawgMetadataSettings(this);
            
        }
        public override string Name => throw new NotImplementedException();

        public override List<MetadataField> SupportedFields => throw new NotImplementedException();

        public override Guid Id => throw new NotImplementedException();

        public override OnDemandMetadataProvider GetMetadataProvider(MetadataRequestOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
