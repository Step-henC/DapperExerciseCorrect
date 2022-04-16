using System;
using System.Collections.Generic;
using System.Text;

namespace DapperDap
{
    public interface IDepartmentRepository
    {
        //we need a method that returns a collection of all departments
        IEnumerable<Department> GetAllDepartments();
        void InsertDepartment(string str);
    }
}
