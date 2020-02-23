using Playnite.SDK;
using Rawg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAWGMetadata.Model
{
    public class GameOption : GenericItemOption
    {
        public Game Game { get; private set; }

        public GameOption(Game game)
        {
            this.Game = game;
            this.Description = game.Slug;
            this.Name = game.Name;
        }
        
        

    }
}
