using System.ComponentModel.DataAnnotations;

namespace XmpManager.Contracts
{
    public class KeyBundle
    {
        [Required]
        public Key IdentityKey { get; }
        [Required]
        public Key SignedPreKey { get; }
        [Required]
        [MinLength(200)]
        public Key[] OneTimePreKeys { get; }
    }
}
