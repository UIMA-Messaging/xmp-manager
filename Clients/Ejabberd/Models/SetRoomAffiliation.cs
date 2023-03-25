namespace XmpManager.Clients.Ejabberd.Models
{
    public class SetRoomAffiliation
    {
        public string Name { get; set; }
        public string Service { get; set; }
        public string Jid { get; set; }
        public string Affiliance { get; set; } = "memeber";
    }
}
