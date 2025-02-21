
using System.ComponentModel.DataAnnotations;
namespace L01_2023PA651_2022IV650.Models
{
    public class Clientes
    {
        [Key]
        public int clienteId { get; set; }

        public string nombreCliente { get; set; }

        public string direccion {  get; set; }
    }
}
