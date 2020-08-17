using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroPong.Class.Colliders;

namespace RetroPong.Class.Shapes
{
    public class ShapeCircle : Shape
    {
        public float Radius { get; set; }

        public ShapeCircle(float radius)
        {
            Radius = radius;
            TextureRect = new Rectangle(0, 0, (int)radius, (int)radius);
            Origin = new Vector2(radius, radius)/2;
            Collider = new ColliderCircle(0, 0, radius);
        }

        #region METHODS
        public override void Draw(SpriteBatch spriteBatch)
        {
            InitTexture(spriteBatch);
            spriteBatch.Draw(Texture, Position, TextureRect, Color, (float)Math.PI * Rotation / 180.0f, Origin, Scale, SpriteEffects.None, 0);
        }

        protected override void InitTexture(SpriteBatch spriteBatch)
        {
            if (Texture == null)
            {
                Texture = new Texture2D(spriteBatch.GraphicsDevice, (int)Radius, (int)Radius);
                Color[] colorData = new Color[(int)Radius * (int)Radius];

                float diam = Radius / 2f;
                float diamsq = diam * diam;

                for (int x = 0; x < Radius; x++)
                {
                    for (int y = 0; y < Radius; y++)
                    {
                        int index = x * (int)Radius + y;
                        Vector2 pos = new Vector2(x - diam, y - diam);
                        if (pos.LengthSquared() <= diamsq)
                        {
                            colorData[index] = Color.White;
                        }
                        else
                        {
                            colorData[index] = Color.Transparent;
                        }
                    }
                }

                Texture.SetData(colorData);
            }
        }
        #endregion
    }
}
