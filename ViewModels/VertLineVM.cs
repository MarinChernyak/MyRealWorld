using MyRealWorld.Common;
using System.Data;

namespace MyRealWorld.ViewModels
{
    public class VertLineVM
    {
        public string LineData { get; protected set; }=string.Empty;
        public int Timeout { get; protected set; }
        public string Id { get; private set; }
        protected int NUM_CHAR_LINE => 42;
        public VertLineVM() {
            Random rnd = new Random();
            Timeout = rnd.Next(400, 2100);
            //UpdatLineData(LineData);
            Id= $"ln_{Constants.Values.Counter}";
        }
        public async Task UpdatLineData(string sline)
        {

            LineData = string.Empty;
            Random rnd = new Random();
            int val = rnd.Next(32, 126);
            if(string.IsNullOrEmpty(sline))
            {
                LineData+= Convert.ToChar(val);
            }
            else if (sline.Length< NUM_CHAR_LINE)
            {                 
                LineData = sline+ Convert.ToChar(val);
            }
            else
            {
                LineData = Convert.ToChar(val) +sline.Substring(0, sline.Length-1);
            }
             await Task.CompletedTask;
        }
    }
}
