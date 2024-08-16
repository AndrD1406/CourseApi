using CourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Data
{
    public class TeachersTestCaseDataProvider
    {
        public static List<Teacher> GetTeachersSource()
        {
            return new List<Teacher>
        {
        new Teacher
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@gmail.com"
        },
        new Teacher
        {
            Id = 2,
            Name = "Jane Smith",
            Email = "jane.smith@gmail.com"
        },
        new Teacher
        {
            Id = 3,
            Name = "Emily Johnson",
            Email = "emily.johnson@gmail.com"
        },
        new Teacher
        {
            Id = 4,
            Name = "Michael Brown",
            Email = "michael.brown@gmail.com"
        }
        };
        }



    }
}
