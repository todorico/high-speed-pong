using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RetroPong.Class.Colliders
{
    public class ColliderRect : Collider
    {
        #region PROPERTIES
        public float Width { get; set; }
        public float Height { get; set; }
        #endregion

        #region ACCESSORS
        public int Top
        {
            get
            {
                return (int)Position.Y;
            }
        }

        public int Bottom
        {
            get
            {
                return (int)Position.Y + (int)Height;
            }
        }

        public int Left
        {
            get
            {
                return (int)Position.X;
            }
        }

        public int Right
        {
            get
            {
                return (int)Position.X + (int)Width;
            }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Constructor of a ColliderRect.
        /// </summary>
        public ColliderRect() : base()
        {
            Width = 0;
            Height = 0;
        }

        /// <summary>
        /// Constructor of a ColliderRect.
        /// </summary>
        /// <param name="rectangle">Rectangle of a ColliderRect.</param>
        public ColliderRect(Rectangle rectangle) : base(rectangle.X, rectangle.Y)
        {
            Width = rectangle.Width;
            Height = rectangle.Height;
        }

        /// <summary>
        /// Constructor of a ColliderRect.
        /// </summary>
        /// <param name="x">Position x of a ColliderRect.</param>
        /// <param name="y">Position y of a ColliderRect.</param>
        /// <param name="width">Width of a ColliderRect.</param>
        /// <param name="height">Height of a ColliderRect.</param>
        public ColliderRect(float x, float y, float width, float height) : base(x, y)
        {
            Width = width;
            Height = height;
        }
        #endregion

        #region METHODS
        public override bool Contains(Vector2 point)
        {
            return point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom; 
        }

        #region INTERSECTIONS
        public override bool Intersects(Collider other)
        {
            return other.Intersects(this);
        }

        public override bool Intersects(ColliderRect other)
        {
            return Collision(this, other);
        }

        public override bool Intersects(ColliderCircle other)
        {
            return Collision(this, other);
        }

        public override bool Intersects(Ray2D ray)
        {
            List<Ray2D> rays = new List<Ray2D>(4);

            Ray2D up = new Ray2D(Position, Position + new Vector2(Width, 0));
            Ray2D left = new Ray2D(Position, Position + new Vector2(0, Height));
            Ray2D right = new Ray2D(Position + new Vector2(Width, Height), Position + new Vector2(Width, 0));
            Ray2D down = new Ray2D(Position + new Vector2(Width, Height), Position + new Vector2(0, Height));

            return Raycast(ray, up) || Raycast(ray, left) || Raycast(ray, down) || Raycast(ray, right);
        }

        public override bool Intersects(Ray2D ray, out RaycastHit hit)
        {
            List<Ray2D> rays = new List<Ray2D>(4);

            Ray2D up = new Ray2D(Position, Position + new Vector2(Width, 0));
            Ray2D left = new Ray2D(Position, Position + new Vector2(0, Height));
            Ray2D right = new Ray2D(Position + new Vector2(Width, Height), Position + new Vector2(Width, 0));
            Ray2D down = new Ray2D(Position + new Vector2(Width, Height), Position + new Vector2(0, Height));
            
            rays.Add(up); rays.Add(left); rays.Add(right);  rays.Add(down);

            return ClosestHit(ray, rays, out hit);
        }
        #endregion

        #endregion
    }
}
