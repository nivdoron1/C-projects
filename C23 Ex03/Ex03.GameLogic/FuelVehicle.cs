using System;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {
        public eFuelType FuelType { get; set; }
        public float CurrentFuelAmount { get; set; }
        public float MaxFuelAmount { get; set; }

        public void Refuel(float i_amount, eFuelType i_fuelType)
        {
            if (i_fuelType != FuelType)
            {
                throw new ArgumentException("Incompatible fuel type.");
            }

            if (CurrentFuelAmount + i_amount > MaxFuelAmount)
            {
                throw new InvalidOperationException("Amount exceeds tank capacity.");
            }

            CurrentFuelAmount += i_amount;
        }
    }
}
