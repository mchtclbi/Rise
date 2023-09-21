using Space.Data.Entities;

namespace Space.UserAPI.Models.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}