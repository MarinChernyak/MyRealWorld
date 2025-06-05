using Microsoft.AspNetCore.Mvc;
using MyRealWorld.ViewModels.Programming;

namespace MyRealWorld.Controllers.Programming
{
    public class ProjectsController : Controller
    {
        public IActionResult AddProject()
        {
            AddProjectVM model = new AddProjectVM();
            return View("~/Views/Programming/AddEditProject.cshtml", model);
        }
        public async Task<IActionResult> SaveProject(AddProjectVM model)
        {
            if (ModelState.IsValid)
            {
                if(model.Id==0)
                {
                    await model.SaveProject();
                }
                else
                {
                    await model.UpdateProject();
                }
               
            }
            return RedirectToAction("Programming","Home");
        }
        public IActionResult EditProject(int projId)
        {
            AddProjectVM model = new AddProjectVM(projId);
            return View("~/Views/Programming/AddEditProject.cshtml", model);
        }
    }
}
