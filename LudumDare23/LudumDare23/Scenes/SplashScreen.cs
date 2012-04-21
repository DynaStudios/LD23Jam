using DynaStudios;

namespace LudumDare23.Scenes
{
    public class SplashScreen : IScene
    {

        public Engine Engine { get; set; }

        //3 Seconds visible
        public int splashVisisble = 3;

        public SplashScreen(Engine engine)
        {
            Engine = engine;
            Engine.Logger.Debug("Loaded SplashScreen");
        }

        public void doRender()
        {
            //Render Splash here

            
        }

        public void loadScene()
        {
            
        }

        public void unloadScene()
        {
            //Unload Texture from GPU here
            
        }
    }
}
