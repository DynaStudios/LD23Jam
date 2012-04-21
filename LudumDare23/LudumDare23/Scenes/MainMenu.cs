using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaStudios;

namespace LudumDare23.Scenes
{
    public class MainMenu : IScene
    {

        public Engine Engine { get; set; }

        public MainMenu(Engine engine)
        {
            Engine = engine;
            Engine.Logger.Debug("Loaded MainMenu");
        }

        public void doRender()
        {
           
        }

        public void loadScene()
        {
            //Load Textures here
          
        }

        public void unloadScene()
        {
           
        }
    }
}
