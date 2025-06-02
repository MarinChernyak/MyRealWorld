using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.Utilities
{
    public class StringGenerator
    {
        private const int default_string_length = 8;
        public string GenericString { get; protected set; }
        protected int NumberSymbols { get; set; }
        protected bool bNumbers { get; set; }
        protected bool bSpecialSymbols { get; set; }
        protected bool bCapitalsLetters { get; set; }
        protected bool bSmallLetters { get; set; }

        protected const string LLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        protected const string lLetters = "abcdefghijklmnopqrstuvwxyz";
        protected const string Numbers = "0123456789";
        protected const string Symbols = "!@#$%^&*-_=+|";

        protected List<string> Collections;
        protected List<bool> generated;

        public StringGenerator(int iNumSymb = default_string_length, bool bNum = true, bool bSpecSymb = true, bool CapLetters = true, bool SmLetters = true)
        {
            NumberSymbols = iNumSymb;
            bNumbers = bNum;
            bSpecialSymbols = bSpecSymb;
            bCapitalsLetters = CapLetters;
            bSmallLetters = SmLetters;
            Collections = new List<string>()
            {
                LLetters,lLetters,Numbers,Symbols
            };
            generated = new List<bool>()
            {
                false, false, false, false
            };


            Generate();
        }
        public void Generate()
        {
            GenericString = String.Empty;
            if (NumberSymbols == 0)
                NumberSymbols = default_string_length;


            Random rnd = new Random();

            while (GenericString.Length < NumberSymbols)
            {
                int index = 0;

                index = generated.FindIndex(x => x == false);
                if (GenericString.Length >= NumberSymbols - 4 && index>0)
                {
                    UpdateString(index);
                }
                else
                {
                    index = rnd.Next(0, Collections.Count - 1);
                    UpdateString(index);
                }
                
            }
        }
        protected void UpdateString(int indexCollection)
        {
            string str = Collections[indexCollection];
            generated[indexCollection] = true;
            Random rnd = new Random();
            int index = rnd.Next(0, str.Length - 1);
            GenericString += str[index];
        }
    }
}
