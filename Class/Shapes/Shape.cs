using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RetroPong.Class.Shapes
{
    public abstract class Shape : Sprite
    {
        #region PROPERTIES
        public Color FillColor { get; set; }
        public Color OutlineColor { get; set; }
        public float OutlineThickness { get; set; }
        #endregion

        #region CONSTRUCTORS
        public Shape()
        {
            FillColor = Color.White;
            OutlineColor = Color.Black;
            OutlineThickness = 0;
        }
        #endregion

        #region METHODS
        public override void Draw(SpriteBatch spriteBatch)
        {
            InitTexture(spriteBatch);
            base.Draw(spriteBatch);
        }

        protected abstract void InitTexture(SpriteBatch spriteBatch);
        #endregion
    }
}