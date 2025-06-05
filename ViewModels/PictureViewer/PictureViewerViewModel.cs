using Microsoft.AspNetCore.Http;
using Nostralogia3.Models.DataWorking;
using System;
using System.Collections.Generic;

namespace Nostralogia3.ViewModels.PictureViewer
{
    public class PictureViewerViewModel : PictureViewerBase
    {
        //private readonly IWebHostEnvironment webHostEnvironment;
        public int RefID { get; set; }
        protected List<MPicture> _pictures;
        public int NumPictures { get { return _pictures!=null?_pictures.Count:0; } }

        public int GetID(int index)
        {
            int id = -1;
            if (_pictures != null && NumPictures > 0 && index >= 0 && index < _pictures.Count)
            {
                id = _pictures[index].Idpicture;
            }

            return id;
        }
        public PictureViewerViewModel()
        {

        }
        public PictureViewerViewModel(int IDRef, int icategory = 0)
        {
            if (IDRef > 0)
            {
                RefID = IDRef;
                //GetPictures(IDRef);
                UpdateImgDimentions();
            }
      
        }
        public PictureViewerViewModel(List<MPicture> _lst,int IDRef, int currentindex = 0, int icategory = 0)
        {
            RefID = IDRef;
            _pictures = _lst;
            UpdateImgDimentions();
       }
        //protected abstract void GetPictures(int IDRef);
        //protected abstract String GeDefaultPict();

        public String GetImgPath(int index)
        {
            String sout = "";
           //String sout = GeDefaultPict();
            if (_pictures != null && index >= 0 && index<_pictures.Count)
            {

                sout = String.Format("/DataRepository/PeopleImages/{0}", _pictures[index].FileName);
               
            }
            return sout;
        }

        protected void UpdateImgDimentions()
        {
            //if(_pictures != null && _pictures.Count>0)
            //{
            //    for(int index=0; index<_pictures.Count; index++)
            //    {
            //        String sout = GetImgPath(index);
            //        string uploadsFolder = Path.Combine(IWebHostEnvironment.WebRootPath, "images");
            //        Image img = Image.FromFile(HttpContext..Server.MapPath("~/{0}", sout)));
            //        if (img != null)
            //        {
            //            int width = img.Width;
            //            int height = img.Height;

            //            double kImg = 1.0;
            //            double kImgW = 1.0;
            //            double kImgH = 1.0;
            //            if (width > GetMaxWidth() )
            //            {
            //                kImgW =width / GetMaxWidth();
            //            }
            //            else if (height > GetMaxHeigth() )
            //            {
            //                kImgH = height / GetMaxHeigth();
            //            }
            //            kImg = kImgW > kImgH ? kImgW : kImgH;

            //            _pictures[index].Width = (int)(width / kImg);
            //            _pictures[index].Height = (int)(height / kImg);
            //        }
            //    }
            //}
        }

        public int GetImgWidth(int index)
        {
            int iW=0;
            if (_pictures != null && index >= 0 && index < _pictures.Count)
            {
                //UpdateDimentions(index);
                iW = (int)_pictures[index].Width;                
            }
            return iW;
        }
        public int GetImgHeight(int index)
        {
            int iH = 0;
            if (_pictures != null && index >= 0 && index < _pictures.Count)
            {               
                 //UpdateDimentions(index);
                 iH = (int)_pictures[index].Height;
                
            }
            return iH;
        }

        public override bool IsEdit()
        {
            return false;
        }
    }
}