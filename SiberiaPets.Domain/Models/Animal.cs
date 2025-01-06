using SiberiaPets.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace SiberiaPets.Domain.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }

        [Required(ErrorMessage = ErrorMessages.ErrorEmpty)]
        [StringLength(45, ErrorMessage = ErrorMessages.ErrorMaxCharacters + "45")]
        public string Description { get; set; }
    }
}
