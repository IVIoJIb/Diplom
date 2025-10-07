using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmExpertAPI.Models
{
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Manufacturer { get; set; }
    public string Dosage { get; set; }
    public string ActiveSubstance { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool RequiresPrescription { get; set; } 
}

}
