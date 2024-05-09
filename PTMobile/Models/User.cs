using SQLite;

namespace PTMobile.Models
{
    [Table ("User")]
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Unique]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
