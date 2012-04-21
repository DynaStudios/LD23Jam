using DynaStudios;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using QuickFont;
using System;

namespace LudumDare23.Scenes
{
    public class MainMenu : IScene
    {

        public Engine Engine { get; set; }

        private int _backgroundTexture;
        private int _cursorTexture;
        private int _cursorYPosition = 1;

        public MainMenu(Engine engine)
        {
            Engine = engine;
            Engine.Logger.Debug("Loaded MainMenu");
        }

        public void doRender()
        {

            GL.LoadIdentity();
            GL.BindTexture(TextureTarget.Texture2D, _backgroundTexture);

            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0, 1); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0, 0); GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.End();

            //Render Cursor on his Position here

            GL.MatrixMode(MatrixMode.Modelview);
            GL.Translate(50, 0, 0);
            GL.LoadIdentity();
            GL.BindTexture(TextureTarget.Texture2D, _cursorTexture);


            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0, 1); GL.Vertex3(-0.05f, -0.035f, 0.05f);
            GL.TexCoord2(1, 1); GL.Vertex3(0.05f, -0.035f, 0.05f);
            GL.TexCoord2(1, 0); GL.Vertex3(0.05f, 0.035f, 0.05f);
            GL.TexCoord2(0, 0); GL.Vertex3(-0.05f, 0.035f, 0.05f);

            GL.End();
        }

        public void loadScene()
        {

            Engine.Camera.WorldObject = null;

            GL.Viewport(new System.Drawing.Size(Engine.Width, Engine.Height));

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(-5.0f, 5.0f, -30.0f, 15.0f, -1.0f, 1.0f);
            GL.Disable(EnableCap.DepthTest);

            //GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            //GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            _cursorTexture = Engine.TextureManager.getTexture(@"Images\Game\menuselection.png");
            _backgroundTexture = Engine.TextureManager.getTexture(@"Images\Game\mainmenu.png");

        }

        public void unloadScene()
        {
            GL.DeleteTexture(_backgroundTexture);
            GL.DeleteTexture(_cursorTexture);
        }

    }
}
