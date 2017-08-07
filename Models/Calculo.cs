using System;
using System.ComponentModel.DataAnnotations;
using Econoterm.Models.Identity;

namespace Econoterm.Models
{

    public enum FormayPosicion
    {
        Deposito,
        Esfera,
        Pared,
        Tapa,
        [Display(Name = "Tuberia Horizontal")]
        Tuberia1,
        [Display(Name = "Tuberia Vertical")]
        Tuberia2
    }

    public enum ProductosList
    {
        [Display(Name = "COLCHA DE FIBRA MINERAL ROLAN® CP/CA - 96 ")]
        CPCA96,
        [Display(Name = "COLCHA DE FIBRA MINERAL ROLAN® CP/CA - 144 ")]
        CPCA144,
        [Display(Name = "COLCHA DE FIBRA MINERAL ROLAN® CP/CA - 192")]
        CPCA192,
        [Display(Name = "TERMOAISLANTE PREFORMADO DE FIBRA MINERAL ROLAN®")]
        TERMOAISLANTE,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 32")]
        FF32,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 48")]
        FF48,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 64")]
        FF64,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 96")]
        FF96,
        [Display(Name = "PLACA TERMOAISLANTE DE FIBRA MINERAL ROLAN® FF 128")]
        FF128

    }

    public class Resultado
    {

        public string Espesor { get; set; }
        public string Flux { get; set; }
        public string SupMaxima { get; set; }

    }

    public class Calculo 
    {

        public int Id {get; set;}

        [Required]
        public ProductosList Aislante {get; set;}

        [Required]
        public FormayPosicion Forma {get; set;}

        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Valor invalido")] 
        [Display(Name = "Diametro (mm)")]
       
		public double Diametro { get; set; }
		
        [Required(ErrorMessage="Este campo es obligatorio")] 
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Valor invalido")]
        [Display(Name = "Temperatura ambiental (°C)")]
        public double Ambiental { get; set; }
		
        [Required(ErrorMessage="Este campo es obligatorio")] 
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Valor invalido")]
        [Display(Name = "Temperatura superficial (°C)")]
        public double Superficial { get; set; }
		
        [Required(ErrorMessage="Este campo es obligatorio")] 
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Valor invalido")]
        [Display(Name = "Temperatura Operacion (°C)")]
        public double Operacion { get; set; }
		
        [Required(ErrorMessage="Este campo es obligatorio")] 
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Valor invalido")]
        [Display(Name="Velocidad del viento km/hr")]
        public double Viento { get; set; }
		
        [Required(ErrorMessage="Este campo es obligatorio")] 
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Valor invalido")]
        public double Emisividad { get; set; }

        public bool Ener { get; set; } 
        public bool Astm { get; set; }
		
        public ApplicationUser User {get; set;}

        public Calculo() 
        {
        }
    }
}
