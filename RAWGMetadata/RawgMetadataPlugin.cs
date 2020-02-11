using Playnite.SDK;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RAWGMetadata
{
    public class RawgMetadataPlugin : MetadataPlugin
    {
        
        internal readonly RawgMetadataSettings Settings;

        public RawgMetadataPlugin(IPlayniteAPI playniteAPI) : base(playniteAPI)
        {
            Settings = new RawgMetadataSettings(this);
            
        }

        public override ISettings GetSettings(bool firstRunSettings)
        {
            return Settings;
        }

        public override UserControl GetSettingsView(bool firstRunView)
        {
            return new RawgMetadataSettingsView(this);
        }

        public override string Name { get; } = "RAWG";

        public override List<MetadataField> SupportedFields { get; } = new List<MetadataField>
        {
            MetadataField.Name,
            MetadataField.Genres,
            MetadataField.ReleaseDate,
            MetadataField.Developers,
            MetadataField.Publishers,
            //MetadataField.Tags,
            MetadataField.Description,
            MetadataField.Links,
            MetadataField.CriticScore,
            MetadataField.CommunityScore,
            MetadataField.Icon,
            MetadataField.CoverImage,
            MetadataField.BackgroundImage

        };

        public override Guid Id => new Guid();

        public override OnDemandMetadataProvider GetMetadataProvider(MetadataRequestOptions options)
        {
            return new RawgLazyMetadataProvider(options, this);
        }
    }
}
