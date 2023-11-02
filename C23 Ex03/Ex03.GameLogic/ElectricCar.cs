using Ex03.GameLogic.Enums;
using Ex03.GarageLogic;
using System.Collections.Generic;

namespace Ex03.GameLogic
{
    public class ElectricCar : ElectricVehicle
    {
        public eCarColor Color { get; set; }
        public eDoorCount DoorQuantity { get; set; }

        public ElectricCar(eCarColor i_color, eDoorCount i_door_count, List<Wheel> i_wheels)
        {
            this.Wheels = i_wheels;
            this.MaxBatteryTime = (float) 5.2;
            this.Color = i_color;
            this.DoorQuantity = i_door_count;
        }

    }

}
