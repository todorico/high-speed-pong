using System;
using Microsoft.Xna.Framework;

namespace RetroPong.Class
{
    public abstract class Transformable
    {
        #region PROPERTIES
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }
        #endregion

        #region ACCESSORS
        public float X { get { return Position.X; } }
        public float Y { get { return Position.Y; } }
        #endregion

        #region CONSTRUCTORS
        public Transformable()
        {
            Position = new Vector2(0, 0);
            Scale = new Vector2(1, 1);
            Origin = new Vector2(0, 0);
            Rotation = 0;
        }

        public Transformable(Vector2 position)
        {
            Position = position;
            Scale = new Vector2(1, 1);
            Origin = new Vector2(0, 0);
            Rotation = 0;
        }

        public Transformable(Vector2 position, Vector2 scale, Vector2 origin, float rotation = 0)
        {
            Position = position;
            Scale = scale;
            Origin = origin;
            Rotation = rotation;
        }
        #endregion

        #region METHODS
        public void Move(float offSetX, float offSetY)
        {
            Position += new Vector2(offSetX, offSetY);
        }
 
        public void Move(Vector2 offset)
        {
            Position += offset;
        }

        public void Rotate(float angle)
        {
            Rotation += angle;
        }

        public void Scaling(float factorX, float factorY)
        {
            Scale += new Vector2(factorX, factorY);
        }

        public void Scaling(Vector2 factor)
        {
            Scale += factor;
        }
        #endregion
    }
}
