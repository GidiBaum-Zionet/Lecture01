

//using Newtonsoft.Json;

namespace LinqDemo.Models
{
    public class Person
    {
        public int Id { get; set; }

        //[JsonProperty("first_name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string IpAddress { get; set; }

        public override string ToString() => $"{Id} {FirstName} {LastName} ({Gender})";
    }
}
