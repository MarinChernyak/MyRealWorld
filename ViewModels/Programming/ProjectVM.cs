using MRW_DAL.MyWEntities;
using MyRealWorld.Common;
using System.ComponentModel;

namespace MyRealWorld.ViewModels.Programming
{
    public class ProjectVM
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public int YearPublished { get; set; } = DateTime.Now.Year;
        public List<Picture> Pictures { get; set; }=new List<Picture>();
        public List<KeyWord> KeyWords { get; set; }= new List<KeyWord>();

        protected const int DECRIPTION_LENGTH_LIMIT = 200;
        protected int Currect_index_img = 0;
        public ProjectVM(int id)
        {
            Id = id;
            GetProject();
            GetCollections();
        }
        public ProjectVM()
        {

        }
        protected void GetProject()
        {
            using (var context = new MRWContext())
            {
                try
                {
                    List<Project> proj = context.Projects.Where(x => x.Id == Id).ToList();
                    if(proj!=null && proj.Count>0)
                    {
                        ProjectName = proj[0].ProjectName;
                        Description = proj[0].Description;
                        ProjectUrl = proj[0].UrlProject;
                        YearPublished = proj[0].YearPublishing;

                    }
                    
                }
                catch (Exception e)
                {
                    var s = e.Message;
                }
            }
        }
        protected void GetCollections()
        {

            using (var context = new MRWContext())
            {
                try
                {
                    var proj_pics = context.Project_Picture.Where(x => x.ProjectId == Id).Select(z=>z.PictureID).ToList();
                    Pictures = context.Pictures.Where(x => proj_pics.Any(pp => x.Id == pp)).ToList();

                    var proj_kw = context.ProjectsKW.Where(x => x.ProjectId == Id).Select(x=>x.KWId).ToList();
                    KeyWords = context.KeyWords.Where(kw => proj_kw.Any(pkw => pkw == kw.Id)).ToList();

                }
                catch(Exception e)
                {
                    var s = e.Message;
                }
            }
        }
        public string GetImgSrc()
        {
            string src = string.Empty;
            if (Pictures.Count > 0 && Currect_index_img< Pictures.Count)
            {
                src = Pictures[Currect_index_img].UrlImg;
            }
            return src;
        }
        public string GetDescription()
        {
            string sdescript = Description;
            if (sdescript.Length > DECRIPTION_LENGTH_LIMIT)
            {
                sdescript = $"{sdescript.Substring(0, DECRIPTION_LENGTH_LIMIT)}...";
            }
            return sdescript;
        }
        public string GetKWList()
        {
            string s = string.Empty;
            if (KeyWords.Count > 0)
            {
                s = string.Join(", ", KeyWords.Select(kw => kw.DefaultEn));
            }
            return s;
        }
    }
}
