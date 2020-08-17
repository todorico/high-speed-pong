using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroPong.Class.Shapes;

namespace RetroPong.Class
{
    public class Ball : ShapeCircle
    {
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public float Acceleration { get; set; }

        #region ACCESSORS

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// The constructor of a ball.
        /// </summary>
        public Ball(Vector2 position, float radius) : base(radius)
        {
            Position = position;
            Speed = 1;
            Acceleration = 0;
            Direction = Vector2.Normalize (new Vector2(-1, 1));
        }
        #endregion

        #region METHODS
        public void Update()
        {
            Position += Direction * Speed;
        }
        #endregion
    }
}