using System.ComponentModel.DataAnnotations;

namespace CrudPoc.Domain
{
    public class AnnouncementWebMotors
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Ano")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1900, 999999, ErrorMessage = "Valor inválido")]
        public int Year { get; set; }
        [Display(Name = "Quilometragem")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0, 999999, ErrorMessage = "Valor inválido")]
        public int Mileage { get; set; }

        [StringLength(45)]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Make { get; set; }
        [StringLength(45)]
        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Model { get; set; }
        [StringLength(45)]
        [Display(Name = "Versão")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Version { get; set; }
        [Display(Name = "Observação")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Observation { get; set; }
    }
}