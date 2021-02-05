using System;
using DrivingSimulator1987.Enums;

namespace DrivingSimulator1987.Models
{
    public class StopLight
    {
        public TrafficLightSignal signalLight = TrafficLightSignal.Green;
        private Random rng = new Random();
        private int redLightCount;

        public StopLight()
        {
            redLightCount = 0;
        }

        public int GetRedLightCount()
        {
            return redLightCount;
        }

        public void GetNextTrafficLightSignal()
        {
            int randomLightSignal = rng.Next(4);

            switch (randomLightSignal)
            {
                case 0:
                    signalLight = TrafficLightSignal.Green;
                    break;
                case 1:
                    signalLight = TrafficLightSignal.Yellow;
                    break;
                case 2:
                    signalLight = TrafficLightSignal.Red;
                    break;
                case 3:
                    signalLight = TrafficLightSignal.LeftTurnGreen;
                    break;
                default:
                    signalLight = TrafficLightSignal.Green;
                    break;
            }
        }

        public void UpdateTrafficLight()
        {
            switch(signalLight)
            {
                case TrafficLightSignal.Green:
                    signalLight = TrafficLightSignal.Yellow;
                    break;
                case TrafficLightSignal.Yellow:
                    signalLight = TrafficLightSignal.Red;
                    redLightCount = 0;
                    break;
                case TrafficLightSignal.Red:
                    bool willBeRed = rng.Next(2) == 1;

                    if (redLightCount == 2)
                    {
                        signalLight = TrafficLightSignal.LeftTurnGreen;
                    }
                    else
                    {
                        if (willBeRed)
                            redLightCount++;
                        else
                            signalLight = TrafficLightSignal.LeftTurnGreen;
                    }
                    break;
                case TrafficLightSignal.LeftTurnGreen:
                    signalLight = TrafficLightSignal.Green;
                    break;
            }
        }
    }
}
