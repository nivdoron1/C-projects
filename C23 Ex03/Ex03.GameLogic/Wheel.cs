using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public string Manufacturer { get; set; }

        public float CurrentAirPressure { get; private set; }

        public float  MaxAirPressure { get; }

        public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            Manufacturer = i_Manufacturer;
            CurrentAirPressure = i_CurrentAirPressure;
            MaxAirPressure = i_MaxAirPressure;
        }

        public void Inflate(float i_airToAdd)
        { 

            float AirPressure = CurrentAirPressure + i_airToAdd;
        
            if (AirPressure <= MaxAirPressure && AirPressure >= 0)
            {
                CurrentAirPressure += i_airToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure);
            }
        }
    }
}
