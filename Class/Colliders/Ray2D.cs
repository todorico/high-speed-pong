using System;
using Microsoft.Xna.Framework;

namespace RetroPong.Class.Colliders
{
    public class Ray2D : Collider
    {
        #region PROPERTIES
        public Vector2 StartPos { get { return Position; } set { Position = value; } }
        public Vector2 Direction { get; set; }
        public float Length { get; set; }
        #endregion

        #region ACCESSORS
        public Vector2 EndPos
        {
            get
            {
                return StartPos + Length * Direction;
            }
            set
            {
                Direction = Vector2.Normalize(value - StartPos);
                Length = Vector2.Distance(StartPos, value);
            }
        }

        public Vector2 Normal
        {
            get
            {
                return new Vector2(-Direction.Y, Direction.X);
            }
        }
        #endregion

        #region CONSTRUCTORS
        public Ray2D()
        {
            StartPos = new Vector2(0,0);
            Direction = new Vector2(1, 0);
            Length = 1;
        }

        public Ray2D(Vector2 startPos, Vector2 direction, float length = 0)
        {
            StartPos = startPos;
            Direction = Vector2.Normalize(direction);
            Length = length;
        }

        public Ray2D(Vector2 startPos, Vector2 endPos)
        {
            StartPos = startPos;
            Direction = Vector2.Normalize(endPos - startPos);
            Length = Vector2.Distance(startPos, endPos);
        }
        #endregion

        #region METHODS
        public override bool Contains(Vector2 point)
        {
            Vector2 directionToPoint = Vector2.Normalize(point - StartPos);
            return directionToPoint == Direction || directionToPoint == -Direction; 
        }

        #region INTERSECTIONS

        public override bool Intersects(Collider other)
        {
            return other.Intersects(this);
        }

        public override bool Intersects(ColliderRect other)
        {
            return other.Intersects(this);
        }

        public override bool Intersects(ColliderCircle other)
        {
            return other.Intersects(this);
        }

        public override bool Intersects(Ray2D ray)
        {
            return Raycast(this, ray);
        }

        public override bool Intersects(Ray2D ray, out RaycastHit hit)
        {
            return Raycast(this, ray, out hit);
        }
        #endregion
        
        #endregion
    }
}
