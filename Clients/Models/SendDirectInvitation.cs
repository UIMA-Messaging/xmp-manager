namespace XmpManager.Clients.Models
{
    public class SendDirectInvitation
    {
        public string Name { get; set; }
        public string Service { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Reason { get; set; }
        public string Users { get; set; }
    }
}
