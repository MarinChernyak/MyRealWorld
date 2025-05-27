namespace MyRealWorld.Common
{
    public class CommonCounter
    {
        protected static int counter = 0;
        public static int Value { get {  return counter++; } }
    }
}
