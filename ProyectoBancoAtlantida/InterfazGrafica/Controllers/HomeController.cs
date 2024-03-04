using ApiRest.Models;
using ClosedXML.Excel;
using InterfazGrafica.Data;
using InterfazGrafica.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InterfazGrafica.Controllers
{
    public class HomeController : Controller
    {
        // Metodo para completar la URL de la API
        public string CompletarUrl(string controlador)
        {
            return $"https://localhost:44320/api/{controlador}";
        }
        
        // Metodo para mostrar la pagina principal con una lista de usuarios 
        public async Task<ActionResult> Index()
        {
            var httpCliente = new HttpClient();
            var json = await httpCliente.GetStringAsync(CompletarUrl("Tarjeta"));
            List<Usuarios> usuariosList = JsonConvert.DeserializeObject<List<Usuarios>>(json);

            return View(usuariosList);
            
        }

        //Metodo para mostrar el estado de cuenta y las compras realizadas
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

        //Metodo para mostrar la vista para registrar una compra
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

        //Metodo para guardar una compra
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

        //Metodo para guardar un pago
        public async Task<ActionResult> GuardarPago(Pago pago)
        {
            try
            {
                using( var httpClient = new HttpClient())
                {
                    var apiUrl = CompletarUrl("Pago");
                    var contenido = new StringContent(JsonConvert.SerializeObject(pago),Encoding.UTF8,"application/json");
                    var respuesta = await httpClient.PostAsync(apiUrl, contenido);
                    if (respuesta.IsSuccessStatusCode)
                        ViewBag.Mensaje = "Pago registrado con exito";
                    else
                        ViewBag.Mensaje = "Errir al registrar el pago";
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ViewBag.Mensaje = $"Error: {ex.Message}";
                return View();
            }
        }

        //Metodo para mostrar la vista de registro de pago
        public async Task<ActionResult> RegistroPago(int id)
        {

            var httpCliente = new HttpClient();

            var json = await httpCliente.GetStringAsync(CompletarUrl("EstadoCuenta") + $"/{id}");
            EstadoCuenta estadoCuentas = JsonConvert.DeserializeObject<EstadoCuenta>(json);
            if (id <= 0)
                return RedirectToAction("Index");

            ViewBag.TarjetaCreditoId = id;
            return View(estadoCuentas);
        }

        // Metodo para mostrar las transacciones
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

        //Metodo para exportar datos a un archivo de excel
        public async Task<FileResult> Exportar()
        {
            var httpCliente = new HttpClient();
            var json = await httpCliente.GetStringAsync(CompletarUrl("Compra"));
            List<Compra> usuariosList = JsonConvert.DeserializeObject<List<Compra>>(json);
            using (XLWorkbook libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add("Compra");
                // Agregar los datos a la hoja de Excel
                hoja.Cell(1, 1).InsertTable(usuariosList, "Compras");
                using (MemoryStream stream = new MemoryStream())
                {
                    libro.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Compras.xlsx");
                }
            }
        }



    }
}