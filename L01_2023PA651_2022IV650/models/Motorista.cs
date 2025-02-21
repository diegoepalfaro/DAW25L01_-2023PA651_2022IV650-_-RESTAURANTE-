using System.ComponentModel.DataAnnotations;

namespace L01_2023PA651_2022IV650.Models
{
    public class Motorista
    {
        [Key] 
        public int MotoristaId { get; set; }

        public string NombreMotorista { get; set; }
    }
}
