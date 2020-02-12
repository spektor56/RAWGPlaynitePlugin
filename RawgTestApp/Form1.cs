using Rawg.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RawgTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            var platformApi = new PlatformsApi();
            var gamesApi = new GamesApi();
            var gameList = await gamesApi.GamesListAsync(null, null, "Super Mario Bros. 2", null, "49");

            
            var platforms = await platformApi.PlatformsListAsync();
            foreach (var platform in platforms.Results)
            {
                Debug.Print(platform.Id.ToString());
                Debug.Print(platform.Name);
            }

        }
    }
}
