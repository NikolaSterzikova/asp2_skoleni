using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model;
// Nepotřebujeme
//public class Rootobject
//{
//    public Class1[] Property1 { get; set; }
//}

public class Person // přejmenovali jsme Class1
{
    public int Id { get; set; }
    [MaxLength(15)] // přidali jsme validaci délky jména
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress] // přidali jsme validaci emailu
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address Address { get; set; }
    public Contract[] Contracts { get; set; }
}

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}

public class Contract
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PlateNumber { get; set; }
    public DateTime Signed { get; set; }
    public int CarBrand { get; set; }
    public string HexColor { get; set; }
}
