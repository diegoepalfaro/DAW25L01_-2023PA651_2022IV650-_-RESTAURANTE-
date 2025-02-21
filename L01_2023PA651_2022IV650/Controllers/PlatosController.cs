using Microsoft.AspNetCore.Mvc;
using L01_2023PA651_2022IV650.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace L01_2023PA651_2022IV650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly RestauranteContext _RestauranteContexto;

        public PlatosController(RestauranteContext RestauranteContexto)
        {
            _RestauranteContexto = RestauranteContexto;
        }

        ///<sumary>
        ///Endpoint que retorna el listado de todos los platos existentes
        ///</sumary>
        ///<returns></returns>
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {

            List<platos> listadoplatos = (from e in _RestauranteContexto.platos
                                              select e).ToList();

            if (listadoplatos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoplatos);
        }

        [HttpGet]
        [Route("GetById/{nombre}")]
        public IActionResult Get(string nombre)
        {
            var listadopedidos = (from e in _RestauranteContexto.platos                                 
                                    where e.nombrePlato.Contains(nombre)
                                  select e).ToList();

            if (listadopedidos == null)
            {
                return NotFound();
            }
            return Ok(listadopedidos);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPlato([FromBody] platos plato)
        {
            try
            {
                _RestauranteContexto.platos.Add(plato);
                _RestauranteContexto.SaveChanges();
                return Ok(plato);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPlato(int id, [FromBody] platos platoModificar)
        {
            //Para actualizar un registro primero se accede a el desde la base
            platos? platoActual = (from e in _RestauranteContexto.platos
                                       where e.platoId == id
                                       select e).FirstOrDefault();
            //Se verifica que exista segun su ID
            if (platoActual == null)
            { return NotFound(); }

            //Si se ecuentra se altera
            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;

            //Se marca como modificado y se envía
            _RestauranteContexto.Entry(platoActual).State = EntityState.Modified;
            _RestauranteContexto.SaveChanges();
            return Ok(platoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPlato(int id)
        {
            //Se obtiene el original de la base
            platos? plato = (from e in _RestauranteContexto.platos
                                 where e.platoId == id
                                 select e).FirstOrDefault();
            //Verificar si existe
            if(plato == null)
                return NotFound();

            //Se elimina el registro
            _RestauranteContexto.platos.Attach(plato);
            _RestauranteContexto.platos.Remove(plato);
            _RestauranteContexto.SaveChanges();
            return Ok(plato);
        }

    }
}
