using Rawg.Api;
using Rawg.Client;
using Rawg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


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
