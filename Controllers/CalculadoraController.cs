using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Econoterm.Models;
using Microsoft.AspNetCore.Mvc;
using Econoterm.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Econoterm.Controllers
{
    public class CalculadoraController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public CalculadoraController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int CalculoTransferencia(double Diametro, double temperatura, bool superficie)
        {

            temperatura = temperatura - 274;

            int[] matrizTemperatura = new int[] { 60, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650 };
            int[] matrizDiametro = new int[] { 13, 19, 25, 38, 51, 64, 76, 102, 127, 152, 203, 254, 305, 356, 406, 457, 508, 559, 610, 660, 711, 762, 10000 };

            int[,] matrizTransferencia = new int[,] { {6,12,19,29,37,49,66,75,88,97,111,125,141 }, {7,13,21,32,41,50,68,82,96,105,121,136,153},
                                                    {8,15,24,36,46,56,75,90,101,116,133,142,160}, {10,18,29,40,50,67,85,101,119,130,148,168,188 },
                                                    {12,21,33,45,57,70,95,113,119,138,157,178,200 }, {13,24,37,50,63,77,104,116,130,150,172,194,218 },
                                                    {16,26,43,57,72,87,109,131,145,168,192,207,232}, {19,28,44,60,77,102,119,143,167,194,210,238,267 },
                                                    {17,30,54,70,84,101,126,153,170,202,214,253,279}, {20,34,58,79,95,114,134,162,174,211,239,266,310 },
                                                    {33,43,65,94,104,138,161,171,208,234,267,315,349 },{40,51,77,100,123,148,174,200,243,276,311,347,405 },
                                                    {47,59,89,115,141,169,198,227,276,293,351,391,433 },{51,64,96,125,152,182,213,244,291,314,355,419,463 },
                                                    {58,72,108,140,169,203,237,271,306,347,392,438,511 }, {57,80,120,154,187,224,260,298,336,380,429,479,530 },
                                                    {63,88,132,169,205,245,284,324,365,388,465,519,574 }, {69,97,143,184,222,265,273,315,395,407,502,559,618 },
                                                    {75,105,155,199,240,251,295,339,424,449,494,553,661 }, {82,113,167,214,223,270,325,374,454,479,528,590,705 },
                                                    {88,121,179,229,238,296,346,398,438,510,575,642,694 },{94,129,190,243,261,314,368,422,466,540,605,679,736 },
                                                    {37,49,70,88,91,107,123,126,153,157,175,192,224 } };

            int i = 0;
            int r = 0;

            if (superficie == false)
            {
                while (Convert.ToInt32(temperatura) > matrizTemperatura[i])
                {
                    i++;
                }
                r = 22;
            }

            else
            {
                while (Convert.ToInt32(Diametro) > matrizDiametro[r])
                {
                    r++;
                }

                while (Convert.ToInt32(temperatura) > matrizTemperatura[i])
                {
                    i++;
                }
            }

            return matrizTransferencia[r, i];
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,Aislante,Forma,Diametro,Ambiental,Superficial,Operacion,CSuperficie,Viento,Emisividad,Capa,Espesor,DiametroVisible,CoefForma,Conductividad,TransCalor,Transferencia,User")]Calculo calculo)
        {
            if (ModelState.IsValid)
            {

                var currentUser = User.Identity.Name;
                calculo.User =  _context.User.FirstOrDefault(x => x.UserName == currentUser);
                _context.Add(calculo);
                await _context.SaveChangesAsync();
                return Redirect(Url.Action("Detalles",calculo));

            }

            return View(calculo);
        }
        [HttpGet]
        public ActionResult Detalles(Calculo calculo) 
        {

           // Calculo calculo = _context.Calculos.Where(c => c.Id == Id).SingleOrDefault(); //_context.Set<Calculo>().SingleOrDefault(c => c.Id == Id);


            ArrayList datagri = new ArrayList();
            int transfMax = 0;
            bool tipo = false;

            for (int i = 0; i < 100; i++)
            {
             var ta = calculo.Ambiental + 274;
             var tsup = calculo.Superficial + 274;
             var top = calculo.Operacion + 274;
             double C = (calculo.Diametro <= 0 ) ? 1.79 : 1.016;
             double esp = (6.35 * i) * 0.001 ;
             double kais = 0;
             double V = calculo.Viento *  0.621371;
             double Emss = calculo.Emisividad;
             double Do = calculo.Diametro * 0.001;
             bool key = false;
             

             double ValorA = 0;
             double ValorB = 0;
             double ValorC = 0;
             double TOperacion2 = 0;

            if (calculo.Aislante == ProductosList.CPCA96)
                {
                    ValorA = 0.2187 / 6.935;
                    ValorB = 0.0002148 / 6.935;
                    ValorC = 0.0000006908 / 6.935;

                    TOperacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = ValorA + ValorB * TOperacion2 + ValorC * Math.Pow(TOperacion2, 2);

                }
            if (calculo.Aislante == ProductosList.CPCA144)
                {
                    ValorA = 0.2192 / 6.935;
                    ValorB = 0.0001433 / 6.935;
                    ValorC = 0.0000006312 / 6.935;

                    TOperacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = ValorA + ValorB * TOperacion2 + ValorC * Math.Pow(TOperacion2, 2);

                }
            if (calculo.Aislante == ProductosList.CPCA192)
                {

                    ValorA = 0.2331 / 6.935;
                    ValorB = 0.0000679 / 6.935;
                    ValorC = 0.0000006385 / 6.935;

                    TOperacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = ValorA + ValorB * TOperacion2 + ValorC * Math.Pow(TOperacion2, 2);

                }
            if (calculo.Aislante == ProductosList.TERMOAISLANTE)
                {
                    ValorA = 0.1977/ 6.935;
                    ValorB = 0.0004 / 6.935;
                    ValorC = 0.0000002262 / 6.935;

                    TOperacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = ValorA + ValorB * TOperacion2 + ValorC * Math.Pow(TOperacion2, 2);
                }
            if (calculo.Aislante == ProductosList.FF32)
                {
                    ValorA = 0.212 / 6.935;
                    ValorB = 0.0002696 / 6.935;
                    ValorC = 0.000001367 / 6.935;

                    TOperacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = ValorA + ValorB * TOperacion2 + ValorC * Math.Pow(TOperacion2, 2);

                }
            if (calculo.Aislante == ProductosList.FF48)
                {
                    ValorA = 0.2035 / 6.935;
                    ValorB = 0.000373 / 6.935;
                    ValorC = 0.0000009124 / 6.935;

                    TOperacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = ValorA + ValorB * TOperacion2 + ValorC * Math.Pow(TOperacion2, 2);

                }
            if (calculo.Aislante == ProductosList.FF64)
                {
                    ValorA = 0.2033 / 6.935;
                    ValorB = 0.0003868 / 6.935;
                    ValorC = 0.0000006548 / 6.935;

                    TOperacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = ValorA + ValorB * TOperacion2 + ValorC * Math.Pow(TOperacion2, 2);

                }
            if (calculo.Aislante == ProductosList.FF96)
                {
                    ValorA = 0.2187 / 6.935;
                    ValorB = 0.0002148 / 6.935;
                    ValorC = 0.0000006908 / 6.935;

                    TOperacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = ValorA + ValorB * TOperacion2 + ValorC * Math.Pow(TOperacion2, 2);

                }
            if (calculo.Aislante == ProductosList.FF128)
                {
                    ValorA = 0.2114 / 6.935;
                    ValorB = 0.0002836 / 6.935;
                    ValorC = 0.0000004948 / 6.935;

                    TOperacion2 = ((top + tsup) / 2) * (1.8) + 32;

                    kais = ValorA + ValorB * TOperacion2 + ValorC * Math.Pow(TOperacion2, 2);

                }

            if (C == 1.79)
            {
                transfMax = CalculoTransferencia(Do * 1000, top, false);
            tipo = true;
                while (key == false)
                {

                    double hc = 3.0075 * C * (Math.Pow(1.11 / (tsup + ta - 510.44), 0.181)) * (Math.Pow(1.8 * (tsup - ta), 0.266)) * (Math.Pow(1 + 7.9366 * Math.Pow(10, -4) * V, 0.5));
                    double hr = (0.9824 * Math.Pow(10, -8) * Emss) * (Math.Pow(ta, 4) - Math.Pow(tsup, 4)) / (ta - tsup);
                    double hs = hc + hr;
                    double q = (top - ta) / ((esp / kais) + (1 / hs));
                    double tsc = ta + q / hs;


                    int tsc2 = Convert.ToInt32(tsc);
                    int tsup2 = Convert.ToInt32(tsup);

                    if (tsc2 == tsup2)
                    {
                        key = true;

                        int esp2 = Convert.ToInt32(esp * 1000);
                        string q2 = Convert.ToString(q);


                        int qint = Convert.ToInt32(q);
                        int tscint = Convert.ToInt32(tsc - 274);

                        datagri.Add( new Resultado { Espesor = esp2 +" mm", Flux = qint + " w/m2", SupMaxima = tscint + " °C" });
                        
                    }
                    else
                    {
                        key = false;
                        tsup = tsc;
                    }
                }



            }
            else
            {

            transfMax= CalculoTransferencia(Do * 1000, top, true);
            tipo= false;

                    while (key == false)
                    {
                        double da = Do + 2 * esp;
                        double hc = 2.7241 * C * Math.Pow(da, -0.2) * Math.Pow(1.11 / (tsup + ta - 510.44), 0.181) * Math.Pow(1.8 * (tsup - ta), 0.266) * Math.Pow(1 + 7.9366 * Math.Pow(10, -4) * V, 0.5);
                        double hr = 0.9824 * Math.Pow(10, -8) * Emss * (Math.Pow(ta, 4) - Math.Pow(tsup, 4)) / (ta - tsup);
                        double hs = hc + hr;
                        double q = Math.PI * (top - ta) / (1 / (2 * kais) * Math.Log(da / Do) + 1 / (hs * da));
                        double tsc = top - (q / (2 * Math.PI * kais) * Math.Log(da / Do));
                        

                        int tsc2 = Convert.ToInt32(tsc);
                        int tsup2 = Convert.ToInt32(tsup);
                        if (tsc2 == tsup2)
                        {
                            key = true;
                            tsc += -274;

                            int esp2 = Convert.ToInt32(esp * 1000);

                            int qint = Convert.ToInt32(q);
                            int tscint = Convert.ToInt32(tsc);


                           datagri.Add( new Resultado { Espesor = esp2 +" mm", Flux = qint + " w/m", SupMaxima = tscint + " °C" });
                            

                        }

                        else
                        {
                            key = false;
                            tsup = tsc;

                        }
                    }
                }
                


            }

                var ViewModel = new DetallesViewModel
                {
                Lista = datagri,
                TransfMax = transfMax,
                tuberia = tipo
                };
                ViewData["MyCalculo"] = ViewModel;

            


            return View(ViewModel);
        }
    }
}
