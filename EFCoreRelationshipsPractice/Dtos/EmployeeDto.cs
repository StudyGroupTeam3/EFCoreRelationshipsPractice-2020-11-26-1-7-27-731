using EFCoreRelationshipsPractice.NewFolder;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
        }

        public EmployeeDto(EmployeeEntity employee)
        {
            Name = employee.Name;
            Age = employee.Age;
        }

        public string Name { get; set; }
        public int Age { get; set; }
    }
}