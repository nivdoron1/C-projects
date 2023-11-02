using Ex03.GameLogic.Enums;
using Ex03.GarageLogic;
using System.Collections.Generic;

namespace Ex03.GameLogic
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        private eLicenseType License { get; set; }
        private float EngineVolume { get; set; }

        public ElectricMotorcycle(eLicenseType i_license, float i_engineVolume, List<Wheel> i_wheels)
        {
            this.Wheels = i_wheels;
            this.MaxBatteryTime = (float) 2.4;
            this.License = i_license;
            this.EngineVolume = i_engineVolume;
        }


    }

}
