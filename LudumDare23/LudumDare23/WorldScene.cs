using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DynaStudios.Blocks;
using DynaStudios;
using OpenTK.Graphics.OpenGL;

namespace LudumDare23
{
    public class WorldScene : IScene
    {
        public Engine Engine { get; set; }
        //private Room _room;
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
            player.Position.z = -3.0;
            Engine.Camera.WorldObject = (IWorldObject) player;
            //_room = new Room(Path.Combine(DynaStudios.Utils.StreamTool.DIR, "Maps", "map.xml"), Engine.TextureManager);

            _abilityBarTextureId = Engine.TextureManager.getTexture(@"Images\Game\abilityBar.png");

        }

        public void doRender()
        {
            player.doMovement();

            //_room.render();
        
            //Render GUI
            renderGui();

        }

        private void renderGui()
        {
            //Render Ability Bar
            GL.LoadIdentity();
            GL.BindTexture(TextureTarget.Texture2D, _abilityBarTextureId);

            //Ability Bar Vars
            int texW = 800;
            int texH = 80;

            float translateX = ((Engine.Width / 2) - (texW / 2)) / Engine.Width;

            GL.Translate(translateX, 0, 0);

            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0, 1); GL.Vertex3(-0.78f, -0.104f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(0.78f, -0.104f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(0.78f, 0.104f, 1.0f);
            GL.TexCoord2(0, 0); GL.Vertex3(-0.78f, 0.104f, 1.0f);

            GL.End();

            //Render Ability Icons

            //Render Selected Ability

        }

        public void unloadScene() {

            GL.DeleteTexture(_abilityBarTextureId);

        }
    }
}
