using MyRealWorld.Models;
using System.Drawing;

namespace MyRealWorld.ViewModels
{
    public class MainVM
    {
        public List<VertLineVM> VertLines = new List<VertLineVM>();
        public SysRectangle Rectangle = new SysRectangle();
        
        public MainVM()
        {
            
        }
        public MainVM(int width, int height)
        {
            Rectangle.Width = width;
            Rectangle.Height = height;
            UpdateCollection();
        }
        protected void UpdateCollection()
        {
            int numlines = (int)(Rectangle.Width / 22);
            for (int i = 0; i< numlines; ++i)
            {
                VertLines.Add( new VertLineVM());
            }
        }
        public void Update()
        {
        }
    }
}
