namespace XmpManager.Clients.Models
{
    public class CreateMucWithOptions
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Service { get; set; }
        public Options Options { get; set; }
    }

    public class Options
    {
        public string Name { get; set; } = "members_only";
        public string Password { get; set; } = Guid.NewGuid().ToString();
    }
}
