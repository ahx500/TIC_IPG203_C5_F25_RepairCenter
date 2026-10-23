
namespace RepairCenter.Interfaces
{
    public interface IRepairable
    {
        void StartMaintenance();

        void CompleteMaintenance();

        decimal CalculateRepairCost();
    }
}
