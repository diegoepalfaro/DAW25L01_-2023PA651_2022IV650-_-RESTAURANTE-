using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L01_2023PA651_2022IV650.Controllers
{
    using L01_2023PA651_2022IV650.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly RestauranteContext _RestauranteContexto;

        public PedidosController(RestauranteContext RestauranteContexto)
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

            List<pedidos> listadopedidos = (from e in _RestauranteContexto.pedidos
                                            select e).ToList();

            if (listadopedidos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadopedidos);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPedido([FromBody] pedidos pedido)
        {
            try
            {
                _RestauranteContexto.pedidos.Add(pedido);
                _RestauranteContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult Actualizarpedidos(int id, [FromBody] pedidos pedidosModificar)
        {
            //Para actualizar un registro primero se accede a el desde la base
            pedidos? pedidoActual = (from e in _RestauranteContexto.pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();
            //Se verifica que exista segun su ID
            if (pedidoActual == null)
            { return NotFound(); }

            //Si se ecuentra se altera
            pedidoActual.motoristaId = pedidosModificar.motoristaId;
            pedidoActual.clienteId = pedidosModificar.clienteId;
            pedidoActual.platoId = pedidosModificar.platoId;
            pedidoActual.cantidad = pedidosModificar.cantidad;
            pedidoActual.precio = pedidosModificar.precio;

            //Se marca como modificado y se envía
            _RestauranteContexto.Entry(pedidoActual).State = EntityState.Modified;
            _RestauranteContexto.SaveChanges();
            return Ok(pedidosModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPedidos(int id)
        {
            //Se obtiene el original de la base
            pedidos? pedido = (from e in _RestauranteContexto.pedidos
                               where e.pedidoId == id
                               select e).FirstOrDefault();
            //Verificar si existe
            if (pedido == null)
                return NotFound();

            //Se elimina el registro
            _RestauranteContexto.pedidos.Attach(pedido);
            _RestauranteContexto.pedidos.Remove(pedido);
            _RestauranteContexto.SaveChanges();
            return Ok(pedido);
        }
    }
}
