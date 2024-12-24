using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PruebaTecnica.Domain.Entities;

    [Table("Banks")]
    public class BankEntity
    {
        [Key]
        [MaxLength(8)]
        [JsonPropertyName("bic")]
        public string BIC { get; set; }

        [Required]
        [MaxLength(255)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        [JsonPropertyName("country")]
        public string Country { get; set; }
        public bool IsValid(){
            return IsBicValid(BIC) && IsCountryValid(Country) && IsNameValid(Name);
        }
        
        private bool IsBicValid(string bic) => bic.Length == 8;
        private bool IsCountryValid(string country) => country.Length >= 2;
        private bool IsNameValid(string name) => !string.IsNullOrEmpty(name);

    }