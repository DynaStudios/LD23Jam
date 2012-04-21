using DynaStudios;
using LudumDare23.Scenes;

namespace LudumDare23
{
    public class Game : Engine
    {
        public Game() : base(System.Reflection.Assembly.GetExecutingAssembly().Location + "/Maps")
        {

            //Define your Screens here. Don't worry! They won't be dity up your resources as long you use the loadScene() method as your constructor.
            IScene splashScreen = new SplashScreen(this);
            IScene mainMenuScreen = new MainMenu(this);
            IScene mainWorld = new WorldScene(this);

            addScene("splashScreen", splashScreen);
            addScene("mainMenu", mainMenuScreen);
            addScene("mainWorld", mainWorld);
            
            //This is the Start Screen. Screens can switch screens.
            switchScene("mainMenu");
        }

        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }

    }
}
