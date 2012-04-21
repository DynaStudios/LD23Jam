using System.Diagnostics;
using DynaStudios;
using System;
using OpenTK.Graphics.OpenGL;
using DynaStudios.IO;

namespace LudumDare23.Scenes
{
    public class SplashScreen : IScene
    {

        public Engine Engine { get; set; }

        private int _splashScreenTexture;

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

            //Render Stuff here
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.MatrixMode(MatrixMode.Texture);
            //GL.Translate(0, 0, 0);
            //GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Begin(BeginMode.Quads);
            GL.BindTexture(TextureTarget.Texture2D, _splashScreenTexture);

            //Console.WriteLine(textureId);

            GL.TexCoord2(0, 1); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0, 0); GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.End();

            if (timeDifference > (long)(splashVisisble * 1000))
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

            //TextureManager.InitTexturing();
            _splashScreenTexture = Engine.TextureManager.getTexture(@"Images\Game\dyna_splash.png");
        }

        public void unloadScene()
        {
            //Unload Texture from GPU here
            Engine.Logger.Debug("SplashScreen Unload called");
            GL.Enable(EnableCap.DepthTest);

            GL.DeleteTexture(_splashScreenTexture);

        }
    }
}
