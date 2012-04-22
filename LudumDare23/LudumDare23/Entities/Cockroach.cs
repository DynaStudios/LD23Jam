using System.IO;
using DynaStudios.Blocks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace LudumDare23.Entities
{
    public class Cockroach : Enemy
    {

        private int _cockroachTexture;

        public Cockroach(WorldScene worldScene, Vector3 vector3)
        {
            Scene = worldScene;

            Position.x = vector3.X;
            Position.y = vector3.Y;
            Position.z = vector3.Z;

            base.Health = 10;
            base.Damage = 2;
        }


    }
}
