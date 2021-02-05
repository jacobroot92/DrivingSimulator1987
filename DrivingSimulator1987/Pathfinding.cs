using DrivingSimulator1987.Enums;
using System;

namespace DrivingSimulator1987
{
    public static class Pathfinding
    {
        public static Directions GetNextDirection()
        {
            Random rng = new Random();
            int nextDirection = rng.Next(3);

            switch (nextDirection)
            {
                case 0:
                    return Directions.Forward;
                case 1:
                    return Directions.Left;
                case 2:
                    return Directions.Right;
                default:
                    return Directions.Forward;
            }
        }
    }
}
