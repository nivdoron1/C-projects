using Ex03.GarageLogic;
using System.Collections.Generic;

namespace Ex03.GameLogic
{
    public class Truck : FuelVehicle
    {

        private bool IsRefrigerated { get; set; }
        private float LoadVolume { get; set; }
        public Truck(bool i_isRefrigerated, float i_loadVolume, List<Wheel> i_wheels)
        {
            this.Wheels = i_wheels;
            this.MaxFuelAmount = 130;
            this.FuelType = eFuelType.Soler;
            this.LoadVolume = i_loadVolume;
            this.IsRefrigerated = i_isRefrigerated;
        }
    }
}
