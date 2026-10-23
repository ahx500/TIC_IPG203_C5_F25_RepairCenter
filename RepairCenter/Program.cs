
using System;
using System.Collections.Generic;
using RepairCenter.Models;
using RepairCenter.Services;
using RepairCenter.Events;

namespace RepairCenter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Appliance> appliances = new List<Appliance>
            {
                new Refrigerator("REF-001", "Samsung", "RF28", true),
                new WashingMachine("WM-001", "LG", "F4V", false),
                new AirConditioner("AC-001", "Gree", "GWC", true)
            };

            foreach (Appliance appliance in appliances)
            {
                appliance.StatusChanged += OnDeviceStatusChanged;
                RepairCenterStatistics.RegisterDevice(appliance);
            }

            Console.WriteLine("=== Starting Maintenance ===");

            foreach (Appliance appliance in appliances)
            {
                appliance.StartMaintenance();
            }

            Console.WriteLine();
            Console.WriteLine("=== Completing Maintenance ===");

            foreach (Appliance appliance in appliances)
            {
                appliance.CompleteMaintenance();
            }

            Console.WriteLine();
            Console.WriteLine("=== Statistics ===");
            Console.WriteLine(
                $"Total devices: {RepairCenterStatistics.TotalDevices}");

            Console.WriteLine(
                $"Total repair cost: {RepairCenterStatistics.TotalRepairCost}");

            Console.WriteLine(
                $"Repaired devices: " +
                $"{RepairCenterStatistics.GetDeviceCountByStatus("Repaired")}");

            Console.WriteLine();
            Console.WriteLine("=== Error Handling Tests ===");

            TestInvalidTransition(appliances[0]);
            TestDuplicateRegistration(appliances[0]);
            TestInvalidSerialNumber();

            Console.WriteLine();
            Console.WriteLine("Tests completed.");
            Console.ReadKey();
        }

        private static void OnDeviceStatusChanged(
            object sender,
            DeviceStatusChangedEventArgs e)
        {
            Console.WriteLine(
                $"Status event: {e.SerialNumber}: " +
                $"{e.OldStatus} -> {e.NewStatus}");
        }

        private static void TestInvalidTransition(Appliance appliance)
        {
            try
            {
                appliance.StartMaintenance();

                Console.WriteLine(
                    "FAIL: Starting an already repaired device was allowed.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(
                    "PASS: Invalid status transition was rejected.");
            }
        }

        private static void TestDuplicateRegistration(Appliance appliance)
        {
            try
            {
                RepairCenterStatistics.RegisterDevice(appliance);

                Console.WriteLine(
                    "FAIL: Duplicate device registration was allowed.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(
                    "PASS: Duplicate registration was rejected.");
            }
        }

        private static void TestInvalidSerialNumber()
        {
            try
            {
                Appliance invalidDevice = new Refrigerator(
                    "", "Samsung", "RF28", false);

                Console.WriteLine(
                    "FAIL: Invalid serial number was accepted.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine(
                    "PASS: Invalid serial number was rejected.");
            }
        }
    }
}
