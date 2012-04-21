using System.Diagnostics;
using DynaStudios;
using System;

namespace LudumDare23.Scenes
{
    public class SplashScreen : IScene
    {

        public Engine Engine { get; set; }

        //3 Seconds visible
        public int splashVisisble = 2;

        //Private Vars
        private Stopwatch _watch;

        private long _currentTime;
        private long _lastTime;

        public SplashScreen(Engine engine)
        {
            Engine = engine;
            Engine.Logger.Debug("Loaded SplashScreen");
        }

        public void doRender()
        {
            //Render Splash here
            _currentTime = _watch.ElapsedMilliseconds;

            long timeDifference = _currentTime - _lastTime;

            if (timeDifference > (long)(splashVisisble * 1000))
            {
                //After 3 seconds switch to Main Menu
                Engine.switchScene("mainMenu");
            }

        }

        public void loadScene()
        {
            _watch = new Stopwatch();
            _watch.Start();
        }

        public void unloadScene()
        {
            //Unload Texture from GPU here
            Engine.Logger.Debug("SplashScreen Unload called");

        }
    }
}
