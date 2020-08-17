using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroPong.Class;
using RetroPong.Class.Shapes;
using RetroPong.Class.Colliders;
using RetroPong.Class.Debugs;

namespace RetroPong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 600;


        Bar bar1;
        Bar bar2;
        Ball ball;

        Ray2D up = new Ray2D(new Vector2(-10, 0), new Vector2(WINDOW_WIDTH + 10, 0));
        Ray2D left = new Ray2D(new Vector2(0, -10), new Vector2(0, WINDOW_HEIGHT + 10));

        Ray2D right = new Ray2D(new Vector2(WINDOW_WIDTH, -10), new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT + 10));
        Ray2D down = new Ray2D(new Vector2(-10, WINDOW_HEIGHT), new Vector2(WINDOW_WIDTH + 10, WINDOW_HEIGHT));

        List<Collider> colliders = new List<Collider>();

        CollisionManager collisionManager;

        Sprite spriteBackground;
        SpriteFont spriteFont;
        Texture2D pongObjects;

        Texture2D Line;

        ShapeRect R;
        ShapeCircle C;
        Text text;
        Text mouseText;
        Color col = Color.Black;
        Delay delay = new Delay(0.1f);
        Snake snake;

        Vector2 mousePosition = new Vector2(0, 0);
        Ray2D rayTemp = new Ray2D();

        Ray2D pointer = new Ray2D(new Vector2(400, 300), new Vector2(1, 1), 1000);
        Ray2D testLine = new Ray2D(new Vector2(10, 550), new Vector2(630, 40));

        Ray2D ballRay = new Ray2D();
        Ray2D reflectionLine = new Ray2D(new Vector2(), new Vector2());
        Ray2D reflection = new Ray2D(new Vector2(), new Vector2());
        Ray2D ray = new Ray2D(new Vector2(), new Vector2());
        RaycastHit hit = new RaycastHit();

        List<Ray2D> rays = new List<Ray2D>();
        List<RaycastHit> path = new List<RaycastHit>();
        Ray2D bar1Ray;
        Ray2D bar2Ray;


        bool creatingRay = false;

        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            rays.Add(up);
            rays.Add(left);
            rays.Add(right);
            rays.Add(down);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D background = Content.Load<Texture2D>("PongBackground");
            pongObjects = Content.Load<Texture2D>("PongObjects");
            
            Line = new Texture2D(GraphicsDevice, 1, 1);
            Line.SetData<Color>(new Color[] { Color.White });
            // TODO: use this.Content to load your game content here

            spriteBackground = new Sprite(background);

            bar1 = new Bar(new Vector2(10, 270), pongObjects, new Rectangle(0, 0, 16, 80), new Vector2(10, 10));
            bar2 = new Bar(new Vector2(774, 270), pongObjects, new Rectangle(0, 0, 16, 80), new Vector2(10, 10));
            ball = new Ball(new Vector2(400, 300), 20);

            bar1Ray = new Ray2D(bar1.Position + new Vector2(16, 0), Vector2.UnitY, 80);
            bar2Ray = new Ray2D(bar2.Position, Vector2.UnitY, 80);

            rays.Add(bar1Ray);
            rays.Add(bar2Ray);

            /*
            colliders.Add(bar1.Collider);
            colliders.Add(bar2.Collider);
            */
            collisionManager = new CollisionManager(rays, ball);
            //ball = new Ball(new Vector2(400, 12), pongObjects, , new Circle(8, 8, 8), new Vector2(-5, 5));

            R = new ShapeRect( 30, 60);
            R.Position = new Vector2(500, 500);
            R.Origin = R.Size / 2;

            C = new ShapeCircle(100);
            C.Position = new Vector2(400, 300);

            spriteFont = Content.Load<SpriteFont>("Arial");
            text = new Text(spriteFont, "tubuluuuuuuu !!!!");
            text.Position = new Vector2(50, 10);

            mouseText = new Text(spriteFont);
            mouseText.Position = new Vector2(600, 10);
            text.Color = Color.Red;


            snake = new Snake(GraphicsDevice, new Vector2(300, 300));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                        mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);


            ball.Speed = 1000;
            bar1.Position = new Vector2(bar1.Position.X, mousePosition.Y);
            bar2.Position = new Vector2(bar2.Position.X, mousePosition.Y);

            bar1Ray.Position = bar1.Position + new Vector2(16, 0);
            bar2Ray.Position = bar2.Position;


            pointer.Length = 800;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !creatingRay)
            {
                rayTemp.StartPos = mousePosition;
                creatingRay = true;
            }
            else if (!creatingRay)
            {
                rayTemp.StartPos = mousePosition;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released && creatingRay)
            {
                Ray2D newRay = new Ray2D(rayTemp.StartPos, rayTemp.EndPos);

                    if(newRay.Length > 0)
                       rays.Add(newRay);

                creatingRay = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                pointer.StartPos = new Vector2(400, 300);
            }

            rayTemp.EndPos = mousePosition;

            pointer.Direction = Vector2.Normalize(mousePosition - pointer.StartPos);

            mouseText.String = "" + ball.Position;
            
            
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ball.Position = new Vector2(400, 300);
                ball.Direction = new Vector2(-1, 0);
            }
            

            collisionManager.Update(gameTime);
            //bar1.Control();
            //bar2.Control();


            /*
            Bar.Control(R);
            Bar.Control(text);*/
            Bar.Control(ball);
            snake.Update(gameTime);



            R.Color = Color.Green;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();


            spriteBackground.Draw(spriteBatch);
           // R.Draw(spriteBatch);
           // C.Draw(spriteBatch);

            ball.Draw(spriteBatch);

            Debug.DrawLine(spriteBatch, rayTemp);

            foreach (Ray2D r in rays)
            {
                Debug.DrawLine(spriteBatch, r);
            }

            if (collisionManager.BallPath.Count <= 1)
                Debug.DrawLine(spriteBatch, collisionManager.BallRay);
            else
                Debug.DrawPath(spriteBatch, collisionManager.BallPath);

            mouseText.Draw(spriteBatch);
            text.Draw(spriteBatch);
            bar1.Draw(spriteBatch);
            bar2.Draw(spriteBatch);
           // snake.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        /*
        public void Drawline(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(Line, new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1), null, Color.Red, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public void DrawPath(SpriteBatch spriteBatch, List<RaycastHit> path, bool drawNormal = true)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (i < path.Count - 1)
                    Drawline(spriteBatch, new Ray2D(path[i].Point, path[i + 1].Point));

                if (drawNormal)
                {
                    Drawline(spriteBatch, path[i].Point, path[i].Point + path[i].Normal * 20, Color.Green);
                }
            }
        }

        public void Drawline(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(Line, new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1), null, color, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public void Drawline(SpriteBatch spriteBatch, Ray2D line)
        {
            Drawline(spriteBatch, line.StartPos, line.EndPos);
        }

        public void Drawline(SpriteBatch spriteBatch, Ray2D line, Color color)
        {
            Drawline(spriteBatch, line.StartPos, line.EndPos, color);
        }
        */
        public Vector2 Reflection(Vector2 vector, Vector2 normal)
        {
            return vector - (2 * Vector2.Dot(vector, normal) * normal);
        }

        public static bool Intersects(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersection)
        {
            intersection = Vector2.Zero;

            Vector2 b = a2 - a1;
            Vector2 d = b2 - b1;

            float bDotDPerp = b.X * d.Y - b.Y * d.X;

            // if b dot d == 0, it means the lines are parallel so have infinite intersection points
            if (bDotDPerp == 0)
                return false;

            Vector2 c = b1 - a1;
            float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
            if (t < 0 || t > 1)
                return false;

            float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
            if (u < 0 || u > 1)
                return false;

            intersection = a1 + t * b;

            return true;
        }

        public static void ControlPos(Ray2D ray, float moveSpeed = 10)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                ray.StartPos += new Vector2(-moveSpeed, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                ray.StartPos += new Vector2(moveSpeed, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                ray.StartPos += new Vector2(0, -moveSpeed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                ray.StartPos += new Vector2(0, moveSpeed);
            }
        }
    }
}
