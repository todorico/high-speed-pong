using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using RetroPong.Class.Colliders;

namespace RetroPong.Class
{
    public class CollisionManager
    {


        private Ball _ball;
        private List<Ray2D> _colliders;
        private List<RaycastHit> _ballPath;

        public Ball Ball
        {
            get
            {
                return _ball;
            }

            set
            {
                _ball = value;
            }
        }

        public List<Ray2D> Colliders
        {
            get
            {
                return _colliders;
            }

            set
            {
                _colliders = value;
            }
        }

        public List<RaycastHit> BallPath
        {
            get
            {
                return _ballPath;
            }

            set
            {
                _ballPath = value;
            }
        }

        public Ray2D BallRay
        {
            get
            {
                return new Ray2D(Ball.Position, Ball.Direction, Ball.Speed);
            }
        }

        public CollisionManager(List<Ray2D> colliders, Ball ball)
        {
            Colliders = colliders;
            Ball = ball;
            BallPath = new List<RaycastHit>();
        }

        public void Update(GameTime gameTime)
        {
            if (Collider.BounceAll(BallRay, _colliders, out _ballPath))
            {
                if (!Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    _ball.Position = _ballPath[_ballPath.Count - 1].Point;
                    _ball.Direction = _ballPath[_ballPath.Count - 1].Direction;
                }
            }
            else
            {
                if (!Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    _ball.Move(_ball.Direction * _ball.Speed);
                }
            }
        }

        public void DrawColliders(SpriteBatch spriteBatch)
        {
            
        }

        public void DrawBallPath(SpriteBatch spriteBatch)
        {

        }
    }
}
