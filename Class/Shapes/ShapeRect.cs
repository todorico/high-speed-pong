using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroPong.Class.Colliders;

namespace RetroPong.Class.Shapes
{
    public class ShapeRect : Shape
    {
        #region ACCESSORS
        public Vector2 Size
        {
            get
            {
                return new Vector2(TextureRect.Width, TextureRect.Height);
            }
            set
            {
                TextureRect = new Rectangle((int)Position.X, (int)Position.Y, (int)value.X, (int)value.Y);
            }
        }

        public float Width
        {
            get
            {
                return TextureRect.Width;
            }
            set
            {
                TextureRect = new Rectangle((int)Position.X, (int)Position.Y, (int)value, TextureRect.Height);
            }
        }

        public float Height
        {
            get
            {
                return TextureRect.Height;
            }
            set
            {
                TextureRect = new Rectangle((int)Position.X, (int)Position.Y, TextureRect.Width, (int)value);
            }
        }
        #endregion

        #region CONSTRUCTORS
        public ShapeRect(Vector2 size)
        {
            TextureRect = new Rectangle(Vector2.Zero.ToPoint(), Size.ToPoint());
            Collider = new ColliderRect(0, 0, Size.X, Size.Y);
        }

        public ShapeRect(int width, int height)
        {
            TextureRect = new Rectangle(0, 0, width, height);
            Collider = new ColliderRect(0, 0, Size.X, Size.Y);
        }
        #endregion

        #region METHODS
        protected override void InitTexture(SpriteBatch spriteBatch)
        {
            if (Texture == null)
            {
                Texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                Texture.SetData<Color>(new Color[] { Color.White });
            }
        }
        #endregion
    }
}
