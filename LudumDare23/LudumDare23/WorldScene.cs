using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LudumDare23
{
    public class WorldScene : IScene
    {
        public Engine Engine { get; set; }
        //private Chunklet chunklet1;

        private Player player;

        private Camera camera = new Camera();
        public Camera Camera
        {
            get { return camera; }
        }

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

            //chunklet1 = new Chunklet(0, 0, 0);

        }

        public void doRender()
        {
            player.doMovement();

            //moves the camera
            camera.move();

            //chunklet1.render(camera);

            // unmoves the camera for the next frame
            camera.moveBack();
        }

        public void unloadScene() { }
    }
}
