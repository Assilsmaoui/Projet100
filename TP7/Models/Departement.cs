using System.ComponentModel.DataAnnotations;

namespace TP7.Models
{
    public class Departement
    {
        [Key]
        public int DepartementID { get; set; }


        [Required]
        [StringLength(50)]
        [MinLength(5,ErrorMessage ="Au moins 5 caractère")]
        public string Name { get; set; }
    }
}
