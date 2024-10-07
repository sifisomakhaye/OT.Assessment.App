using System.ComponentModel.DataAnnotations;
namespace OT.Assessment.App.Models
{
    public class CasinoWager
    {
        [Key] // This attribute specifies that this property is the primary key
        public Guid WagerId { get; set; }
        public string? Theme { get; set; }
        public string? Provider { get; set; }
        public string? GameName { get; set; }
        public string? TransactionId { get; set; }
        public string? BrandId { get; set; }
        public string? AccountId { get; set; }
        public string? Username { get; set; }
        public string? ExternalReferenceId { get; set; }
        public string? TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int NumberOfBets { get; set; }
        public string? CountryCode { get; set; }
        public string? SessionData { get; set; }
        public long Duration { get; set; }
    }
}