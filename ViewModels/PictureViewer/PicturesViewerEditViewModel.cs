using Microsoft.AspNetCore.Http;
using MyRealWorld.Models.DataWorking;
using MyRealWorld.Models.Factories;
using MyRealWorld.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRealWorld.ViewModels.PictureViewer
{
    public class PicturesViewerEditViewModel : VisualBaseVM
    {

        protected String _defaultPicture = "M_NoPict.JPG";
        public bool Expanded { get; set;  }
        protected const double MAX_WIDTH = 200;
        protected const double MAX_HEIGHT = 250;
        public NewPictureData NewPictureData { get; set; } = new();
        public int IdRef { get; set; }
        public bool IsEdit { get; set; }

        public List<MPicture> _pictures { get; protected set; } = new();

        //This list and a property People are just for demo
        protected double GetMaxHeigth()
        {
            return MAX_HEIGHT;
        }
        protected String GeDefaultPict()
        {
            return _defaultPicture;
        }
        protected double GetMaxWidth()
        {
            return MAX_WIDTH;
        }
        public PicturesViewerEditViewModel()
        {
            IsEdit = false;
        }

        public PicturesViewerEditViewModel(int IDperson, ISession session)
        {
            IsEdit = false;
            Expanded = false;
            IdRef = IDperson;
            SetContextValuesAsync(session);
            InitCollection();
        }

        protected void InitCollection()
        {
            int ilevel = GetUserLevel();
            _pictures = PersonalDataFactory.GetPicturesCollection(IdRef, ilevel > 2);
            UpdatePictures();
        }
        public String DeleteImage(int idImage)
        {
            return "OK";
        }
        protected void UpdatePictures()
        {
            if (_pictures != null && _pictures.Count > 0)
            {
                for (int i = 0; i < _pictures.Count; ++i)
                {
                    try
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile($"{NewPictureData.FullPath}\\{_pictures[i].FileName}");
                        if (img != null)
                        {
                            double relationW = img.Width / GetMaxWidth();
                            double relationH = img.Height / GetMaxHeigth();

                            if (relationW > relationH)
                            {
                                _pictures[i].Width = (int)(img.Width / relationW);
                                _pictures[i].Height = (int)(img.Height / relationW);
                            }
                            else
                            {
                                _pictures[i].Width = (int)(img.Width / relationH);
                                _pictures[i].Height = (int)(img.Height / relationH);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        LogMaster lm = new LogMaster();
                        lm.SetLog(e.Message);
                    }
                }
            }
        }
    }
}