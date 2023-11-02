using Ex03.GameLogic;
using Ex03.GameLogic.Enums;
using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageUI
{
    internal class UILogic
    {
        private readonly GarageManager Garage = new GarageManager();

        internal void ManageGarage()
        {
            bool continueRunning = true;

            while (continueRunning)
            {
                string mainMenu =string.Format(
                @"Select an option from the menu:
                1. Add Vehicle
                2. Display all License Numbers
                3. Change Vehicle Status
                4. Inflate Tires to Max
                5. Refuel Vehicle
                6. Recharge Vehicle
                7. Display Vehicle Details
                8. Exit
                Please choose an option between 1-8");
                Console.WriteLine(mainMenu);
                int choice = AccurateUserInputInRangeIntegers(1, 8);
                switch (choice)
                {
                    case 1:
                        AddNewVehicleUI();
                        break;
                    case 2:
                        DisplayLicenseNumbersUI();
                        break;
                    case 3:
                        ChangeVehicleStatusUI();
                        break;
                    case 4:
                        InflateTiresToMaxUI();
                        break;
                    case 5:
                        RefuelVehicleUI();
                        break;
                    case 6:
                        RechargeVehicleUI();
                        break;
                    case 7:
                        DisplayVehicleDetailsUI();
                        break;
                    case 8:
                        continueRunning = false;
                        break;
                }
            }
        }

        internal void AddNewVehicleUI()
        {
            try
            {
                Console.WriteLine("Enter the license number:");
                string licenseNumber = IsNonEmptyInput();

                Console.WriteLine("Enter the model of the car:");
                string model = IsNonEmptyInput();

                Console.WriteLine("Enter the owner's name:");
                string ownerName = IsNonEmptyInput();

                Console.WriteLine("Enter the owner's phone number:");
                string ownerPhoneNumber = IsOnlyDigitsString();


                Console.WriteLine("Enter the car Remaining percentage Energy:");
                float RemainingEnergy = AccurateUserInputInRangeFloat((float) 0,(float) 100);

                string menu = string.Format(
                                "Select the type of vehicle: write {0} - {1}\n" +
                                "1. Fuel Car\n" +
                                "2. Electric Car\n" +
                                "3. Fuel Motorcycle\n" +
                                "4. Electric Motorcycle\n" +
                                "5. Truck",
                                1, 5
                            );
                Console.WriteLine(menu);
                int vehicleTypeChoice = AccurateUserInputInRangeIntegers(1, 5);
                eVehicleType vehicleType = (eVehicleType)vehicleTypeChoice;

                if (vehicleType == eVehicleType.FuelCar)
                {
                    AddFuelCar(licenseNumber,model, ownerName, ownerPhoneNumber, RemainingEnergy);
                }
                else if (vehicleType == eVehicleType.ElectricCar)
                {
                    AddElectricCar(licenseNumber, model, ownerName, ownerPhoneNumber, RemainingEnergy);
                }
                else if (vehicleType == eVehicleType.FuelMotorcycle)
                {
                    AddFuelMotorcycle(licenseNumber, model, ownerName, ownerPhoneNumber, RemainingEnergy);
                }
                else if (vehicleType == eVehicleType.ElectricMotorcycle)
                {
                    AddElectricMotorcycle(licenseNumber, model, ownerName, ownerPhoneNumber, RemainingEnergy);
                }
                else if (vehicleType == eVehicleType.Truck)
                {
                    AddTruck(licenseNumber,
                             model,
                             ownerName,
                             ownerPhoneNumber,
                             RemainingEnergy);
                }
                else
                {
                    Console.WriteLine("Invalid vehicle type.");

                }


                Console.WriteLine("Vehicle added successfully.");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid input format: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("An unexpected error occurred: " + e.Message);
            }
        }

        private string IsNonEmptyInput()
        {
            string userInput;
            do
            {
                userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Input should contain some string try again ");
                }
            }
            while (string.IsNullOrEmpty(userInput));

            return userInput;
        }

        private string IsOnlyDigitsString()
        {
            string userInput = IsNonEmptyInput();

            while (!userInput.All(Char.IsDigit))
            {
                Console.WriteLine($"{Environment.NewLine}this is invalid input and should contain only digits.{Environment.NewLine} try again: ");
                userInput = IsNonEmptyInput();
            }

            return userInput;
        }
        
        private float AccurateUserInputInRangeFloat(float minValue, float maxValue)
        {
            float parsedInput = 0;
            bool isInputValid = false;

            while (!isInputValid)
            {
                Console.WriteLine($"Please enter a value between {minValue} and {maxValue}:");
                string userInput = Console.ReadLine();

                try
                {
                    parsedInput = float.Parse(userInput);

                    if (parsedInput < minValue || parsedInput > maxValue)
                    {
                        throw new ValueOutOfRangeException(minValue, maxValue);
                    }

                    Console.WriteLine($"You entered a valid value: {parsedInput}");
                    isInputValid = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter a number.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An unexpected error occurred: {e.Message}");
                }
            }
            return parsedInput;
        }
        
        public int AccurateUserInputInRangeIntegers(int minValue, int maxValue)
        {
            int parsedInput = 0;
            bool isInputValid = false;

            while (!isInputValid)
            {
                Console.WriteLine($"Please enter an integer value between {minValue} and {maxValue}:");
                string userInput = Console.ReadLine();

                try
                {
                    parsedInput = int.Parse(userInput);

                    if (parsedInput < minValue || parsedInput > maxValue)
                    {
                        throw new ValueOutOfRangeException(minValue, maxValue);
                    }

                    Console.WriteLine($"You entered a valid value: {parsedInput}");
                    isInputValid = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter an integer.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An unexpected error occurred: {e.Message}");
                }
            }

            return parsedInput;
        }
        public List<Wheel> GetWheelsUI(int numberOfWheels, float maxAirPressure)
        {
            List<Wheel> wheels = new List<Wheel>();

            for (int i = 0; i < numberOfWheels; i++)
            {
                Console.WriteLine($"Enter details for wheel {i + 1}:");

                Console.WriteLine("Enter the manufacturer name:");
                string manufacturer = Console.ReadLine();
                Console.WriteLine("Enter the current Air Pressure name:");
                float currentAirPressure = AccurateUserInputInRangeFloat(0,maxAirPressure);

                Wheel wheel = new Wheel(manufacturer, currentAirPressure, maxAirPressure);
                wheels.Add(wheel);
            }

            return wheels;
        }
        public void AddFuelCar(string i_licensenumber,string i_model,string i_ownerName, string i_ownerPhoneNumber , float i_RemainingEnergy)
        {
            Console.WriteLine("Adding a new Fuel Car to the garage...");
            eCarColor carColor = GetCarColor();
            eDoorCount doorQuantity = GetDoorCount();
            List<Wheel> wheels = GetWheelsUI(5, 32);
            FuelCar newFuelCar = new FuelCar(carColor, doorQuantity, wheels);
            newFuelCar.LicenseNumber = i_licensenumber;
            newFuelCar.Model = i_model;
            newFuelCar.RemainingEnergy = i_RemainingEnergy;
            AddFuelInputsForVehicle(newFuelCar);
            Garage.AddVehicle(newFuelCar, i_ownerName, i_ownerPhoneNumber, eVehicleStatus.InRepair, eVehicleType.FuelCar);

        }
        public void AddElectricCar(string i_licensenumber, string i_model, string i_ownerName, string i_ownerPhoneNumber, float i_RemainingEnergy)
        {
            eCarColor carColor = GetCarColor();
            eDoorCount doorQuantity = GetDoorCount();
            List<Wheel> wheels = GetWheelsUI(5, 32);
            ElectricCar newElectricCar = new ElectricCar(carColor, doorQuantity,wheels);
            newElectricCar.LicenseNumber = i_licensenumber;
            newElectricCar.Model = i_model;
            newElectricCar.RemainingEnergy = i_RemainingEnergy;

            AddElectricInputsForVehicle(newElectricCar);
            Garage.AddVehicle(newElectricCar, i_ownerName, i_ownerPhoneNumber, eVehicleStatus.InRepair, eVehicleType.ElectricCar);

        }

        public void AddFuelInputsForVehicle(FuelVehicle i_fuelVehicle)
        {
            string fuelTypeMenu = string.Format(
                "Select the type of fuel: {0} - {1}\n" +
                "1. Soler \n" +
                "2. Octan95 \n" +
                "3. Octan96 \n" +
                "4. Octan98\n",
                1, 4
            );
            Console.WriteLine(fuelTypeMenu);
            int fuelTypeChoice = AccurateUserInputInRangeIntegers(1, 4);
            eFuelType selectedFuelType = (eFuelType)fuelTypeChoice;
            i_fuelVehicle.FuelType = selectedFuelType;

            Console.WriteLine($"Enter the current fuel amount (0 - {i_fuelVehicle.MaxFuelAmount}):");
            float currentFuelAmount = AccurateUserInputInRangeFloat(0, i_fuelVehicle.MaxFuelAmount);
            i_fuelVehicle.CurrentFuelAmount = currentFuelAmount;
        }
        public void AddFuelMotorcycle(string i_licensenumber, string i_model, string i_ownerName, string i_ownerPhoneNumber, float i_RemainingEnergy)
        {
            Console.WriteLine("Adding a new Fuel Motorcycle to the garage...");

            eLicenseType licenseType = GetLicenseType();
            float engineVolume = GetEngineVolume();
            List<Wheel> wheels = GetWheelsUI(2, 30);
            Console.WriteLine("hello");
            FuelMotorcycle newFuelMotorcycle = new FuelMotorcycle(licenseType, engineVolume, wheels)
            {
                LicenseNumber = i_licensenumber,
                Model = i_model,
                RemainingEnergy = i_RemainingEnergy
            };
            AddFuelInputsForVehicle(newFuelMotorcycle);
            Garage.AddVehicle(newFuelMotorcycle, i_ownerName, i_ownerPhoneNumber, eVehicleStatus.InRepair, eVehicleType.FuelCar);

        }
       
        public void AddElectricMotorcycle(string i_licensenumber, string i_model, string i_ownerName, string i_ownerPhoneNumber, float i_RemainingEnergy)
        {
            Console.WriteLine("Adding a new Electric Motorcycle to the garage...");
            eLicenseType licenseType = GetLicenseType();
            float engineVolume = GetEngineVolume();
            List<Wheel> wheels = GetWheelsUI(2, 30);
            ElectricMotorcycle newElectricMotorcycle = new ElectricMotorcycle(licenseType, engineVolume,wheels);
            newElectricMotorcycle.LicenseNumber = i_licensenumber;
            newElectricMotorcycle.Model = i_model;
            newElectricMotorcycle.RemainingEnergy = i_RemainingEnergy;
            AddElectricInputsForVehicle(newElectricMotorcycle);
            Garage.AddVehicle(newElectricMotorcycle, i_ownerName, i_ownerPhoneNumber, eVehicleStatus.InRepair, eVehicleType.FuelCar);

        }
        
        public eLicenseType GetLicenseType()
        {
            string licenseMenu = string.Format(
                "Select the type of license: {0} - {1}\n" +
                "1. A\n" +
                "2. A1\n" +
                "3. A2\n" +
                "4. AB\n",
                1, 4
            );
            Console.WriteLine(licenseMenu);
            int licenseChoice = AccurateUserInputInRangeIntegers(1, 4);
            eLicenseType licenseType = (eLicenseType)licenseChoice;

            return licenseType;
        }

        public float GetEngineVolume()
        {
            Console.WriteLine("Enter the engine volume (in cc):");
            float engineVolume = float.Parse(IsOnlyDigitsString());

            return engineVolume;
        }
        
        public void AddElectricInputsForVehicle(ElectricVehicle i_ElectricVehicle)
        {
            Console.WriteLine($"Enter the current Remaining Battery Time (0 - {i_ElectricVehicle.MaxBatteryTime}):");
            float RemainingBatteryTime = AccurateUserInputInRangeFloat(0, i_ElectricVehicle.MaxBatteryTime);
            i_ElectricVehicle.RemainingBatteryTime = RemainingBatteryTime;

        }
       
        public eCarColor GetCarColor()
        {
            string colorMenu = string.Format(
                "Select the color of the car: {0} - {1}\n" +
                "1. Red \n" +
                "2. Blue \n" +
                "3. Black \n" +
                "4. Gray\n",
                1, 4
            );
            Console.WriteLine(colorMenu);
            int colorChoice = AccurateUserInputInRangeIntegers(1, 4);
            eCarColor carColor = (eCarColor)colorChoice;

            return carColor;
        }

        public eDoorCount GetDoorCount()
        {
            string doorsMenu = string.Format(
                "Select the number of doors: {0} - {1}\n" +
                "2. Two \n" +
                "3. Three \n" +
                "4. Four \n" +
                "5. Five\n",
                2, 5
            );
            Console.WriteLine(doorsMenu);
            int doorChoice = AccurateUserInputInRangeIntegers(2, 5);
            eDoorCount doorQuantity = (eDoorCount)doorChoice;

            return doorQuantity;
        }

        public bool GetIsRefrigerated()
        {
            Console.WriteLine("Is the truck refrigerated? (yes/no)");
            string input = Console.ReadLine().Trim().ToLower();
            while (input != "yes" && input != "no")
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no':");
                input = Console.ReadLine().Trim().ToLower();
            }

            return input == "yes";
        }

        public float GetLoadVolume()
        {
            Console.WriteLine("Enter the load volume (in cubic meters):");
            return float.Parse(IsOnlyDigitsString()); 
        }
        
        public void AddTruck(string i_licensenumber, string i_model, string i_ownerName, string i_ownerPhoneNumber, float i_RemainingEnergy)
        {
            bool isRefrigerated = GetIsRefrigerated();
            float loadVolume = GetLoadVolume();
            List<Wheel> wheels = GetWheelsUI(12, 27);
            Truck newTruck = new Truck(isRefrigerated, loadVolume, wheels);
            newTruck.LicenseNumber = i_licensenumber;
            newTruck.Model = i_model;
            newTruck.RemainingEnergy = i_RemainingEnergy;
            AddFuelInputsForVehicle(newTruck);
            Garage.AddVehicle(newTruck, i_ownerName, i_ownerPhoneNumber, eVehicleStatus.InRepair, eVehicleType.FuelCar);

        }
        
        public void DisplayLicenseNumbersUI()
        {
            string statusMenu = string.Format(
                "Choose a filter by vehicle status or display all:\n" +
                "1. In Repair\n" +
                "2. Fixed\n" +
                "3. Paid\n" +
                "4. Display All\n"
            );

            Console.WriteLine(statusMenu);
            int filterChoice = AccurateUserInputInRangeIntegers(1, 4);

            eVehicleStatus? filterStatus = null;

            if (filterChoice != 4)
            {
                filterStatus = (eVehicleStatus)filterChoice;
            }

            List<string> licenseNumbers = Garage.GetVehicleLicenseNumbers(filterStatus);

            if (licenseNumbers.Count == 0)
            {
                Console.WriteLine("No vehicles found for the specified status.");
                return;
            }

            Console.WriteLine("The list of license numbers:");

            foreach (string license in licenseNumbers)
            {
                Console.WriteLine(license);
            }
        }

        public void ChangeVehicleStatusUI()
        {
            string licenseNumber = GetValidLicenseNumber();
            string statusMenu = string.Format(
                "Select the new status for the vehicle:\n" +
                "1. {0}\n" +
                "2. {1}\n" +
                "3. {2}\n",
                eVehicleStatus.InRepair,
                eVehicleStatus.Fixed,
                eVehicleStatus.Paid
            );

            Console.WriteLine(statusMenu);
            int statusChoice = AccurateUserInputInRangeIntegers(1, 3);
            eVehicleStatus newStatus = (eVehicleStatus)statusChoice;
            Garage.ChangeVehicleStatus(licenseNumber, newStatus);
        }

        public string GetValidLicenseNumber()
        {
            string licenseNumber;

            while (true)
            {
                Console.WriteLine("Enter the license number of the vehicle you want:");
                licenseNumber = Console.ReadLine();

                if (Garage.isInGarage(licenseNumber))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("The entered license number does not exist in the garage. Please try again.");
                }
            }

            return licenseNumber;
        }

        public void InflateTiresToMaxUI()
        {
            string licenseNumber = GetValidLicenseNumber();

            if (Garage.InflateTiresToMax(licenseNumber))
            {
                Console.WriteLine($"Successfully inflated the tires of the vehicle with license number {licenseNumber} to their maximum.");
            }
            else
            {
                Console.WriteLine("An error occurred while inflating the tires. Please try again.");
            }
        }

        public void RefuelVehicleUI()
        {
            string licenseNumber = GetValidLicenseNumber(); 

            string fuelMenu = string.Format(
                "Select the type of fuel: {0} - {1}\n" +
                "1. Soler\n" +
                "2. Octane95\n" +
                "3. Octane98\n" +
                "4. Octane96\n",
                1, 4
            );

            Console.WriteLine(fuelMenu);
            int fuelChoice = AccurateUserInputInRangeIntegers(1, 4);
            eFuelType fuelType = (eFuelType)fuelChoice;

            Console.WriteLine("Enter the amount to refuel:");
            float amountToRefuel = float.Parse(Console.ReadLine());

            if (Garage.RefuelVehicle(licenseNumber, amountToRefuel, fuelType))
            {
                Console.WriteLine("Vehicle has been successfully refueled.");
            }
        }

        public void RechargeVehicleUI()
        {
            string licenseNumber = GetValidLicenseNumber(); 

            Console.WriteLine("Enter the number of minutes you'd like to recharge:");
            float o_minutesToRecharge;
            while (true)
            {
                if (float.TryParse(Console.ReadLine(), out o_minutesToRecharge))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            if (Garage.RechargeVehicle(licenseNumber, o_minutesToRecharge))
            {
                Console.WriteLine("Vehicle has been successfully recharged.");
            }
        }

        public void DisplayVehicleDetailsUI()
        {
            string licenseNumber = GetValidLicenseNumber();  

            string vehicleDetails = Garage.DisplayVehicleDetails(licenseNumber);
            Console.WriteLine(vehicleDetails);
        }
    }
}
