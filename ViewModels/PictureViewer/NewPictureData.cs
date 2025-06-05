using Microsoft.AspNetCore.Http;
using MyRealWorld.BL;
using MyRealWorld.Common;
using MyRealWorld.Models.DataWorking;
using MyRealWorld.Models.Factories;
using MyRealWorld.Models.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.ViewModels.PictureViewer
{
    public class NewPictureData
    {
        public int IdRef { get; set; }
        public IFormFile Photo { get; set; }
        public string Comments { get; set; }

        public string FullPath { get; protected set; }
        public NewPictureData()
        {
            FullPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), Constants.Paths.ImageRepository));
        }
        public NewPictureData(int id)
        {
            IdRef = id;
            FullPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), Constants.Paths.ImageRepository));
        }
        public async Task<int> UploadFile()
        {
            int newid =0;
            try
            {
                if (Photo.Length > 0)
                {
                    int inum = ProjectUtilities.GetNumberOfPictures(IdRef);
                    string filename = $"{IdRef}_{inum}{ Path.GetExtension(Photo.FileName)}";
                    if (Directory.Exists(FullPath))
                    {
                        using (var filestream = new FileStream(Path.Combine(FullPath, filename), FileMode.Create))
                        {
                            await Photo.CopyToAsync(filestream);
                        }
                        MPicture mp = new MPicture()
                        {
                            UrlImg = filename,                            
                            IdReference = IdRef,
                            DescriptionImg = Comments
                        };
                        newid = ProjectUtilities.AddNewPicture(mp);
                    }
                }
            }
            catch (Exception e)
            {
                LogMaster lm = new LogMaster();
                lm.SetLog(e.Message);
            }
            return newid ;
        }
    }
}
