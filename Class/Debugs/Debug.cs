using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroPong.Class.Colliders;

namespace RetroPong.Class.Debugs
{
    public class Debug
    {
        public static Texture2D texture;

        protected static void InitTexture(SpriteBatch spriteBatch)
        {
            if (texture == null)
            {
                texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                texture.SetData<Color>(new Color[] { Color.White });
            }
        }
        #region DRAWLINE
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            InitTexture(spriteBatch);

            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(texture, new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1), null, Color.Red, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            InitTexture(spriteBatch);

            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(texture, new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1), null, color, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Ray2D line)
        {
            DrawLine(spriteBatch, line.StartPos, line.EndPos);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Ray2D line, Color color)
        {
            DrawLine(spriteBatch, line.StartPos, line.EndPos, color);
        }
        #endregion

        public static void DrawPath(SpriteBatch spriteBatch, List<RaycastHit> path, bool drawNormal = true)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (i < path.Count - 1)
                    DrawLine(spriteBatch, new Ray2D(path[i].Point, path[i + 1].Point));

                if (drawNormal)
                {
                    DrawLine(spriteBatch, path[i].Point, path[i].Point + path[i].Normal * 20, Color.Green);
                }
            }
        }
    }
}
