using System;
using Microsoft.Xna.Framework;

namespace RetroPong.Class
{
    public class Delay
    {
        public float DelayTime { get; set; }
        public float RemainingTime { get; set; }

        public Delay(float delay)
        {
            DelayTime = delay;
            RemainingTime = delay;
        }

        public bool Update(GameTime gametime)
        {
            RemainingTime -= (float)gametime.ElapsedGameTime.TotalSeconds;

            return RemainingTime <= 0;
        }
    }
}
