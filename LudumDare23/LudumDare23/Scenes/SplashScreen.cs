﻿using System.Diagnostics;
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
            int textureId = Engine.TextureManager.getTexture(@"Images\Game\dyna_splash.bmp");

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            //GL.Translate(0, 0, 0);

            GL.Begin(BeginMode.Quads);

            //Console.WriteLine(textureId);

            GL.TexCoord2(0, 0); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0, 1); GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.End();

//             if (timeDifference > (long)(splashVisisble * 1000))
//             {
//                 //After 3 seconds switch to Main Menu
//                 Engine.switchScene("mainMenu");
//             }

        }

        public void loadScene()
        {
            _watch = new Stopwatch();
            _watch.Start();

            GL.Viewport(new System.Drawing.Size(Engine.Width, Engine.Height));

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            //GL.Ortho(-5.0f, 5.0f, -30.0f, 15.0f, -1.0f, 1.0f);
            GL.Ortho(-5.0f, 5.0f, -30.0f, 15.0f, -1.0f, 1.0f);
            GL.Disable(EnableCap.DepthTest);

            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
//             GL.Enable(EnableCap.Blend);
//             GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            //TextureManager.InitTexturing();
        }

        public void unloadScene()
        {
            //Unload Texture from GPU here
            Engine.Logger.Debug("SplashScreen Unload called");
            GL.Enable(EnableCap.DepthTest);

        }
    }
}
