
using System;

namespace RepairCenter.Events
{
    public delegate void DeviceStatusChangedHandler(
        object sender,
        DeviceStatusChangedEventArgs e);

    public class DeviceStatusChangedEventArgs : EventArgs
    {
        public string SerialNumber { get; }
        public string OldStatus { get; }
        public string NewStatus { get; }

        public DeviceStatusChangedEventArgs(
            string serialNumber,
            string oldStatus,
            string newStatus)
        {
            SerialNumber = serialNumber;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }
}
