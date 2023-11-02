using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {

        public string Model { get; set; }

        public string LicenseNumber { get; set; }

        public float RemainingEnergy { get; set; }

        public List<Wheel> Wheels { get; set; }

        public void InflateTiresToMax()
        {
            foreach (var wheel in Wheels)
            {
                float airToAdd = wheel.MaxAirPressure - wheel.CurrentAirPressure;
                wheel.Inflate(airToAdd);
            }
        }

    }
}
