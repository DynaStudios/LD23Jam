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
        public Engine Engine { get; set; }
        private Room _room;
        private Player player;

        private int _abilityBarTextureId;

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
            Engine.Camera.WorldObject = (IWorldObject) player;
            //_room = new Room(Path.Combine(DynaStudios.Utils.StreamTool.DIR, "Maps", "map.xml"), Engine.TextureManager);

            _abilityBarTextureId = Engine.TextureManager.getTexture(Path.Combine("Images", "Game", "abilityBar.png"));
            string roomFilePath = Path.Combine(StreamTool.DIR, "Maps", "Room.xml");
            _room = new Room(roomFilePath, Engine.TextureManager);

            Engine.forceResize();
        }

        public void doRender()
        {

            switchBackToFrustrumRendering();
			
			/*if (player.Position.x<0.0) player.Position.x+=0.005;
			else                       player.Position.x-=0.005;
			if (player.Position.y<0.0) player.Position.y+=0.005;
			else                       player.Position.y-=0.005;
			if (player.Direction.x<0.0) player.Direction.X+=0.005;
			else                        player.Direction.X-=0.005;
			if (player.Direction.y<0.0) player.Direction.Y+=0.005;
			else                        player.Direction.Y-=0.005;*/
			
            player.doMovement();
			
			//translate and rotate into the player's view
			/*GL.LoadIdentity();
			GL.Translate(-player.Position.x, -player.Position.y, -player.Position.z);
			GL.Rotate (-player.Direction.X,1.0,0.0,0.0);
			GL.Rotate (-player.Direction.Y,0.0,1.0,0.0);*/
			


            _room.render();
        
            //Render GUI
            switchToOrthoRendering();
            renderGui();

        }

        private void renderGui()
        {
            //switchToOrthoRendering();

            //Render Ability Bar
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.BindTexture(TextureTarget.Texture2D, _abilityBarTextureId);

            //Ability Bar Vars
            int texW = 800;
            int texH = 80;

            float translateX = (((float)Engine.Width / 2) - (texW / 2)) / (float)Engine.Width;
            float translateY = ((float)Engine.Height - texH) / (float)Engine.Height;

            //GL.Translate(translateX, translateY, -5);

            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0, 1); GL.Vertex2(-0.78f, -0.104f);
            GL.TexCoord2(1, 1); GL.Vertex2(0.78f, -0.104f);
            GL.TexCoord2(1, 0); GL.Vertex2(0.78f, 0.104f);
            GL.TexCoord2(0, 0); GL.Vertex2(-0.78f, 0.104f);

            GL.End();

            //Render Ability Icons

            //Render Selected Ability

            //switchBackToFrustrumRendering();

        }

        public void unloadScene() {

            GL.DeleteTexture(_abilityBarTextureId);

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
            GL.Disable(EnableCap.Blend);
        }
    }
}
