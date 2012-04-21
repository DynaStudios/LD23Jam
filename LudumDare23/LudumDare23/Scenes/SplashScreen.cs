using System.Diagnostics;
using DynaStudios;

namespace LudumDare23.Scenes
{
    public class SplashScreen : IScene
    {

        public Engine Engine { get; set; }

        //3 Seconds visible
        public int splashVisisble = 3;

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
//             _currentTime = _watch.ElapsedMilliseconds;
// 
//             long timeDifference = _currentTime - _lastTime;
// 
//             //If more then 1 Second difference: Recalculate fps
//             if (timeDifference > splashVisisble * 1000 && _lastTime != 0)
//             {
//                 //After 3 seconds switch to Main Menu
//                 Engine.switchScene("mainMenu");
//             }
// 
//             _lastTime = _currentTime;

        }

        public void loadScene()
        {

        }

        public void unloadScene()
        {
            //Unload Texture from GPU here
            Engine.Logger.Debug("SplashScreen Unload called");

        }
    }
}
