using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2023PA651_2022IV650.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;

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

            List<clientes> listadoclientes = (from e in _RestauranteContexto.clientes
                                              select e).ToList();

            if (listadoclientes.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoclientes);
        }

        [HttpGet]
        [Route("GetById/{direccion}")]
        public IActionResult Get(string direccion)
        {
            List<clientes> listadoclientes = (from e in _RestauranteContexto.clientes                                                                     
                                    where e.direccion.Contains(direccion)
                                    select e).ToList();

            if (listadoclientes == null)
            {
                return NotFound();
            }
            return Ok(listadoclientes);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarCliente([FromBody] clientes clientes)
        {
            try
            {
                _RestauranteContexto.clientes.Add(clientes);
                _RestauranteContexto.SaveChanges();
                return Ok(clientes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarCliente(int id, [FromBody] clientes clienteModificar)
        {
            //Para actualizar un registro primero se accede a el desde la base
            clientes? clienteActual = (from e in _RestauranteContexto.clientes
                                  where e.clienteId == id
                                  select e).FirstOrDefault();
            //Se verifica que exista segun su ID
            if (clienteActual == null)
            { return NotFound(); }

            //Si se ecuentra se altera
            clienteActual.nombreCliente = clienteModificar.nombreCliente;
            clienteActual.direccion = clienteModificar.direccion;

            //Se marca como modificado y se envía
            _RestauranteContexto.Entry(clienteActual).State = EntityState.Modified;
            _RestauranteContexto.SaveChanges();
            return Ok(clienteModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarCliente(int id)
        {
            //Se obtiene el original de la base
            clientes? cliente = (from e in _RestauranteContexto.clientes
                            where e.clienteId == id
                            select e).FirstOrDefault();
            //Verificar si existe
            if (cliente == null)
                return NotFound();

            //Se elimina el registro
            _RestauranteContexto.clientes.Attach(cliente);
            _RestauranteContexto.clientes.Remove(cliente);
            _RestauranteContexto.SaveChanges();
            return Ok(cliente);
        }

    }
}
