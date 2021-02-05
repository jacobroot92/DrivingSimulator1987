using DrivingSimulator1987.Enums;

namespace DrivingSimulator1987.Models
{
    public class Vehicle
    {
        protected VehicleMovement CurrentMovement;
        protected string VehicleName = "Vehicle";

        public Vehicle() 
        {
            CurrentMovement = VehicleMovement.MovingForward;
        }

        public virtual string GetVehicleName()
        {
            return VehicleName;
        }

        public virtual VehicleMovement GetCurrentMovement()
        {
            return CurrentMovement;
        }

        public virtual string MoveVehicleForward()
        {
            if (CurrentMovement == VehicleMovement.MovingForward)
                return VehicleName + " is already moving forward! Invalid input.";
            else
                CurrentMovement = VehicleMovement.MovingForward;
                return VehicleName + " started moving forward";
        }

        public virtual string TurnVehicleLeft(TrafficLightSignal currentSignal)
        {
            if (currentSignal == TrafficLightSignal.LeftTurnGreen)
            {
                CurrentMovement = VehicleMovement.TurningLeft;
                return VehicleName + " turned left.";
            }
            else
            {
                return VehicleName + "can only turn left on a Left Turn Green signal.";
            }
        }

        public virtual string TurnVehicleRight()
        {
            CurrentMovement = VehicleMovement.TurningRight;
            return VehicleName + " turned right.";
        }

        public virtual string StopVehicle()
        {
            CurrentMovement = VehicleMovement.Stopped;
            return VehicleName + " has stopped normally.";
        }
    }
}
