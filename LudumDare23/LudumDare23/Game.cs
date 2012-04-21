using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaStudios;
using LudumDare23.Scenes;

namespace LudumDare23
{
    public class Game : Engine
    {
        public Game() : base(System.Reflection.Assembly.GetExecutingAssembly().Location + "/Maps")
        {

            IScene splashScreen = new SplashScreen(this);
            addScene("splashScreen", splashScreen);
            switchScene("splashScreen");
        }

        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }

    }
}
