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
            // Front Face
//             glTexCoord2f(0.0f, 0.0f); glVertex3f(-1.0f, -1.0f, 1.0f);  // Bottom Left Of The Texture and Quad
//             glTexCoord2f(1.0f, 0.0f); glVertex3f(1.0f, -1.0f, 1.0f);  // Bottom Right Of The Texture and Quad
//             glTexCoord2f(1.0f, 1.0f); glVertex3f(1.0f, 1.0f, 1.0f);  // Top Right Of The Texture and Quad
//             glTexCoord2f(0.0f, 1.0f); glVertex3f(-1.0f, 1.0f, 1.0f);

            GL.Begin(BeginMode.Quads);

            TextureManager.InitTexturing();
            int textureId = Engine.TextureManager.getTexture(@"Images\Game\dyna_splash.png");
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.End();

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
