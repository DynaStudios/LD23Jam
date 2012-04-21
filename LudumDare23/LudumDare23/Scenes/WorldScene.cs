using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;

using DynaStudios;
using DynaStudios.Blocks;
using DynaStudios.Utils;


namespace LudumDare23
{
    public class WorldScene : IScene
    {
        private DateTime _lastFrameTime;
        public Engine Engine { get; set; }
        private Room _room = null;
        private Player player;

        private int _abilityBarTextureId;
        private int _abilitySelectedTextureId;

        public WorldScene(Engine engine)
        {
            Engine = engine;
            Engine.Logger.Debug("Loaded WorldScene");
        }

        public void loadScene()
        {
            player = new Player(Engine.InputDevice);
            player.Position.x =  0.0;
            player.Position.y =  0.0;
            player.Position.z = -3.0;
            Engine.Camera.WorldObject = player;
            //_room = new Room(Path.Combine(DynaStudios.Utils.StreamTool.DIR, "Maps", "map.xml"), Engine.TextureManager);

            _abilityBarTextureId = Engine.TextureManager.getTexture(Path.Combine("Images", "Game", "abilityBar.png"));
            _abilitySelectedTextureId = Engine.TextureManager.getTexture(Path.Combine("Images", "Game", "abilitySelected.png"));
            string roomFilePath = Path.Combine(StreamTool.DIR, "Maps", "Room.xml");
            if (_room == null)
            {
                _room = new Room(roomFilePath, Engine.TextureManager);
            }

            Engine.forceResize();
            _lastFrameTime = DateTime.Now;
        }

        public void doRender()
        {
            DateTime now = DateTime.Now;
            TimeSpan timePast = now - _lastFrameTime;



            //switchBackToFrustrumRendering();

            //GL.Enable(EnableCap.DepthTest);
			

            player.doMovement(timePast);
            GL.Viewport(0, 0, Engine.Width, Engine.Height);
            GL.Enable(EnableCap.DepthTest);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Engine.forceResize();
            //GL.LoadIdentity();
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
			//translate and rotate into the player's view
			GL.Rotate (player.Direction.X,1.0,0.0,0.0);
			GL.Rotate (player.Direction.Y,0.0,1.0,0.0);
			GL.Translate(-player.Position.x, -player.Position.y, -player.Position.z);
            _room.render();

            //Render GUI
            renderGui();

            _lastFrameTime = now;
        }

        private void renderGui()
        {
            int texW = 800;
            int texH = 80;

            int startX = (Engine.Width / 2) - (texW / 2);

            GL.Disable(EnableCap.DepthTest);
            GL.Viewport(startX, 0, texW, texH);
            GL.Enable(EnableCap.ScissorTest);
            GL.Scissor(startX, 0, texW, texH);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 1, 0, 1, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.BindTexture(TextureTarget.Texture2D, _abilityBarTextureId);
            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0, 1); GL.Vertex3(0.00f, 0.00f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(1.00f, 0.00f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(1.00f, 1.00f, 1.0f);
            GL.TexCoord2(0, 0); GL.Vertex3(0.00f, 1.00f, 1.0f);

            GL.End();

            int selTexW = 75;
            int selTexH = 75;

            float selWidth = (float)selTexW / (float)texW;
            float selHeight = (float)selTexH / (float)texH;

            float offsetW = player.WeaponSelectIndex * (selWidth + (13.5f / (float)texW));


            float startSelX = (float)445 / (float)texW + offsetW;
            float startSelY = (float)3 / (float)texH;

            GL.Enable(EnableCap.Blend);
            GL.BindTexture(TextureTarget.Texture2D, _abilitySelectedTextureId);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.DstAlpha);

            GL.Translate(startSelX, startSelY, 0);            
            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0, 1); GL.Vertex3(0.00f, 0.00f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(selWidth, 0.00f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(selWidth, selHeight, 1.0f);
            GL.TexCoord2(0, 0); GL.Vertex3(0.00f, selHeight, 1.0f);

            GL.End();

            GL.Translate(-startSelX, -startSelY, 0);
            GL.Disable(EnableCap.Blend);
            //Render Selected Ability

            GL.Disable(EnableCap.ScissorTest);

        }

        public void unloadScene() {

            GL.DeleteTexture(_abilityBarTextureId);
            GL.DeleteTexture(_abilitySelectedTextureId);

        }

        /// <summary>
        /// Disable Depth Rendering to draw 2D UIs
        /// </summary>
        private void switchToOrthoRendering()
        {
            GL.Disable(EnableCap.DepthTest);
            GL.MatrixMode(MatrixMode.Projection);
            //GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(0, Engine.Width, Engine.Height, 0, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Disable(EnableCap.CullFace);

            GL.Enable(EnableCap.Blend);

            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.DepthMask(false);
        }

        /// <summary>
        /// Enable Depth Rendering again
        /// </summary>
        private void switchBackToFrustrumRendering()
        {
            GL.Enable(EnableCap.DepthTest);
            Engine.forceResize();
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.DepthMask(true);
            GL.Disable(EnableCap.Blend);
        }
    }
}
