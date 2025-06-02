using MRW_DAL.MyWEntities;

namespace MyRealWorld.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Place { get; set; }
        public byte DataType { get; set; }
        public string Email { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
