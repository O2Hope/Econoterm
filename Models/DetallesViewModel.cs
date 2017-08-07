using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Econoterm.Models.Identity;

namespace Econoterm.Models
{



public class DetallesViewModel
{

    public ArrayList Lista { get; set; } 
    public int TransfMax { get; set; }
    public bool tuberia { get; set; }
    public string TransLetra { get; set; }
}

}