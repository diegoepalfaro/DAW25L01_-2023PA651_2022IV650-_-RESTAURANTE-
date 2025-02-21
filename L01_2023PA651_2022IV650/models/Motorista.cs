using System.ComponentModel.DataAnnotations;

namespace L01_2023PA651_2022IV650.Models
{
    public class motoristas
    {
        [Key] 
        public int motoristaId { get; set; }

        public string nombreMotorista { get; set; }
    }
}
