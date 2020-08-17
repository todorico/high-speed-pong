using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace RetroPong.Class.Colliders
{
    public abstract class Collider
    {

        public Vector2 Position { get; set; }

        #region ACCESSORS
        public float X { get { return Position.X; } }
        public float Y { get { return Position.Y; } }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Constructor of a Collider.
        /// </summary>
        public Collider()
        {
            Position = new Vector2(0, 0);
        }

        /// <summary>
        /// Constructor of a Collider.
        /// </summary>
        /// <param name="position">Position of a Collider.</param>
        public Collider(Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// Constructor of a Collider.
        /// </summary>
        /// <param name="x">Position x of a Collider.</param>
        /// <param name="y">Position y of a Collider.</param>
        public Collider(float x, float y)
        {
            Position = new Vector2(x, y);
        }
        #endregion

        #region METHODS
        public abstract bool Contains(Vector2 point);

        #region INTERSECTIONS

        public abstract bool Intersects(Collider other);

        public abstract bool Intersects(ColliderRect other);

        public abstract bool Intersects(ColliderCircle other);
        
        public abstract bool Intersects(Ray2D other);

        public abstract bool Intersects(Ray2D ray, out RaycastHit hit);

        //public abstract bool Intersects(Collider other, out RaycastHit hit);
        #endregion

        #region COLLISIONS
        public static bool Collision(ColliderRect A, ColliderRect B)
        {
            return !(B.X >= A.X + A.Width || B.X + B.Width <= A.X ||
                B.Y >= A.Y + A.Height || B.Y + B.Height <= A.Y);
        }

        public static bool Collision(ColliderCircle A, ColliderCircle B)
        {
            return Vector2.Distance(A.Position, B.Position) <= (A.Radius + B.Radius);
        }

        public static bool Collision(ColliderRect A, ColliderCircle B)
        {
            return !(B.X >= A.X + A.Width || B.X + B.Radius <= A.X ||
                B.Y >= A.Y + A.Height || B.Y + B.Radius <= A.Y);
        }
        #endregion

        #region RAYCASTS
        public static bool Raycast(Ray2D ray, Ray2D collider)
        {
            Vector2 b = ray.EndPos - ray.StartPos;
            Vector2 d = collider.EndPos - collider.StartPos;

            float bDotDPerp = b.X * d.Y - b.Y * d.X;

            // if b dot d == 0, it means the lines are parallel so have infinite intersection points
            if (bDotDPerp == 0)
                return false;

            Vector2 c = collider.StartPos - ray.StartPos;

            float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;

            if (t < 0 || t > 1)
                return false;

            float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;

            if (u < 0 || u > 1)
                return false;

            return true;
        }

        public static bool Raycast(Ray2D ray, Ray2D collider, out RaycastHit hit)
        {
            hit = new RaycastHit();
            hit.Point = ray.StartPos;
           
            Vector2 b = ray.EndPos - ray.StartPos;
            Vector2 d = collider.EndPos - collider.StartPos;

            float bDotDPerp = b.X * d.Y - b.Y * d.X;

            // if b dot d == 0, it means the lines are parallel so have infinite intersection points
            if (bDotDPerp == 0)
                return false;

            Vector2 c = collider.StartPos - ray.StartPos;

            float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;

            if (t < 0 || t > 1)
                return false;

            float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;

            if (u < 0 || u > 1)
                return false;

            hit.Point = ray.StartPos + t * b;
            hit.Distance = Vector2.Distance(ray.StartPos, hit.Point);
            hit.Normal = collider.Normal;
            hit.Collider = collider;
            hit.Direction = Vector2.Normalize(hit.Point - ray.StartPos);
            return true;
        }

        public static bool Raycast(Ray2D ray, Collider collider, out RaycastHit hit)
        {
            return collider.Intersects(ray, out hit);
        }

        public static bool RaycastAll(Ray2D ray, List<Ray2D> rays, out List<RaycastHit> hits)
        {
            RaycastHit closestHit;
            float Dist = 0;
            Ray2D reflectedRay;
            hits = new List<RaycastHit>();

            if (ClosestHit(ray, rays, out closestHit))
            {
                hits.Add(closestHit);
                Dist += closestHit.Distance;
                reflectedRay = new Ray2D(closestHit.Point, Vector2.Reflect(ray.Direction, closestHit.Normal), Vector2.Distance(closestHit.Point, ray.EndPos));
                
                while (Dist < ray.Length)
                {
                    if(ClosestHit(reflectedRay, rays, out closestHit, closestHit.Collider))
                    {
                        hits.Add(closestHit);
                        reflectedRay = new Ray2D(closestHit.Point, Vector2.Reflect(reflectedRay.Direction, closestHit.Normal), Vector2.Distance(closestHit.Point, reflectedRay.EndPos));
                        Dist += closestHit.Distance;
                    }
                    else
                    {
                        closestHit.Distance = ray.Length - Dist;
                        closestHit.Point = reflectedRay.StartPos + reflectedRay.Direction * reflectedRay.Length;
                        closestHit.Direction = reflectedRay.Direction;

                        hits.Add(closestHit);
                        Dist += ray.Length;

                    }

                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool BounceAll2(Ray2D ray, List<Ray2D> rays, out List<RaycastHit> path)
        {
            RaycastHit closestHit = new RaycastHit(ray.StartPos, ray.Direction);
            Ray2D reflectedRay = ray;
            bool touched = false;
            path = new List<RaycastHit>();
            path.Add(closestHit);
            float Dist = 0;

            while (ClosestHit(reflectedRay, rays, out closestHit, closestHit.Collider))
            {
                path.Add(closestHit);
                reflectedRay = new Ray2D(closestHit.Point, Vector2.Reflect(reflectedRay.Direction, closestHit.Normal), Vector2.Distance(closestHit.Point, reflectedRay.EndPos));
                touched = true;
                Dist += closestHit.Distance;

            }

            if (touched)
            {
                closestHit.Distance = ray.Length - Dist;
                closestHit.Point = reflectedRay.StartPos + reflectedRay.Direction * reflectedRay.Length;
                closestHit.Direction = reflectedRay.Direction;

                path.Add(closestHit);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool BounceAll(Ray2D ray, List<Ray2D> rays, out List<RaycastHit> path)
        {
            RaycastHit closestHit;
            float Dist = 0;
            Ray2D reflectedRay;
            path = new List<RaycastHit>();
            path.Add(new RaycastHit(ray.StartPos));

            if (ClosestHit(ray, rays, out closestHit))
            {
                path.Add(closestHit);
                Dist += closestHit.Distance;
                reflectedRay = new Ray2D(closestHit.Point, Vector2.Reflect(ray.Direction, closestHit.Normal), Vector2.Distance(closestHit.Point, ray.EndPos));

                while (Dist < ray.Length)
                {
                    if (ClosestHit(reflectedRay, rays, out closestHit, closestHit.Collider))
                    {
                        path.Add(closestHit);
                        reflectedRay = new Ray2D(closestHit.Point, Vector2.Reflect(reflectedRay.Direction, closestHit.Normal), Vector2.Distance(closestHit.Point, reflectedRay.EndPos));
                        Dist += closestHit.Distance;
                    }
                    else
                    {
                        closestHit.Distance = ray.Length - Dist;
                        closestHit.Point = reflectedRay.StartPos + reflectedRay.Direction * reflectedRay.Length;
                        closestHit.Direction = reflectedRay.Direction;

                        path.Add(closestHit);
                        Dist += ray.Length;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ClosestHit(Ray2D ray, List<Ray2D> rays, out RaycastHit closestHit, Ray2D ignoredRay = null)
        {
            RaycastHit hit;
            closestHit = new RaycastHit(ray.StartPos);

            bool touched = false;
            float dist = float.PositiveInfinity;

            foreach (Ray2D r in rays)
            {
                hit = new RaycastHit();
                if (r != ignoredRay && ray.Intersects(r, out hit))
                {
                    if (hit.Distance < dist)
                    {
                        closestHit = hit;
                        dist = hit.Distance;
                    }
                    touched = true;
                }
            }
            return touched;
        }

        public static bool ClosestHit(Ray2D ray, List<Collider> colliders, out RaycastHit closestHit, Collider ignoredCollider = null)
        {
            RaycastHit hit;
            closestHit = new RaycastHit(ray.StartPos);

            bool touched = false;
            float dist = float.PositiveInfinity;

            foreach (Collider c in colliders)
            {
                hit = new RaycastHit();
                if (c != ignoredCollider && c.Intersects(ray, out hit))
                {
                    if (hit.Distance < dist)
                    {
                        closestHit = hit;
                        dist = hit.Distance;
                    }
                    touched = true;
                }
            }
            return touched;
        }
        #endregion

        #endregion
    }
}
