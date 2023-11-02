using Ex03.GameLogic.Enums;
using Ex03.GarageLogic;
using System.Collections.Generic;

namespace Ex03.GameLogic
{
    public class FuelCar : FuelVehicle
    {

        public eCarColor Color { get; set; }
        public eDoorCount DoorQuantity { get; set; }

        public FuelCar(eCarColor i_color, eDoorCount i_door_count, List<Wheel> i_wheels)
        {
            this.Wheels = i_wheels;
            this.MaxFuelAmount = 44;
            this.FuelType = eFuelType.Octane95;
            this.Color = i_color;
            this.DoorQuantity = i_door_count;
        }
    }

}
