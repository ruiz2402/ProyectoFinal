using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using ProyectoInde.Models;
using RazorPDF;



namespace ProyectoInde.Controllers
{
    public class HomeController : Controller
    {

     


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        [Authorize(Roles = "administrador,operador")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "administrador")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "administrador")]
        [HttpGet]
        public ActionResult Graficas()

        {
            bd_inde2Context dc = new bd_inde2Context();
            // ViewData["CodTransformador"] = new SelectList(dc.Transformador, "CodTransformador", "Transformador1");

            var lista = dc.Transformador.Select(p => p.Transformador1).ToList();

            return View(); 

        }





        [HttpPost("/potencia")]

        public ActionResult GetPotencia(miFormulario miFormulario)
        {


            Debug.WriteLine("aquí estoy +++++++++++++++++");
          //  Debug.WriteLine(jsonData);


            Debug.WriteLine(miFormulario.fechaInicial);

            bd_inde2Context dc = new bd_inde2Context();
            

            var query = dc.DatoTransformador.Include("CodTransformadorNavigation")
                .GroupBy(p => p.CodTransformadorNavigation.Transformador1)
                .Select(g => new { name = g.Key, count = g.Sum(w => w.PotenciaMw) }).ToList();



            if (miFormulario.fechaInicial.Equals(miFormulario.fechaFinal))
            {
                Debug.WriteLine("esta es la hora +++++++++++++++++");
                Debug.WriteLine(miFormulario.horaInicial);

                var c = miFormulario.horaInicial.ToString();
                var d = miFormulario.horaFinal.ToString();
                if (!c.Equals("00:00:00") && !d.Equals("00:00:00"))
                {

                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.PotenciaMw.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Select(g => g.PotenciaMw.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);

                }
                else if (!c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                            .Select(g => g.PotenciaMw.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && !d.Equals("00:00:00")) {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                            .Select(g => g.PotenciaMw.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);

                }
            }
            else
            {
                var c = miFormulario.horaInicial.Hours;
                var d = miFormulario.horaFinal.Hours;

                var e = miFormulario.horaInicial.ToString();
                var f = miFormulario.horaFinal.ToString();

                double r = (d - c) + 1;
                double m = 1 / r;

                double k = 24;
                double s = 1 / k;

                if (!e.Equals("00:00:00") && !f.Equals("00:00:00"))
                {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                    .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.PotenciaMw * m).ToString()).ToList();



                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
                else if (e.Equals("00:00:00") && f.Equals("00:00:00")) {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))                   
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.PotenciaMw * 0.25).ToString()).ToList();

                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
            }

            return View();

        }
            

                                 

        [HttpPost("/temperaturaAc(C°)")]
        public ActionResult GetTemperatura(miFormulario miFormulario)
        {

            bd_inde2Context dc = new bd_inde2Context();
            var query = dc.DatoTransformador.Include("CodTransformadorNavigation")
                .GroupBy(p => p.CodTransformadorNavigation.Transformador1)
                .Select(g => new { name = g.Key, count = g.Sum(w => w.PotenciaMw) }).ToList();


            if (miFormulario.fechaInicial.Equals(miFormulario.fechaFinal))
            {
                Debug.WriteLine("esta es la hora +++++++++++++++++");
                Debug.WriteLine(miFormulario.horaInicial);

                var c = miFormulario.horaInicial.ToString();
                var d = miFormulario.horaFinal.ToString();
                if (!c.Equals("00:00:00") && !d.Equals("00:00:00"))
                {

                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.TemperaturaAcC.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Select(g => g.TemperaturaAcC.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);

                }
                else if (!c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                            .Select(g => g.TemperaturaAcC.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && !d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                            .Select(g => g.TemperaturaAcC.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);

                }
            }
            else
            {
                var c = miFormulario.horaInicial.Hours;
                var d = miFormulario.horaFinal.Hours;

                var e = miFormulario.horaInicial.ToString();
                var f = miFormulario.horaFinal.ToString();

                double r = (d - c) + 1;
                double m = 1 / r;

                double k = 24;
                double s = 1 / k;

                if (!e.Equals("00:00:00") && !f.Equals("00:00:00"))
                {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                    .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.TemperaturaAcC * m).ToString()).ToList();



                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
                else if (e.Equals("00:00:00") && f.Equals("00:00:00"))
                {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.TemperaturaAcC * 0.25).ToString()).ToList();

                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
            }

            return View();

        }

        [HttpPost("/temperaturaDE(C°)")]
        public ActionResult GetTemperatura2(miFormulario miFormulario)
        {

            bd_inde2Context dc = new bd_inde2Context();
            var query = dc.DatoTransformador.Include("CodTransformadorNavigation")
                .GroupBy(p => p.CodTransformadorNavigation.Transformador1)
                .Select(g => new { name = g.Key, count = g.Sum(w => w.PotenciaMw) }).ToList();


            if (miFormulario.fechaInicial.Equals(miFormulario.fechaFinal))
            {
                Debug.WriteLine("esta es la hora +++++++++++++++++");
                Debug.WriteLine(miFormulario.horaInicial);

                var c = miFormulario.horaInicial.ToString();
                var d = miFormulario.horaFinal.ToString();
                if (!c.Equals("00:00:00") && !d.Equals("00:00:00"))
                {

                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.TemperaturaDeC.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Select(g => g.TemperaturaDeC.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);

                }
                else if (!c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                            .Select(g => g.TemperaturaDeC.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && !d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                            .Select(g => g.TemperaturaDeC.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);

                }
            }
            else
            {
                var c = miFormulario.horaInicial.Hours;
                var d = miFormulario.horaFinal.Hours;

                var e = miFormulario.horaInicial.ToString();
                var f = miFormulario.horaFinal.ToString();

                double r = (d - c) + 1;
                double m = 1 / r;

                double k = 24;
                double s = 1 / k;

                if (!e.Equals("00:00:00") && !f.Equals("00:00:00"))
                {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                    .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.TemperaturaDeC * m).ToString()).ToList();



                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
                else if (e.Equals("00:00:00") && f.Equals("00:00:00"))
                {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.TemperaturaDeC * 0.25).ToString()).ToList();

                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
            }

            return View();

        }


        [HttpPost("/nivelAC(%)")]
        public ActionResult GetNivel(miFormulario miFormulario)
        {

            bd_inde2Context dc = new bd_inde2Context();
            var query = dc.DatoTransformador.Include("CodTransformadorNavigation")
                .GroupBy(p => p.CodTransformadorNavigation.Transformador1)
                .Select(g => new { name = g.Key, count = g.Sum(w => w.PotenciaMw) }).ToList();


            if (miFormulario.fechaInicial.Equals(miFormulario.fechaFinal))
            {
                Debug.WriteLine("esta es la hora +++++++++++++++++");
                Debug.WriteLine(miFormulario.horaInicial);

                var c = miFormulario.horaInicial.ToString();
                var d = miFormulario.horaFinal.ToString();
                if (!c.Equals("00:00:00") && !d.Equals("00:00:00"))
                {

                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.NivelAc.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Select(g => g.NivelAc.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);

                }
                else if (!c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                            .Select(g => g.NivelAc.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && !d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                            .Select(g => g.NivelAc.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);

                }
            }
            else
            {
                var c = miFormulario.horaInicial.Hours;
                var d = miFormulario.horaFinal.Hours;

                var e = miFormulario.horaInicial.ToString();
                var f = miFormulario.horaFinal.ToString();

                double r = (d - c) + 1;
                double m = 1 / r;

                double k = 24;
                double s = 1 / k;

                if (!e.Equals("00:00:00") && !f.Equals("00:00:00"))
                {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                    .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.NivelAc * m).ToString()).ToList();



                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
                else if (e.Equals("00:00:00") && f.Equals("00:00:00"))
                {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.NivelAc * 0.25).ToString()).ToList();

                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
            }

            return View();

        }


        [HttpPost("/vent")]
        public ActionResult GetVent(miFormulario miFormulario)
        {

            bd_inde2Context dc = new bd_inde2Context();
            var query = dc.DatoTransformador.Include("CodTransformadorNavigation")
                .GroupBy(p => p.CodTransformadorNavigation.Transformador1)
                .Select(g => new { name = g.Key, count = g.Sum(w => w.PotenciaMw) }).ToList();


            if (miFormulario.fechaInicial.Equals(miFormulario.fechaFinal))
            {
                Debug.WriteLine("esta es la hora +++++++++++++++++");
                Debug.WriteLine(miFormulario.horaInicial);

                var c = miFormulario.horaInicial.ToString();
                var d = miFormulario.horaFinal.ToString();
                if (!c.Equals("00:00:00") && !d.Equals("00:00:00"))
                {

                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.VentIMA.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Select(g => g.VentIMA.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);

                    return Json(myList);

                }
                else if (!c.Equals("00:00:00") && d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                            .Select(g => g.VentIMA.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);
                }
                else if (c.Equals("00:00:00") && !d.Equals("00:00:00"))
                {
                    var query2 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                            .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                            .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                            .Select(g => g.VentIMA.ToString()).ToList();

                    var query3 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                        .Where(p => p.FecIngreso == Convert.ToDateTime(miFormulario.fechaInicial))
                        .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                        .Select(g => g.CodHoraNavigation.Hora.ToString()).ToList();

                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query2);
                    myList.Add(query3);
                    return Json(myList);

                }
            }
            else
            {
                var c = miFormulario.horaInicial.Hours;
                var d = miFormulario.horaFinal.Hours;

                var e = miFormulario.horaInicial.ToString();
                var f = miFormulario.horaFinal.ToString();

                double r = (d - c) + 1;
                double m = 1 / r;

                double k = 24;
                double s = 1 / k;

                if (!e.Equals("00:00:00") && !f.Equals("00:00:00"))
                {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .Where(g => g.CodHoraNavigation.Hora >= miFormulario.horaInicial)
                    .Where(g => g.CodHoraNavigation.Hora <= miFormulario.horaFinal)
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.VentIMA * m).ToString()).ToList();



                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
                else if (e.Equals("00:00:00") && f.Equals("00:00:00"))
                {
                    var query4 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Sum(w => w.VentIMA * 0.25).ToString()).ToList();

                    var query5 = dc.DatoTransformador.Include("CodTransformadorNavigation").Where(p => p.CodTransformadorNavigation.Transformador1 == miFormulario.transformador)
                    .Where(p => p.FecIngreso >= Convert.ToDateTime(miFormulario.fechaInicial))
                    .Where(p => p.FecIngreso <= Convert.ToDateTime(miFormulario.fechaFinal))
                    .GroupBy(p => p.FecIngreso).Select(g => g.Key.ToString()).ToList();


                    List<List<String>> myList = new List<List<String>>();
                    myList.Add(query4);
                    myList.Add(query5);

                    return Json(myList);
                }
            }

            return View();

        }



        [HttpGet]
        public ActionResult Graficas1()
        {
            bd_inde2Context dc = new bd_inde2Context();
            ViewData["CodTransformador"] = new SelectList(dc.Transformador, "CodTransformador", "Transformador1");
            return View();
        }






        public ActionResult Graficas2()
        {
            return View();
        }


        public ActionResult Graficas3()
        {
            return View();
        }

        [HttpGet]
        public ActionResult informe()
        {

            return View();
        }

        [HttpPost]
        public ActionResult informe(string r) {

            return View();




        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
    }
}
