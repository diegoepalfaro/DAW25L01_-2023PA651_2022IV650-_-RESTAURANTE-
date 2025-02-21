using System.ComponentModel.DataAnnotations;
namespace L01_2023PA651_2022IV650.Models
{
    public class Plato
    {
        [Key] 
        public int PlatoId { get; set; }

        public string NombrePlato { get; set; }

        public decimal Precio { get; set; }
    }
}
