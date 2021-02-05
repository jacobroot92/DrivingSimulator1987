using DrivingSimulator1987.Enums;

namespace DrivingSimulator1987.Models
{
    public class SemiTruck : Vehicle
    {
        public SemiTruck()
        {
            CurrentMovement = VehicleMovement.MovingForward;
            VehicleName = "Semi Truck";
        }

        public override string TurnVehicleLeft(TrafficLightSignal currentSignal)
        {
            if (currentSignal == TrafficLightSignal.LeftTurnGreen)
            {
                int result = CheckIfStoppedBeforeTurn(VehicleMovement.TurningLeft);
                if (result == 0)
                    return VehicleName + " turned left.";
                else
                    return VehicleName + " can only move forward after stopping! Invalid input.";
            }
            else
            {
                if (CurrentMovement == VehicleMovement.Stopped)
                    return VehicleName + " can only move forward after stopping! Invalid input.";
                else
                    return VehicleName + " can only turn left on a Left Turn Green signal.";
            }
        }

        public override string TurnVehicleRight()
        {
            int result = CheckIfStoppedBeforeTurn(VehicleMovement.TurningRight);

            if (result == 0)
                return VehicleName + " turned right.";
            else
                return VehicleName + " can only move forward after stopping! Invalid input.";
        }

        public override string StopVehicle()
        {
            CurrentMovement = VehicleMovement.Stopped;
            return VehicleName + " has jack knifed to a stop";
        }

        int CheckIfStoppedBeforeTurn(VehicleMovement turnDirection)
        {
            if (CurrentMovement == VehicleMovement.Stopped)
                return 1;
            else
                CurrentMovement = turnDirection;
            return 0;
        }
    }
}
