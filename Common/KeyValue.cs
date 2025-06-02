namespace MyRealWorld.Common
{
    public class KeyValue
    {
        public object Key { get; set; }
        public object Value { get; set; }
        public KeyValue() { }
        public KeyValue(object key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
