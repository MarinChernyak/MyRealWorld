using Microsoft.EntityFrameworkCore;
using MRW_DAL.MyWEntities;
using MyRealWorld.Common;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyRealWorld.ViewModels.Programming
{
    public class ProjectVM
    {
        public int Id { get; protected set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public List<KeyValue> Pictures { get; set; }=new List<KeyValue>();
        public List<KeyWord> KeyWords { get; set; }= new List<KeyWord>();

        protected const int DECRIPTION_LENGTH_LIMIT = 200;
        protected int Currect_index_img = 0;
        public ProjectVM(int id)
        {
            Id = id;
            GetCollections();
        }
        protected void GetCollections()
        {

            using (var context = new MRWContext())
            {
                try
                {
                    var proj_pics = context.Project_Picture.Where(x => x.ProjectId == Id).Select(z=>z.PictureID).ToList();
                    var pictures = context.Pictures.Where(x => proj_pics.Any(pp => x.Id == pp)).ToList();

                    var proj_kw = context.ProjectsKW.Where(x => x.ProjectId == Id).Select(x=>x.KWId).ToList();
                    KeyWords = context.KeyWords.Where(kw => proj_kw.Any(pkw => pkw == kw.Id)).ToList();

                    foreach (var p in pictures)
                    {
                        Pictures.Add(new KeyValue(p.Id, p.UrlImg));
                    }
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
                src = Pictures[Currect_index_img].Value.ToString();
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
    }
}
