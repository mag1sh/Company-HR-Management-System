using CompanyHRManagementSystem.Employees.Domain.Entities;
using Domain.Entities;

namespace Services.Interfaces
{
    internal interface ILeaveService
    {
        void RequestLeave(Leave leave);

        void ApproveLeave(int leaveId);

        void RejectLeave(int leaveId);

        List<Leave> GetAllLeaves();
    }
}