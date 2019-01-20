using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectModel
{
    public abstract class GameSettings : IGameSettings
    {
        public int CurrentLevel { get; private set; } = 0;

        public GameSettings(Game i_Game)
        {
            i_Game.Services.AddService(typeof(IGameSettings), this);
        }

        protected virtual void setLevel(int i_Level)
        {
            CurrentLevel = i_Level;
        }

        public void GoToNextLevel()
        {
            setLevel(++CurrentLevel);
        }

        public void ResetLevel()
        {
            setLevel(0);
        }
    }
}
