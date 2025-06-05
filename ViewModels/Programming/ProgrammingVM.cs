using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MRW_DAL.MyWEntities;
using MyRealWorld.Common;
using Project = MRW_DAL.MyWEntities.Project;


namespace MyRealWorld.ViewModels.Programming
{
    public class ProgrammingVM
    {
        public enum ERROR_CODES
        {
            NO_ERROR = 0,
            ERROR_ADDING_PROJECT = 1,
            ERROR_DELETING_PROJECT = 2,
            ERROR_UPDATING_PROJECT = 3,
            ERROR_DELETING_PICTURE_DB = 4,
            ERROR_DELETING_PICTURE_FS = 5,
            ERROR_DELETING_PROJECT_PICTURE = 6,
            ERROR_PROJ_ID_NOT_SET = 7
        }
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
        public ERROR_CODES DeleteProject(int projId)
        {
            ERROR_CODES Err = ERROR_CODES.NO_ERROR;
            if (projId > 0)
            {
                using (var context = new MRWContext())
                {
                    try
                    {
                        var project = context.Projects.FirstOrDefault(x => x.Id == projId);
                        if (project != null)
                        {
                            context.Projects.Remove(project);
                            context.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Err= ERROR_CODES.ERROR_DELETING_PROJECT;
                    }
                }
                if(Err == ERROR_CODES.NO_ERROR)
                {
                    ProjectVM proj = ProjectsCollection.FirstOrDefault(x => x.Id == projId);
                    int idImg = proj.Pictures.Count > 0 ? proj.Pictures[0].Id : 0;
                    if (idImg > 0)
                    {
                        using (var context = new MRWContext())
                        {
                            var picture = context.Pictures.FirstOrDefault(x => x.Id == idImg);
                            try
                            {
                                if (picture != null)
                                {
                                    context.Pictures.Remove(picture);

                                }
                            }
                            catch (Exception e)
                            {
                                Err = ERROR_CODES.ERROR_DELETING_PICTURE_DB;
                            }
                            try
                            {
                                var projectPicture = context.Project_Picture.FirstOrDefault(x => x.PictureID == idImg && x.ProjectId == projId);
                                if (projectPicture != null)
                                {
                                    context.Project_Picture.Remove(projectPicture);

                                }
                            }
                            catch (Exception e)
                            {
                                Err = ERROR_CODES.ERROR_DELETING_PROJECT_PICTURE;
                            }
                            context.SaveChanges();

                            // Optionally, delete the image file from the file system if needed
                            string fullPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), Constants.Paths.ImageRepositoryRoot));
                            string imagePath = Path.Combine(fullPath, picture.UrlImg);
                            if (File.Exists(imagePath))
                            {
                                try
                                {
                                    File.Delete(imagePath);
                                }
                                catch (Exception ex)
                                {
                                    Err = ERROR_CODES.ERROR_DELETING_PICTURE_FS;
                                    Console.WriteLine($"Error deleting file: {ex.Message}");
                                }
                            }

                        }

                    }

                    ProjectsCollection.Remove(ProjectsCollection.FirstOrDefault(x => x.Id == projId));

                }
            }
            else
                Err= ERROR_CODES.ERROR_PROJ_ID_NOT_SET;
                return Err;
        }

    }
}
