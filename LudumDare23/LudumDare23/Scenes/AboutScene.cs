﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynaStudios;

namespace LudumDare23.Scenes
{
    public class AboutScene : IScene
    {

        public Engine Engine { get; set; }

        public AboutScene(Engine engine)
        {
            Engine = engine;
        }

        public void doRender()
        {
           
        }

        public void loadScene()
        {
       
        }

        public void unloadScene()
        {
          
        }
    }
}
