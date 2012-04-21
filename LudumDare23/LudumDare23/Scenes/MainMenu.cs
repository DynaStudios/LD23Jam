using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaStudios;
using OpenTK.Graphics.OpenGL;

namespace LudumDare23.Scenes
{
    public class MainMenu : IScene
    {

        public Engine Engine { get; set; }

        private int _backgroundTexture;

        public MainMenu(Engine engine)
        {
            Engine = engine;
            Engine.Logger.Debug("Loaded MainMenu");
        }

        public void doRender()
        {
            GL.LoadIdentity();

            GL.Begin(BeginMode.Quads);
            GL.BindTexture(TextureTarget.Texture2D, _backgroundTexture);

            //Console.WriteLine(textureId);

            GL.TexCoord2(0, 1); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0, 0); GL.Vertex3(-1.0f, 1.0f, 1.0f);

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

            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            _backgroundTexture = Engine.TextureManager.getTexture(@"Images\Game\mainmenu.png");
          
        }

        public void unloadScene()
        {
            GL.DeleteTexture(_backgroundTexture);
        }
    }
}
