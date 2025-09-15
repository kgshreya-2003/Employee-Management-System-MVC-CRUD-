using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

public class EmployeeController : Controller
{
    public ActionResult Index()
    {
        var employees = EmployeeDataAccess.GetAllEmployees();
        return View(employees);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Employee emp)
    {
        if (ModelState.IsValid)
        {
            EmployeeDataAccess.AddEmployee(emp);
            return RedirectToAction("Index");
        }
        return View(emp);
    }

    public ActionResult Edit(int id)
    {
        var emp = EmployeeDataAccess.GetEmployeeById(id);
        return View(emp);
    }

    [HttpPost]
    public ActionResult Edit(Employee emp)
    {
        if (ModelState.IsValid)
        {
            EmployeeDataAccess.UpdateEmployee(emp);
            return RedirectToAction("Index");
        }
        return View(emp);
    }

    public ActionResult Delete(int id)
    {
        var emp = EmployeeDataAccess.GetEmployeeById(id);
        return View(emp);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        EmployeeDataAccess.DeleteEmployee(id);
        return RedirectToAction("Index");
    }
}
