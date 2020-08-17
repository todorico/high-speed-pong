using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RetroPong.Class
{
    public class Bar : Sprite
    {
        private Vector2 _speed;
        private bool lastTouched;

        #region ACCESSORS
        public Vector2 Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }

        public bool LastTouched
        {
            get
            {
                return lastTouched;
            }

            set
            {
                lastTouched = value;
            }
        }
        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// The constructor of a bar. This is the element that a player can control.
        /// </summary>
        /// <param name="width">The width of the bar.</param>
        /// <param name="heigth">The height of the bar.</param>
        /// <param name="position">The vector of the position of the bar.</param>
        /// <param name="velocity">The velocity of the bar.</param>
        /// <param name="texture">The texture of the bar.</param>
        /// <param name="collider">The hitbox of the bar.</param>
        public Bar(Vector2 position, Texture2D texture, Rectangle textureRect, Vector2 speed) : base(texture, textureRect)
        {
            Position = position;
            _speed = speed;
        }
        #endregion

        #region METHODS
        public void Control()
        {
            Control(this);
        }

        public static void Control(Transformable transform, float moveSpeed = 10, float rotationAngle = 10, float scaleSpeed = 0.1f)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                transform.Move(-moveSpeed, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                transform.Move(moveSpeed, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                transform.Move(0, -moveSpeed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                transform.Move(0, moveSpeed);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                transform.Rotate(rotationAngle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                transform.Rotate(-rotationAngle);
            }
            /*
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
            {
                transform.Scaling(scaleSpeed, scaleSpeed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
            {
                transform.Scaling(-scaleSpeed, -scaleSpeed);
            }
            */
        }
        #endregion
    }
}
