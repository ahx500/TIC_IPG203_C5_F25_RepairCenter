
using System;
using RepairCenter.Events;
using RepairCenter.Interfaces;
using RepairCenter.Services;

namespace RepairCenter.Models
{
    public abstract class Appliance : IRepairable
    {
        private readonly string _serialNumber;
        private string _brand;
        private string _model;
        private string _status;

        public event DeviceStatusChangedHandler StatusChanged;

        protected Appliance(
            string serialNumber,
            string brand,
            string model)
        {
            if (!ApplianceValidator.IsValidSerialNumber(serialNumber))
            {
                throw new ArgumentException(
                    "Invalid serial number.", nameof(serialNumber));
            }

            if (!ApplianceValidator.IsValidText(brand))
            {
                throw new ArgumentException(
                    "Brand cannot be empty.", nameof(brand));
            }

            if (!ApplianceValidator.IsValidText(model))
            {
                throw new ArgumentException(
                    "Model cannot be empty.", nameof(model));
            }

            _serialNumber = serialNumber;
            _brand = brand.Trim();
            _model = model.Trim();
            _status = "Waiting";
        }

        public string SerialNumber
        {
            get { return _serialNumber; }
        }

        public string Brand
        {
            get { return _brand; }
            set
            {
                if (!ApplianceValidator.IsValidText(value))
                {
                    throw new ArgumentException("Invalid brand.");
                }

                _brand = value.Trim();
            }
        }

        public string Model
        {
            get { return _model; }
            set
            {
                if (!ApplianceValidator.IsValidText(value))
                {
                    throw new ArgumentException("Invalid model.");
                }

                _model = value.Trim();
            }
        }

        public string Status
        {
            get { return _status; }
        }

        private void ChangeStatus(string newStatus)
        {
            if (_status == newStatus)
                return;

            string oldStatus = _status;
            _status = newStatus;

            StatusChanged?.Invoke(
                this,
                new DeviceStatusChangedEventArgs(
                    SerialNumber,
                    oldStatus,
                    newStatus));
        }

        public void StartMaintenance()
        {
            if (_status != "Waiting")
            {
                throw new InvalidOperationException(
                    "Only devices waiting for maintenance can be started.");
            }

            ChangeStatus("In Progress");
        }

        public void CompleteMaintenance()
        {
            if (_status != "In Progress")
            {
                throw new InvalidOperationException(
                    "Only devices in progress can be marked as repaired.");
            }

            ChangeStatus("Repaired");
        }

        public abstract decimal CalculateRepairCost();
    }
}
