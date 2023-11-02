using System;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle: Vehicle
    {
        public float RemainingBatteryTime { get; set; }
        public float MaxBatteryTime { get; set; }

        public void Recharge(float i_amount)
        {
            float newRemainingBatteryTime = RemainingBatteryTime + i_amount;
            if (newRemainingBatteryTime > MaxBatteryTime)
            {
                throw new InvalidOperationException("Amount exceeds battery capacity.");
            }

            RemainingBatteryTime = newRemainingBatteryTime;
        }
    }
}
