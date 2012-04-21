using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DynaStudios.Blocks;

namespace LudumDare23
{
    public class WorldScene : IScene
    {
        public Engine Engine { get; set; }
        //private Chunklet chunklet1;
        private Room _room;
        private Player player;

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
            Console.WriteLine("preRoom");
            FileStream stream = new FileInfo("FirstTest.map").OpenRead;
            room = new Room(stream, Engine.TextureManager);
            Console.WriteLine("postRoom");
            //chunklet1 = new Chunklet(0, 0, 0);

        }

        public void doRender()
        {
            player.doMovement();

            room.render();
            //chunklet1.render(camera);
        }

        public void unloadScene() { }
    }
}
