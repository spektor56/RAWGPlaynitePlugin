using Rawg.Api;
using System.Windows;
using System.Windows.Controls;


namespace RAWGMetadata
{
    /// <summary>
    /// Interaction logic for RawgMetadataSettingsView.xaml
    /// </summary>
    public partial class RawgMetadataSettingsView : UserControl
    {
        private readonly RawgMetadataPlugin _plugin;

        public RawgMetadataSettingsView()
        {
            InitializeComponent();
        }

        public RawgMetadataSettingsView(RawgMetadataPlugin plugin)
        {
            _plugin = plugin;
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            
            var platformApi = new PlatformsApi();
            
            var platforms = await platformApi.PlatformsListAsync();
            
        }
    }
}
