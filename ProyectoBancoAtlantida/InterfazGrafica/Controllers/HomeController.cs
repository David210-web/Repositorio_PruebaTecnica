using ApiRest.Models;
using InterfazGrafica.Data;
using InterfazGrafica.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InterfazGrafica.Controllers
{
    public class HomeController : Controller
    {

        public string CompletarUrl(string controlador)
        {
            return $"https://localhost:44320/api/{controlador}";
        }
        
        public async Task<ActionResult> Index()
        {
           
            var httpCliente = new HttpClient();
            var json = await httpCliente.GetStringAsync(CompletarUrl("Tarjeta"));
            List<Usuarios> usuariosList = JsonConvert.DeserializeObject<List<Usuarios>>(json);

            return View(usuariosList);
            
        }

        public async Task<ActionResult> EstadoCuenta(int id)
        {

            if (id <= 0)
                return RedirectToAction("Index");

            var httpCliente = new HttpClient();

            var json = await httpCliente.GetStringAsync(CompletarUrl("EstadoCuenta")+$"/{id}");
            EstadoCuenta estadoCuentas = JsonConvert.DeserializeObject<EstadoCuenta>(json);

            var json2 = await httpCliente.GetStringAsync(CompletarUrl("Compra")+$"/{id}");
            List<Compra> compra = JsonConvert.DeserializeObject<List<Compra>>(json2);

            var verModelos = new Estado_Compra
            {
                comprasUsuario = compra,
                EstadoCuenta = estadoCuentas
            };

            return View(verModelos);
        }

        public async Task<ActionResult> RegistrarCompra(int id)
        {
            var httpCliente = new HttpClient();

            var json = await httpCliente.GetStringAsync(CompletarUrl("EstadoCuenta") + $"/{id}");
            EstadoCuenta estadoCuentas = JsonConvert.DeserializeObject<EstadoCuenta>(json);

            if (id <= 0)
                return RedirectToAction("Index");

            ViewBag.TarjetaCreditoId = id;
            return View(estadoCuentas);
        }

        public async Task<ActionResult> GuardarCompra(Compra compra)
        {
            try
            {
                using(var httpClient = new HttpClient())
                {
                    var apiUrl = CompletarUrl("Compra");
                    var contenido = new StringContent(JsonConvert.SerializeObject(compra),Encoding.UTF8,"application/json");
                    var respuesta = await httpClient.PostAsync(apiUrl, contenido);

                    if (respuesta.IsSuccessStatusCode)
                        ViewBag.Mensaje = "Compra registrada con exito";
                    else
                        ViewBag.Mensaje = "Error al registrar la compra";

                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ViewBag.Mensaje = $"Error: {ex.Message}";
                return View();
            }
        }

        public async Task<ActionResult> RegistroPago(int id)
        {

            var httpCliente = new HttpClient();

            var json = await httpCliente.GetStringAsync(CompletarUrl("EstadoCuenta") + $"/{id}");
            EstadoCuenta estadoCuentas = JsonConvert.DeserializeObject<EstadoCuenta>(json);
            if (id <= 0)
                return RedirectToAction("Index");
            return View(estadoCuentas);
        }

        public async Task<ActionResult> Transacciones(int id)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            var httpCliente = new HttpClient();

            var json = await httpCliente.GetStringAsync(CompletarUrl("EstadoCuenta") + $"/{id}");
            EstadoCuenta estadoCuentas = JsonConvert.DeserializeObject<EstadoCuenta>(json);

            var json2 = await httpCliente.GetStringAsync(CompletarUrl("Transacciones") + $"/{id}");
            List<Transacciones> transacciones = JsonConvert.DeserializeObject<List<Transacciones>>(json2);

            var verModelos = new Estado_Transaccion
            {
                TransaccionesUsuario = transacciones,
                EstadoCuenta = estadoCuentas
            };
            return View(verModelos);
        }
    }
}