using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroPong.Class.Shapes;
using Microsoft.Xna.Framework.Input;

namespace RetroPong.Class
{
    public class Snake
    {
        private GraphicsDevice graphicsDevice;
        private LinkedList<ShapeRect> snake;
        public static readonly TimeSpan intervalBetweenMoves = TimeSpan.FromMilliseconds(75);
        public TimeSpan lastMove;
        public TimeSpan lastGrow;

        public ShapeRect Head { get { return snake.First.Value; } }
        public ShapeRect Tale { get { return snake.Last.Value; } }
        public Vector2 Direction { get; set; }

        public static Vector2 Up { get { return new Vector2(0, -1); } }
        public static Vector2 Down { get { return new Vector2(0, 1); } }
        public static Vector2 Left { get { return new Vector2(-1, 0); } }
        public static Vector2 Right { get { return new Vector2(1, 0); } }

        public Snake(GraphicsDevice graphicsDevice, Vector2 headPosition)
        {
            Direction = Left;
            this.graphicsDevice = graphicsDevice;
            snake = new LinkedList<ShapeRect>();
            int k = 2;
            for(int i = 0; i < 3; i++)
            {
                snake.AddFirst(new ShapeRect(30, 30));
                snake.First.Value.Position = headPosition + new Vector2(30, 0) * k;
                k--;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(ShapeRect s in snake)
            {
                if (s != Head && snake.Count > 3 && Head.Collider.Intersects(s.Collider))
                {
                    ShapeRect oldHead = Head;
                    snake = new LinkedList<ShapeRect>();
                    int k = 2;
                    for (int i = 0; i < 3; i++)
                    {
                        snake.AddFirst(new ShapeRect(30, 30));
                        snake.First.Value.Position = oldHead.Position + new Vector2(30, 0) * k;
                        k--;
                    }

                }
            }

            Control(gameTime);

            if (lastMove + intervalBetweenMoves < gameTime.TotalGameTime)
            {
                Move();
                lastMove = gameTime.TotalGameTime;
            }


        }

        public void Move()
        {
            ShapeRect oldHead = Head;
            snake.AddFirst(new ShapeRect(30, 30));
            Head.Position = oldHead.Position + new Vector2(30, 30) * Direction;

            snake.RemoveLast();
        }

        public void Grow()
        {
            ShapeRect oldTale = Tale;
            snake.AddLast(new ShapeRect(30, 30));
            Tale.Position = oldTale.Position;
        }

        public void Control(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Direction = Left;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Direction = Right;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Direction = Up;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Direction = Down;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                if (lastGrow + intervalBetweenMoves < gameTime.TotalGameTime)
                {
                    Grow();
                    lastGrow = gameTime.TotalGameTime;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(ShapeRect s in snake)
            {
                s.Draw(spriteBatch);
            }
        }
    }
}
