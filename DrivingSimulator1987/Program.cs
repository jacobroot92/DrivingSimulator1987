using System;
using DrivingSimulator1987.Models;
using DrivingSimulator1987.Enums;

namespace DrivingSimulator1987
{
    class Program
    {
        static Vehicle vehicle;
        static ConsoleKey currentInput;
        static StopLight signal = new StopLight();
        static Directions currentDirection = Directions.Right;
        static VehicleType vehicleType;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Driving Simulator 1987!");
            Console.WriteLine("Would you like to drive a [S]UV or a Semi [T]ruck?");
            ReadKey();

            while (currentInput != ConsoleKey.S && currentInput != ConsoleKey.T)
            {
                if (currentInput == ConsoleKey.S)
                    DriveSUV();
                else if (currentInput == ConsoleKey.T)
                    DriveTruck();
                else
                    InvalidInput();
            }

            if (currentInput == ConsoleKey.S)
                DriveSUV();
            else if (currentInput == ConsoleKey.T)
                DriveTruck();
        }

        private static void InvalidInput()
        {
            Console.WriteLine("Invalid input.");
            ReadKey();
        }

        private static void DriveSUV()
        {
            vehicleType = VehicleType.SUV;
            vehicle = new SUV();
            PrintIntro("SUV", "SUV", "[F]orward", "Turn [L]eft", "Turn [R]ight", "[S]top and run over a Ford Pinto");

            MoveSUVAndPrintResult();
            Console.WriteLine("Stop the vehicle when you feel you've reached your destination");

            while (vehicle.GetCurrentMovement() != VehicleMovement.Stopped)
            {
                Console.WriteLine("Remember: [S]top when you've reached your destination.");
                PrintDriving();
            }

            Console.WriteLine("Game over.");
        }

        private static void DriveTruck()
        {
            vehicleType = VehicleType.SemiTruck;
            vehicle = new SemiTruck();
            PrintIntro("18 wheeler Semi Truck", "truck", "[F]orward", "Turn [L]eft", "Turn [R]ight", "Jack Knive to a [S]top");

            MoveTruckAndPrintResult();

            for (int i = 0; i < 5; i++)
            {
                PrintDriving();
            }

            currentDirection = Directions.Blockage;
            GetNextDirection(false);
            ProcessUserInput();

            Console.WriteLine("You're almost at your destination! Move [F]orward to approach destination.");
            ProcessUserInput();

            Console.WriteLine("Congratulations! You've arrived at your destination.");
        }

        private static void PrintIntro(string longName, string shortName, string forwardCommand, string leftCommand, string rightCommand, string stopCommand)
        {
            Console.WriteLine("Today you'll be driving a " + longName + ". How exciting!");
            Console.WriteLine("To control the " + shortName + ", use the following commands: " + forwardCommand + ", " + leftCommand + ", " + rightCommand + ", and " + stopCommand);
            Console.WriteLine();
            Console.WriteLine("Press any key to start " + shortName + ": ");
            ReadKey();
            Console.WriteLine(shortName + " starting... (Press any key to continue)");
            ReadKey();
            Console.WriteLine(shortName + " is currently facing exit. Please turn [R]ight.");
            ReadKey();

            while (currentInput != ConsoleKey.R)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine(shortName + " is currently facing exit. Please turn [R]ight.");
                ReadKey();
            }
        }

        private static void PrintDriving()
        {
            PrintCurrentTrafficSignal();
            GetNextDirection();

            while (signal.signalLight != TrafficLightSignal.LeftTurnGreen && signal.signalLight != TrafficLightSignal.Green)
            {
                WaitForLight();
            }

            if (currentDirection != Directions.Left && signal.signalLight != TrafficLightSignal.Green)
            {
                WaitForLight();
            }

            ProcessUserInput();
            signal.GetNextTrafficLightSignal();
        }

        private static void ReadKey()
        {
            currentInput = Console.ReadKey().Key;
            Console.WriteLine();
        }

        private static void ProcessUserInput()
        {
            ReadKey();
            int result = MoveVehicle();
            while (result < 0)
            {
                switch (result)
                {
                    case -1:
                        Console.WriteLine("Select [C]ontinue: ");
                        break;
                    case -2:
                        Console.WriteLine("Select another direction: ");
                        break;
                    case -3:
                        Console.WriteLine("Please [S]top the vehicle: ");
                        break;
                    case -4:
                        Console.WriteLine("You can only move [F]orward: ");
                        break;
                    case -5:
                        Console.WriteLine("You can only stop after moving [F]orward: ");
                        break;
                }
                ReadKey();
                result = MoveVehicle();
            }
        }

        private static int MoveVehicle()
        {
            int result = 0;

            if (vehicleType == VehicleType.SemiTruck)
                result = MoveTruckAndPrintResult();
            else if (vehicleType == VehicleType.SUV)
                result = MoveSUVAndPrintResult();

            return result;
        }

        private static void WaitForLight()
        {
            Console.WriteLine("Press any key to wait for light.");
            ReadKey();
            signal.UpdateTrafficLight();
            PrintCurrentTrafficSignal();
        }

        private static int MoveSUVAndPrintResult()
        {
            if (currentInput == ConsoleKey.F)
            {
                return MoveForward();
            }
            else if (currentInput == ConsoleKey.L)
            {
                return TurnLeft();
            }
            else if (currentInput == ConsoleKey.R)
            {
                return TurnRight();
            }
            else if (currentInput == ConsoleKey.S)
            {
                if (vehicle.GetCurrentMovement() == VehicleMovement.MovingForward)
                    Console.WriteLine(vehicle.StopVehicle());
                else
                    return -5;
            }

            return 0;
        }

        private static int MoveTruckAndPrintResult()
        {
            if (currentDirection != Directions.Blockage)
            {
                if (currentInput == ConsoleKey.F)
                {
                    return MoveForward();
                }
                else if (currentInput == ConsoleKey.L)
                {
                    if (vehicle.GetCurrentMovement() == VehicleMovement.Stopped)
                        return -4;

                    return TurnLeft();
                }
                else if (currentInput == ConsoleKey.R)
                {
                    if (vehicle.GetCurrentMovement() == VehicleMovement.Stopped)
                        return -4;

                    return TurnRight();
                }
                else if (currentInput == ConsoleKey.C)
                {
                    Console.WriteLine("Vehicle continued moving.");
                }
                else
                {
                    Console.WriteLine("Invalid input.");

                    if (vehicle.GetCurrentMovement() == VehicleMovement.Stopped)
                        return -4;

                    return -2;
                }
            }
            else
            {
                if (currentInput == ConsoleKey.S)
                {
                    Console.WriteLine(vehicle.StopVehicle());
                    currentDirection = Directions.Forward;
                }
                else
                {
                    Console.WriteLine("No! You need to stop the vehicle!");
                    return -3;
                }
            }

            return 0;
        }

        private static int MoveForward()
        {
            bool vehicleAlreadyMovingForward = (vehicle.GetCurrentMovement() == VehicleMovement.MovingForward);
            Console.WriteLine(vehicle.MoveVehicleForward());

            if (currentDirection != Directions.Forward)
                Console.WriteLine("Recalculating...");
            else if (vehicleAlreadyMovingForward)
                return -1;

            return 0;
        }

        private static int TurnLeft()
        {
            Console.WriteLine(vehicle.TurnVehicleLeft(signal.signalLight));

            if (currentDirection != Directions.Left && vehicle.GetCurrentMovement() != VehicleMovement.Stopped)
                Console.WriteLine("Recalculating...");

            if (signal.signalLight != TrafficLightSignal.LeftTurnGreen)
                return -2;

            return 0;
        }

        private static int TurnRight()
        {
            Console.WriteLine(vehicle.TurnVehicleRight());
            if (currentDirection != Directions.Right && vehicle.GetCurrentMovement() != VehicleMovement.Stopped)
                Console.WriteLine("Recalculating...");
            return 0;
        }

        private static void GetNextDirection(bool generate = true)
        {
            if (generate)
                currentDirection = Pathfinding.GetNextDirection();

            switch (currentDirection)
            {
                case Directions.Forward:
                    Console.WriteLine("Go [F]orward to the next traffic light. (If already moving forward, [C]ontinue.)");
                    break;
                case Directions.Left:
                    Console.WriteLine("Turn [L]eft at this traffic light");
                    break;
                case Directions.Right:
                    Console.WriteLine("Turn [R]ight at this traffic light");
                    break;
                case Directions.Blockage:
                    Console.WriteLine("There is a blockage up ahead! It seems to be a fiery collision of a SUV and a red Ford Pinto. [S]top the " + vehicle.GetVehicleName());
                    break;
            }
        }

        private static void PrintCurrentTrafficSignal()
        {
            switch (signal.signalLight)
            {
                case TrafficLightSignal.Green:
                    Console.WriteLine("The signal light is Green.");
                    break;
                case TrafficLightSignal.Yellow:
                    Console.WriteLine("This signal light is Yellow.");
                    break;
                case TrafficLightSignal.Red:
                    if (signal.GetRedLightCount() == 0)
                        Console.WriteLine("This signal light is Red.");
                    else if (signal.GetRedLightCount() == 1)
                        Console.WriteLine("This signal light is still Red.");
                    else if (signal.GetRedLightCount() == 2)
                        Console.WriteLine("This signal light is unfortunately still Red.");
                    break;
                case TrafficLightSignal.LeftTurnGreen:
                    Console.WriteLine("This signal light is Left Turn Green.");
                    break;
            }
        }
    }
}
