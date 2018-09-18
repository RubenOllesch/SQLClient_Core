using SQLClient_Web.Models;

namespace SQLClient_Web
{
    public class DataBaseSettings
    {
        public string ConnectionString { get; set; }
        public User[] Credentials { get; set; }
    }
}
