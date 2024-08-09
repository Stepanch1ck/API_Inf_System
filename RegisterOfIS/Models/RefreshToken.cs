
using System.ComponentModel.DataAnnotations;

namespace RegisterOfIS.Models
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
