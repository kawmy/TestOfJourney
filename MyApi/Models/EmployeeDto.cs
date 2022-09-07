using System;
using WebFramework.Api;
using Entities.Employee;

namespace MyApi.Models
{
    public class EmployeeDto : BaseDto<EmployeeDto,Employee,Guid>
    {
        //no inform on which fields are required
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
