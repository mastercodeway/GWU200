using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PixelCritic.WebApp.Models
{
    public class Review
    {
        #region Review public properties
        [Key]
        public Guid Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Score { get; set; }
        public DateOnly Posted { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        #endregion

        [Required]
        public int GameId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserRefId { get; set; }
        public  User User { get; set; }
    }
}
