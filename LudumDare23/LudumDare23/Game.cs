using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaStudios;

namespace LudumDare23
{
    public class Game : Engine
    {
        public Game() : base(System.Reflection.Assembly.GetExecutingAssembly().Location + "/Maps")
        { 
        }

        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }

    }
}
