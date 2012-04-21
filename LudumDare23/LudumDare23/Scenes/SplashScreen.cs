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


            if (true)
            {
                //After 3 seconds switch to Main Menu
                Engine.switchScene("mainMenu");
            }
            
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
