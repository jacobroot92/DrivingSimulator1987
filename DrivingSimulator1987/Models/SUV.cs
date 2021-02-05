using DrivingSimulator1987.Enums;

namespace DrivingSimulator1987.Models
{
    public class SUV : Vehicle
    {
        public SUV()
        {
            CurrentMovement = VehicleMovement.MovingForward;
            VehicleName = "SUV";
        }

        public override string StopVehicle()
        {
            if(CurrentMovement == VehicleMovement.MovingForward)
            {
                CurrentMovement = VehicleMovement.Stopped;
                return VehicleName + " has run over a red Ford Pinto... Oh no! Both the Pinto and SUV erupted in flames.";
            }
            else
            {
                return VehicleName + " can only stop after moving forward.";
            }
        }
    }
}
