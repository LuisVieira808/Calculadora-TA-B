using Calculadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index() {
            //perpara os valores iniciais do visor
            ViewBag.Visor = "0";
            ViewBag.PrimeiroOperador = "Sim";
            ViewBag.Operador = "";
            ViewBag.PrimeiroOperando = "";
            ViewBag.LimpaVisor = "Sim";
            return View();
        }
        [HttpPost]
        public IActionResult Index(string botao, string visor, string PrimeiroOperador,
            string primeiroOperando, string operador, string limpaVisor) {
            switch (botao) {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    //atribuir ao visor o valor do botao
                    if (visor == "0"||limpaVisor=="Sim") {
                        visor = botao;
                    } else {
                        visor = visor + botao;
                    }
                    limpaVisor = "Nao";
                    break;
                case"+/-":
                    //faz a inversao do valor do visor
                    if (visor.StartsWith('-')) visor = visor.Substring(1);
                    else visor = "-" + visor;
                    break;

                case ",":   
                    if (!visor.Contains(',')) {
                        visor += ",";
                    }
                    break;
                case "+":
                case "-":
                case "X":
                case ":":
                    limpaVisor = "Sim";//marcar o visor como sendo necessario o seu reinicio
                    if (PrimeiroOperador != "Sim") {
                        //esta é a 2º vez ou mais que selecionou um operador
                        double operando1 = Convert.ToDouble(primeiroOperando);
                        double operando2 = Convert.ToDouble(visor);
                        //efetuar operacao aritmetica
                        switch (operador) {
                            case "+":
                                visor = operando1 + operando2 + "";
                                break;
                            case "-":
                                visor = operando1 - operando2 + "";
                                break;
                            case "X":
                                visor = operando1 * operando2 + "";
                                break;
                            case ":":
                                visor = operando1 / operando2 + "";
                                break;

                        }
                        
                    }
                    //armazenar os valores atuais para calculos futuros
                        primeiroOperando = visor;
                        //guardar valor
                        operador = botao;
                    if (botao != "=") PrimeiroOperador = "Nao";
                    else PrimeiroOperador = "Sim";

                    break;
                case "C":
                    visor = "0";
                    PrimeiroOperador = "Sim";
                    operador = "";
                    primeiroOperando = "";
                    limpaVisor = "Sim";
                    break;

            }//fim do switch

            //enviar o valor do visor para a view
            ViewBag.Visor = visor;
            ViewBag.PrimeiroOperador = PrimeiroOperador;
            ViewBag.Operador = operador;
            ViewBag.PrimeiroOperando = primeiroOperando;
            ViewBag.LimpaVisor = limpaVisor;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
