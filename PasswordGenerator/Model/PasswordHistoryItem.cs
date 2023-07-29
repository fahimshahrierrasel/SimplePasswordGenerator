using SQLite;

namespace PasswordGenerator.Model
{
    public class PasswordHistoryItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Password { get; set; }
        public DateTime CopiedAt { get; set; }
    }
}
