using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2023PA651_2022IV650.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace L01_2023PA651_2022IV650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly RestauranteContext _RestauranteContexto;

        public ClienteController(RestauranteContext RestauranteContexto)
        {
            _RestauranteContexto = RestauranteContexto;
        }

        ///<sumary>
        ///Endpoint que retorna el listado de todos los clientes existentes
        ///</sumary>
        ///<returns></returns>
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {

            List<Clientes> listadoLibros = (from e in _RestauranteContexto.Clientes
                                            select e).ToList();

            if (listadoLibros.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibros);
        }

    }
}
