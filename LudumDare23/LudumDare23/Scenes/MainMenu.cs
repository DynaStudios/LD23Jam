using DynaStudios;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using QuickFont;
using System;
using OpenTK.Input;

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

        private float getCursorPosition()
        {
            float position = 0.0f;

            switch (_cursorYPosition)
            {
                case 1:
                    position = 0.27f;
                    break;
                case 2:
                    position = 0.17f;
                    break;
                case 3:
                    position = 0.06f;
                    break;
            }

            return position;
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
            GL.LoadIdentity();
            GL.BindTexture(TextureTarget.Texture2D, _cursorTexture);
            GL.Translate(0.37, getCursorPosition(), 0);

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

            Engine.InputDevice.Keyboard.KeyUp += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(Keyboard_KeyUp);

        }

        void Keyboard_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                selectMenuEvent();
            }
            if (e.Key == Key.Up)
            {
                if (_cursorYPosition != 1)
                {
                    _cursorYPosition--;
                }
                else
                {
                    _cursorYPosition = 3;
                }
            }
            if (e.Key == Key.Down)
            {
                if (_cursorYPosition != 3)
                {
                    _cursorYPosition++;
                }
                else
                {
                    _cursorYPosition = 1;
                }
            }
        }

        public void unloadScene()
        {
            GL.DeleteTexture(_backgroundTexture);
            GL.DeleteTexture(_cursorTexture);
            Engine.InputDevice.Keyboard.KeyUp -= Keyboard_KeyUp;
        }

        public void selectMenuEvent()
        {
            switch (_cursorYPosition)
            {
                case 1:
                    Engine.switchScene("mainWorld");
                    break;
                case 2:
                    Engine.switchScene("about");
                    break;
                case 3:
                    Engine.Close();
                    break;
            }
        }

    }
}
