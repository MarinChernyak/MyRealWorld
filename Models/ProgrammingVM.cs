using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using MyRealWorld.DAL;
using System.Xml;
using Project = MyRealWorld.DAL.Project;

namespace MyRealWorld.Models
{
    public class ProgrammingVM
    {


        public async Task AddProject(Project project)
        {
            if (project != null)
            {
                using (var context = new MRWContext())
                {
                    context.Projects.Add(project);

                    await context.SaveChangesAsync();

                    Console.WriteLine($"Added record with ID: {project.Id}");
                }
            }
        }

    }
}
