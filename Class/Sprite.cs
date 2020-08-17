using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroPong.Class.Colliders;

namespace RetroPong.Class
{
    public class Sprite : Drawable
    {

        private Collider _collider;

        #region PROPERTIES
        public Texture2D Texture { get; set; }
        public Rectangle TextureRect { get; set; }
        #endregion

        #region ACCESSORS
        public Rectangle LocalBounds { get { return TextureRect; } }
        public Rectangle GlobalBounds { get { return new Rectangle((int)Position.X, (int)Position.Y, TextureRect.Width, TextureRect.Height); } }
        public Collider Collider
        {
            get
            {
                _collider.Position = Position;
                return _collider;
            }

            set
            {
                _collider = value;
            }
        }
        #endregion

        #region CONSTRUCTORS
        public Sprite()
        {

        }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            TextureRect = texture.Bounds;
            Collider = new ColliderRect(texture.Bounds);
        }

        public Sprite(Texture2D texture, Rectangle textureRect)
        {
            Texture = texture;
            TextureRect = textureRect;
            Collider = new ColliderRect(textureRect);
        }
        
        public Sprite(Texture2D texture, Rectangle textureRect, ColliderCircle collider)
        {
            Texture = texture;
            TextureRect = textureRect;
            Collider = collider;
        }

        public Sprite(Texture2D texture, Rectangle textureRect, ColliderRect collider)
        {
            Texture = texture;
            TextureRect = textureRect;
            Collider = collider;
        }
        #endregion

        #region METHODS
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position + Origin, TextureRect, Color, (float)Math.PI * Rotation / 180.0f, Origin, Scale, SpriteEffects.None, 0);
        }
        #endregion
    }
}
