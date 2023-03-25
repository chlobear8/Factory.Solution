using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class AssignmentsController : Controller
  {
    private readonly FactoryContext _db;

    public AssignmentsController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Assignments.ToList());
    }

    public ActionResult Details(int id)
    {
      Assignment thisAssignment = _db.Assignments
          .Include(assignment => assignment.Machine)
          .Include(assignment => assignment.Engineer)
          .FirstOrDefault(assignment => assignment.AssignmentId == id);
      return View(thisAssignment);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Assignment assignment)
    {
      _db.Assignments.Add(assignment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMachine(int id)
    {
      Assignment thisAssignment = _db.Assignments.FirstOrDefault(assignments => assignments.AssignmentId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisAssignment);
    }

    [HttpPost]
    public ActionResult AddMachine(Assignment assignment, int machineId)
    {
      #nullable enable
      Assignment? joinEntity = _db.Assignments.FirstOrDefault(join => (join.MachineId == machineId && join.AssignmentId == assignment.AssignmentId));
      #nullable disable
      if (joinEntity == null && machineId != 0)
      {
        _db.Assignments.Add(new Assignment() { MachineId = machineId, AssignmentId = assignment.AssignmentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = assignment.AssignmentId });
    }

    public ActionResult AddEngineer(int id)
    {
      Assignment thisAssignment = _db.Assignments.FirstOrDefault(assignments => assignments.AssignmentId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisAssignment);
    }

    [HttpPost]
    public ActionResult AddEngineer(Assignment assignment, int engineerId)
    {
      #nullable enable
      Assignment? joinEntity = _db.Assignments.FirstOrDefault(join => (join.EngineerId == engineerId && join.AssignmentId == assignment.AssignmentId));
      #nullable disable
      if (joinEntity == null && engineerId != 0)
      {
        _db.Assignments.Add(new Assignment() { EngineerId = engineerId, AssignmentId = assignment.AssignmentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = assignment.AssignmentId });
    }

    public ActionResult Edit(int id)
    {
      Assignment thisAssignment = _db.Assignments.FirstOrDefault(assignments => assignments.AssignmentId == id);
      return View(thisAssignment);
    }

    [HttpPost]
    public ActionResult Edit(Assignment assignment)
    {
      _db.Assignments.Update(assignment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Assignment thisAssignment = _db.Assignments.FirstOrDefault(assignments => assignments.AssignmentId == id);
      ViewBag.field = "Name";
      ViewBag.name = thisAssignment;
      return View(thisAssignment);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Assignment thisAssignment = _db.Assignments.FirstOrDefault(assignments => assignments.AssignmentId == id);
      _db.Assignments.Remove(thisAssignment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      Assignment joinEntry = _db.Assignments.FirstOrDefault(entry => entry.AssignmentId == joinId);
      _db.Assignments.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}