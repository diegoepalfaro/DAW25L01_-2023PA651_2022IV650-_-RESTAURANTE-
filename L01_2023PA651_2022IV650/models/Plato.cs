using System.ComponentModel.DataAnnotations;
namespace L01_2023PA651_2022IV650.Models
{
    public class platos
    {
        [Key] 
        public int platoId { get; set; }

        public string nombrePlato { get; set; }

        public decimal precio { get; set; }
    }
}
