using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Background : Sprite
    {
        private const string k_AssetName = @"Sprites\BG_Space01_1024x768";

        public Background(Game i_Game, GameScreen i_Screen) : base(k_AssetName, i_Game, i_Screen)
        {
        }

        public Background(Game i_Game, GameScreen i_Screen, string i_AssetName) : base(i_AssetName, i_Game, i_Screen)
        {
        }
    }
}
