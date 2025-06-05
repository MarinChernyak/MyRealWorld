using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MRW_DAL.MyWEntities;
using Project = MRW_DAL.MyWEntities.Project;


namespace MyRealWorld.ViewModels.Programming
{
    public class ProgrammingVM
    {
        public List<ProjectVM> ProjectsCollection { get; set; } = new List<ProjectVM>();

        public ProgrammingVM() {
            GetProjects();
        }
        public void GetProjects()
        {
            using (var context = new MRWContext())
            {
                
                var projects = context.Projects.ToList();
                foreach (var project in projects)
                {
                    ProjectsCollection.Add(new ProjectVM(project.Id));                    
                }                
            }
        }
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
