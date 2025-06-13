using Microsoft.CodeAnalysis;
using MRW_DAL.MyWEntities;
using MyRealWorld.Common;
using MyRealWorld.Models.DataWorking;
using MyRealWorld.Models.Utilities;
using System.Net;
using Project = MRW_DAL.MyWEntities.Project;

namespace MyRealWorld.ViewModels.Programming
{
    public class AddProjectVM : ProjectVM
    {
        public IFormFile Photo { get; set; }
        public string Comments { get; set; }
        public string ImageUrl { get; set; }
        public string FullPath { get; protected set; }

        public AddProjectVM()
        {
            ProjectName = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            ImageUrl = string.Empty;
            ProjectUrl = string.Empty;
            KeyWords = string.Empty;
            YearPublished = DateTime.Now.Year;
            CodeUrl = string.Empty;
            ProjectVersion = string.Empty;
            FullPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), Constants.Paths.ImageRepositoryRoot));
        }
        public AddProjectVM(int projId)
            :base(projId)
        {
            //GetProject();
            UpdatePictureData();
            FullPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), Constants.Paths.ImageRepositoryRoot));
        }
        protected void UpdatePictureData()
        {
            if(Pictures.Count>0)
            {
                Comments = Pictures[0].DescriptionImg;
                ImageUrl = Pictures[0].UrlImg;

            }
        }
        public async Task<bool>  SaveProject()
        {
            bool success = true;
            Project project = null;
            Picture pic = null;

            using (var context = new MRWContext())
            {
                try
                {
                    if (await UploadPicture())
                    {
                        pic = new Picture()
                        {
                            DescriptionImg = Comments,
                            UrlImg = ImageUrl
                        };
                        context.Pictures.Add(pic);    

                        project = new Project()
                        {
                            ProjectName = ProjectName,
                            Description = Description,
                            UrlProject = ProjectUrl??"",
                            YearPublishing = YearPublished,
                            KeyWords = KeyWords,
                            UrlCode = CodeUrl,
                            ProjectVersion = ProjectVersion

                        };
                        context.Projects.Add(project);
                        await context.SaveChangesAsync();

                        Project_Picture projectPicture = new Project_Picture
                        {
                            PictureID = pic.Id,
                            ProjectId = project.Id // This will be updated after the project is saved
                        };
                        context.Project_Picture.Add(projectPicture);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        success = false;
                    }
                }
                catch (Exception e)
                {
                    var s = e.Message;
                }
            }

            //if (success)
            //    await SaveComposedKeys(pic.Id, project.Id);
            return success;

        }
        public async Task<bool> UpdateProject()
        {
            bool success = true;
            Project project = null;
            Picture pic = null;
            using (var context = new MRWContext())
            {
                try
                {
                    if (await UploadPicture())
                    {
                        int picid = context.Project_Picture.Where(x => x.ProjectId == Id).Select(x => x.PictureID).FirstOrDefault();
                        pic = new Picture()
                        {
                            DescriptionImg = Comments,
                            UrlImg = ImageUrl,
                            Id = picid
                        };
                        context.Pictures.Update(pic);
                    }
                    
                
                    project = await context.Projects.FindAsync(Id);
                    if (project != null)
                    {
                        project.ProjectName = ProjectName;
                        project.Description = Description;
                        project.UrlProject = ProjectUrl??"";
                        project.YearPublishing = YearPublished;
                        project.KeyWords = KeyWords;
                        project.UrlCode = CodeUrl;
                        project.ProjectVersion = ProjectVersion;
                        context.Projects.Update(project);
                    }
                    else
                    {
                        success = false;
                    }
                    await context.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    var s = e.Message;
                }
            }
            return success;
        }
        protected async Task<bool> SaveComposedKeys(int picid, int projid)
        {
            bool success = true;

            using (var context = new MRWContext())
            {
                try
                {
                    Project_Picture projectPicture = new Project_Picture
                    {
                        PictureID = picid,
                        ProjectId = projid // This will be updated after the project is saved
                    };
                    context.Project_Picture.Add(projectPicture);
                    await context.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    success = false;
                    var s = e.Message;
                }
                return success;
            }
        }
        protected async Task<bool> UploadPicture()
        {
            bool success = true;
            try { 
            if (Photo != null && Photo.Length > 0)
            {
                ImageUrl = Path.GetFileName(Photo.FileName);
                if (Directory.Exists(FullPath))
                {
                    using (var filestream = new FileStream(Path.Combine(FullPath, ImageUrl), FileMode.Create))
                    {
                        await Photo.CopyToAsync(filestream);
                    }
                }
            }
            else
            {
                success = false;
                    ImageUrl = string.Empty;
            }
        }
            catch(Exception ex)
            {
                success = false;

                LogMaster logMaster = new LogMaster();
                logMaster.SetLog($"Error uploading picture: {ex.Message}");
            }

            return success;
        }
    }
}
