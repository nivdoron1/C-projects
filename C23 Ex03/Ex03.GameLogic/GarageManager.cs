using Ex03.GameLogic;
using Ex03.GameLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private class GarageVehicle
        {
            public string OwnerName { get; set; }
            public string OwnerPhoneNumber { get; set; }
            public eVehicleStatus Status { get; set; }
            public Vehicle vehicle { get; }

            public GarageVehicle(Vehicle i_vehicle, string i_ownerName, string i_ownerPhoneNumber, eVehicleStatus i_status)
            {
                this.vehicle = i_vehicle;
                this.OwnerName = i_ownerName;
                this.OwnerPhoneNumber = i_ownerPhoneNumber;
                this.Status = i_status;
            }
        }

        private readonly Dictionary <string, GarageVehicle> Garage_Vehicles = new Dictionary<string, GarageVehicle>();

        public bool isInGarage(string i_LicenseNumber)
        {
            return Garage_Vehicles.ContainsKey(i_LicenseNumber);
        }

        public GarageManager()
        {
        }

        public void AddVehicle(Vehicle i_vehicle, string i_ownerName, string i_ownerPhoneNumber, eVehicleStatus i_status, eVehicleType i_VehicleType)
        {
            if (isInGarage(i_vehicle.LicenseNumber))
            {
                Garage_Vehicles[i_vehicle.LicenseNumber].Status = eVehicleStatus.InRepair;
                throw new ArgumentException("The vehicle already exists, and its status has been updated to 'In Repair'");
            }
            else
            {

                GarageVehicle newGarageVehicle = new GarageVehicle(i_vehicle, i_ownerName, i_ownerPhoneNumber, i_status);
                Garage_Vehicles.Add(i_vehicle.LicenseNumber, newGarageVehicle);
            }
        }
        public List<string> GetVehicleLicenseNumbers(eVehicleStatus? filterStatus = null)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (var entry in Garage_Vehicles)
            {
                if (!filterStatus.HasValue || entry.Value.Status == filterStatus.Value)
                {
                    licenseNumbers.Add(entry.Key);
                }
            }

            return licenseNumbers;
        }
        public bool ChangeVehicleStatus(string i_licenseNumber, eVehicleStatus i_newStatus)
        {
            if (Garage_Vehicles.TryGetValue(i_licenseNumber, out GarageVehicle i_garageVehicle))
            {
                i_garageVehicle.Status = i_newStatus;
                return true; 
            }
            else
            {
                return false;
            }
        }
        
        
        public bool InflateTiresToMax(string licenseNumber)
        {
            if (Garage_Vehicles.TryGetValue(licenseNumber, out GarageVehicle garageVehicle))
            {
                garageVehicle.vehicle.InflateTiresToMax(); 
                return true;  
            }
            else
            {
                return false; 
            }
        }

        public bool RefuelVehicle(string licenseNumber, float amountToRefuel, eFuelType fuelType)
        {
            if (Garage_Vehicles.TryGetValue(licenseNumber, out GarageVehicle garageVehicle))
            {
                if (garageVehicle.vehicle is FuelVehicle fuelVehicle)
                {
                    try
                    {
                        fuelVehicle.Refuel(amountToRefuel, fuelType);
                        return true;
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message); 
                        return false;  
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);  
                        return false;  
                    }
                }
                else
                {
                    Console.WriteLine("The vehicle is not a fuel vehicle.");
                    return false;  
                }
            }
            else
            {
                Console.WriteLine("Vehicle not found.");
                return false; 
            }
        }
        public bool RechargeVehicle(string licenseNumber, float minutesToRecharge)
        {
            if (Garage_Vehicles.TryGetValue(licenseNumber, out GarageVehicle garageVehicle))
            {
                if (garageVehicle.vehicle is ElectricVehicle electricVehicle)
                {
                    try
                    {
                        electricVehicle.Recharge(minutesToRecharge); 
                        return true;  
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);  
                        return false;  
                    }
                }
                else
                {
                    Console.WriteLine("The vehicle is not an electric vehicle.");
                    return false; 
                }
            }
            else
            {
                Console.WriteLine("Vehicle not found.");
                return false; 
            }
        }

        public string DisplayVehicleDetails(string licenseNumber)
        {
            StringBuilder sb = new StringBuilder();

            if (Garage_Vehicles.TryGetValue(licenseNumber, out GarageVehicle garageVehicle))
            {
                Vehicle vehicle = garageVehicle.vehicle;

                sb.AppendLine("-------- Vehicle Details --------");
                sb.AppendFormat("License Number: {0}\n", vehicle.LicenseNumber);
                sb.AppendFormat("Model: {0}\n", vehicle.Model);
                sb.AppendFormat("Owner Name: {0}\n", garageVehicle.OwnerName);
                sb.AppendFormat("Status in Garage: {0}\n", garageVehicle.Status.ToString());

                sb.AppendLine("-------- Wheels --------");
                for (int i = 0; i < vehicle.Wheels.Count; i++)
                {
                    sb.AppendFormat("Wheel {0}:\n  Manufacturer: {1}\n  Current Air Pressure: {2}\n",
                                    i + 1, vehicle.Wheels[i].Manufacturer, vehicle.Wheels[i].CurrentAirPressure);
                }

                sb.AppendFormat("Remaining Energy: {0}\n", vehicle.RemainingEnergy);

                if (vehicle is FuelVehicle fuelVehicle)
                {
                    sb.AppendLine("-------- Fuel Vehicle Details --------");
                    sb.AppendFormat("Fuel Type: {0}\n", fuelVehicle.FuelType);
                    sb.AppendFormat("Current Fuel Amount: {0}\n", fuelVehicle.CurrentFuelAmount);
                    sb.AppendFormat("Maximum Fuel Amount: {0}\n", fuelVehicle.MaxFuelAmount);
                }
                else if (vehicle is ElectricVehicle electricVehicle)
                {
                    sb.AppendLine("-------- Electric Vehicle Details --------");
                    sb.AppendFormat("Battery Capacity: {0}\n", electricVehicle.RemainingBatteryTime);
                    sb.AppendFormat("Max Battery Capacity: {0}\n", electricVehicle.MaxBatteryTime);

                }

                if (vehicle is FuelCar fuelCar)
                {
                    sb.AppendLine("-------- Fuel Car Specific Details --------");
                    sb.AppendFormat("Color: {0}\n", fuelCar.Color);
                    sb.AppendFormat("Door Quantity: {0}\n", fuelCar.DoorQuantity);
                }
                else if (vehicle is ElectricCar electricCar)
                {
                    sb.AppendLine("-------- Electric Car Specific Details --------");
                    sb.AppendFormat("Color: {0}\n", electricCar.Color);
                    sb.AppendFormat("Door Quantity: {0}\n", electricCar.DoorQuantity);
                }

                return sb.ToString();
            }
            else
            {
                return "Vehicle not found.";
            }
        }


    }
}
