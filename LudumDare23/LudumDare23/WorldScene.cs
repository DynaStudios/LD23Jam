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
        //private Chunklet chunklet1;
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
            //chunklet1 = new Chunklet(0, 0, 0);

            _abilityBarTextureId = Engine.TextureManager.getTexture(@"Images\Game\abilityBar.png");

        }

        public void doRender()
        {
            player.doMovement();

            //_room.render();
            //chunklet1.render(camera);
        }

        private void renderGui()
        {
            //Render Ability Bar
            
            //Render Ability Icons

            //Render Selected Ability

        }

        public void unloadScene() {

            GL.DeleteTexture(_abilityBarTextureId);

        }
    }
}
