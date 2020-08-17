using System;
using Microsoft.Xna.Framework;

namespace RetroPong.Class.Colliders
{
    public class RaycastHit
    {
        public Vector2 Point { get; set; }
        public float Distance { get; set; }
        public Vector2 Normal { get; set; }
        public Ray2D Collider { get; set; }
        public Vector2 Direction { get; set; }

        public RaycastHit()
        {
            Point = new Vector2(0, 0);
            Distance = 0;
            Collider = null;
            Normal = new Vector2(0, 0);
        }

        public RaycastHit(Vector2 point, float distance = 0, Collider collider = null)
        {
            Point = point;
            Distance = distance;
            Collider = null;
            Normal = new Vector2(0, 0);
        }

        public RaycastHit(Vector2 point, Vector2 normal, float distance = 0, Collider collider = null)
        {
            Point = point;
            Distance = distance;
            Collider = null;
            Normal = normal;
        }
    }
}
