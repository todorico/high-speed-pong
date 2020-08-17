using System;
using Microsoft.Xna.Framework;

namespace RetroPong.Class.Colliders
{
    public class ColliderCircle : Collider
    {
        #region PROPERTIES
        public float Radius { get; set; }
        #endregion
        
        #region CONSTRUCTORS
        /// <summary>
        /// Constructor of a ColliderEllipse.
        /// </summary>
        public ColliderCircle() : base()
        {
            Radius = 0;
        }

        /// <summary>
        /// Constructor of a ColliderEllipse.
        /// </summary>
        /// <param name="position">Position of a ColliderEllipse.</param>
        /// <param name="radius">Radius of a ColliderEllipse.</param>
        public ColliderCircle(Vector2 position, float radius) : base(position)
        {
            Radius = radius;
        }

        /// <summary>
        /// Constructor of a ColliderEllipse.
        /// </summary>
        /// <param name="x">Position x of a ColliderEllipse.</param>
        /// <param name="y">Position y of a ColliderEllipse.</param>
        /// <param name="radius">Radius of a ColliderEllipse.</param>
        public ColliderCircle(float x, float y, float radius) : base(x, y)
        {
            Radius = radius;
        }
        #endregion

        #region METHODS
        public override bool Contains(Vector2 point)
        {
            return Math.Abs(Position.X - point.X) < Radius && Math.Abs(Position.Y - point.Y) < Radius;
        }

        public override bool Intersects(Collider other)
        {
            return other.Intersects(this);
        }

        public override bool Intersects(ColliderRect other)
        {
            return Collision(other, this);
        }

        public override bool Intersects(ColliderCircle other)
        {
            return Collision(this, other); ;
        }

        public override bool Intersects(Ray2D ray)
        {
            float x1 = ray.StartPos.X - X;
            float y1 = ray.StartPos.Y - Y;
            float x2 = ray.EndPos.X - X;
            float y2 = ray.EndPos.Y - Y;

            float dx = x2 - x1;
            float dy = y2 - y1;

            float dirSquared = dx * dx + dy * dy;

            float dist = x1 * y2 - x2 * y1;

            return Radius * dirSquared > dist * dist;
        }

        public override bool Intersects(Ray2D ray, out RaycastHit hit)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}