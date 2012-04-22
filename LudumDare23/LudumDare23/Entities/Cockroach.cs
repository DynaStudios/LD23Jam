using System.IO;
using DynaStudios.Blocks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace LudumDare23.Entities
{
    public class Cockroach : Enemy
    {

        private int _cockroachTexture;
        private WorldScene worldScene;

        public Cockroach(WorldScene scene)
        {
            Scene = scene;
            _cockroachTexture = scene.Engine.TextureManager.getTexture(Path.Combine("Images", "Game", "menuselection.png"));
        }

        public Cockroach(WorldScene worldScene, Vector3 vector3)
        {
            this.worldScene = worldScene;

            Position.x = vector3.X;
            Position.y = vector3.Y;
            Position.z = vector3.Z;
        }

        public override void doRender()
        {
            //Render Cockroach

            GL.BindTexture(TextureTarget.Texture2D, _cockroachTexture);
            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0, 1); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0, 0); GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.End();
        }
    }
}
