using Ex03.GameLogic.Enums;
using Ex03.GarageLogic;
using System.Collections.Generic;

namespace Ex03.GameLogic
{
    public class FuelMotorcycle : FuelVehicle
    {
        private eLicenseType License { get; set; }
        private float EngineVolume { get; set; }
        public FuelMotorcycle(eLicenseType i_license, float i_engineVolume, List<Wheel> i_wheels)
        {
            this.Wheels = i_wheels;
            this.MaxFuelAmount = (float) 6.2;
            this.FuelType = eFuelType.Octane98;
            this.License = i_license;
            this.EngineVolume = i_engineVolume;
        }
    }

}
