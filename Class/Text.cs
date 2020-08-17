using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroPong.Class.Colliders;

namespace RetroPong.Class
{
    public class Text : Drawable
    {
        #region PROPERTIES
        public SpriteFont SpriteFont { get; set; }
        public string String { get; set; }
        #endregion

        #region CONSTRUCTORS
        public Text(SpriteFont spriteFont)
        {
            SpriteFont = spriteFont;
            String = "";
        }

        public Text(SpriteFont spriteFont, string text)
        {
            SpriteFont = spriteFont;
            String = text;
        }
        #endregion

        #region METHODS
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(SpriteFont, String, Position + Origin, Color, Rotation, Origin, Scale, SpriteEffects.None, 0); 
        }

        public override string ToString()
        {
            return String;
        }
        #endregion
    }
}
