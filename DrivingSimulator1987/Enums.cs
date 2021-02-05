using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingSimulator1987.Enums
{
    public enum VehicleMovement
    {
        MovingForward,
        TurningLeft,
        TurningRight,
        Stopped
    }

    public enum TrafficLightSignal
    {
        Green,
        Yellow,
        Red,
        LeftTurnGreen
    }

    public enum Directions
    {
        Forward,
        Left,
        Right,
        Blockage
    }

    public enum VehicleType
    {
        SemiTruck,
        SUV
    }
}
