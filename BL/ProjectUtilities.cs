using Microsoft.EntityFrameworkCore;
using MRW_DAL.MyWEntities;
using MyRealWorld.Models;
using MyRealWorld.Models.DataWorking;
using MyRealWorld.Models.Utilities;

namespace MyRealWorld.BL
{
    public class ProjectUtilities
    {
        public static int GetNumberOfPictures(int projectId)
        {
            if(projectId == 0) return 0;

            using (var context = new MRWContext())
            {
                return context.Project_Picture.Count(x => x.ProjectId == projectId);
            }
        }
        public static int AddNewPicture(MPicture mpict)
        {
            using (MRWContext context = new MRWContext())
            {
                try
                {                    
                    if (mpict != null && mpict.IdReference > 0)
                    {
                        context.Entry(mpict).State = EntityState.Added;
                        context.SaveChanges();

                        context.Project_Picture.Add(new Project_Picture()
                        {
                            ProjectId = mpict.IdReference,
                            PictureID = mpict.Id
                        });
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    LogMaster lm = new LogMaster();
                    lm.SetLog(e.Message);
                }
            }
            return mpict.Id;
        }
    }
}
