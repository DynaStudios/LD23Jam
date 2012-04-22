using System.Diagnostics;
using DynaStudios;
using OpenTK.Graphics.OpenGL;

namespace LudumDare23.Scenes
{
    public class SplashScreen : IScene
    {

        public Engine Engine { get; set; }

        private int _splashScreenTexture;

        //2 Seconds visible
        public int splashVisible = 2;

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

            //Render Stuff here
            GL.LoadIdentity();

            GL.Begin(BeginMode.Quads);
            GL.BindTexture(TextureTarget.Texture2D, _splashScreenTexture);

            GL.TexCoord2(0, 1); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0, 0); GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.End();

            if (timeDifference > (long)(splashVisible * 1000))
            {
                //After 3 seconds switch to Main Menu
                Engine.switchScene("mainMenu");
            }

        }

        public void loadScene()
        {
            Engine.Camera.WorldObject = null;
            _watch = new Stopwatch();
            _watch.Start();

            GL.Viewport(new System.Drawing.Size(Engine.Width, Engine.Height));

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(-5.0f, 5.0f, -30.0f, 15.0f, -1.0f, 1.0f);
            GL.Disable(EnableCap.DepthTest);

            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            _splashScreenTexture = Engine.TextureManager.getTexture(System.IO.Path.Combine("Images", "Game", "dyna_splash.png"));
        }

        public void unloadScene()
        {
            Engine.Logger.Debug("SplashScreen Unload called");
            GL.Enable(EnableCap.DepthTest);

            Engine.TextureManager.unloadTexture(System.IO.Path.Combine("Images", "Game", "dyna_splash.png"));

        }
    }
}
