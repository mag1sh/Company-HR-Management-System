using Domain.Entities;
using System.Collections.Generic;

namespace CompanyHRManagementSystem.Application.Interfaces
{
    public interface IPositionRepository
    {
        void Save(Position position);

        IReadOnlyList<Position> GetAll();

        Position GetById(int positionId);

        List<Position> GetByDepartmentId(int departmentId);

        bool ExistsByTitle(string title);
    }
}
