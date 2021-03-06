﻿using Rawg.Api;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using RAWGMetadata.Extensions;

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
            var developerApi = new DevelopersApi();
            var test = await developerApi.DevelopersListAsync(null, null);
            var platformApi = new PlatformsApi();
            var platforms = await platformApi.PlatformsListAsync();
            var _platformList = platforms.Results.ToDictionary(result => result.Name, result => (int)result.Id, StringComparer.OrdinalIgnoreCase);

            if (_platformList.ContainsKey("NES"))
            {
                string gameName = "Addams family, The";
                var gamesApi = new GamesApi();
                
                var gameList = await gamesApi.GamesListAsync(null, null, gameName, null, _platformList["NES"].ToString());
                
                var foundGame = gameList.Results.FirstOrDefault(game => game.Name.Sanitize().Equals(gameName.Sanitize()));
                if(foundGame == null)
                {
                    foundGame = gameList.Results.First();
                }
                var maxRating = gameList.Results.Max(game => game.Rating);
            }
            

            
            foreach (var platform in platforms.Results)
            {
                Debug.Print(platform.Id.ToString());
                Debug.Print(platform.Name);
            }

        }
    }
}
