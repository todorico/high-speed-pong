using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroPong.Class
{
    public abstract class Drawable : Transformable
    {

        #region PROPERTIES
        public Color Color { get; set; }
        #endregion

        #region CONSTRUCTORS
        public Drawable()
        {
            Color = Color.White;
        }
        #endregion

        #region METHODS
        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion

    }
}
