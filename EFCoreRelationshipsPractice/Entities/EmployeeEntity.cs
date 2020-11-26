using EFCoreRelationshipsPractice.Dtos;

namespace EFCoreRelationshipsPractice.Entities
{
    public class EmployeeEntity
    {
        public EmployeeEntity()
        {
        }

        public EmployeeEntity(EmployeeDto employeeDto)
        {
            Name = employeeDto.Name;
            Age = employeeDto.Age;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
