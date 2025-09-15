using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

public static class EmployeeDataAccess
{
    private static string filePath = "employees.json";

    public static List<Employee> GetAllEmployees()
    {
        if (!File.Exists(filePath))
            return new List<Employee>();

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Employee>>(json) ?? new List<Employee>();
    }

    public static void SaveAllEmployees(List<Employee> employees)
    {
        var json = JsonConvert.SerializeObject(employees);
        File.WriteAllText(filePath, json);
    }

    public static void AddEmployee(Employee emp)
    {
        var employees = GetAllEmployees();
        emp.Id = employees.Count > 0 ? employees.Max(e => e.Id) + 1 : 1;
        employees.Add(emp);
        SaveAllEmployees(employees);
    }

    public static void UpdateEmployee(Employee emp)
    {
        var employees = GetAllEmployees();
        var existing = employees.FirstOrDefault(e => e.Id == emp.Id);
        if (existing != null)
        {
            existing.Name = emp.Name;
            existing.Department = emp.Department;
            existing.Email = emp.Email;
            SaveAllEmployees(employees);
        }
    }

    public static void DeleteEmployee(int id)
    {
        var employees = GetAllEmployees();
        var toRemove = employees.FirstOrDefault(e => e.Id == id);
        if (toRemove != null)
        {
            employees.Remove(toRemove);
            SaveAllEmployees(employees);
        }
    }

    public static Employee GetEmployeeById(int id)
    {
        return GetAllEmployees().FirstOrDefault(e => e.Id == id);
    }
}
