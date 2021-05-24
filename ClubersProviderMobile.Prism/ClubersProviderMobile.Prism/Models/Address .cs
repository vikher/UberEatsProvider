using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public MexicoStates State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string FullAddress => $"{Street} {City} {State} {Country} {PostalCode}";

    }

    public enum MexicoStates
    {
        Aguascalientes,
        BajaCalifornia,
        BajaCaliforniaSur,
        Campeche,
        Chiapas,
        CiudaddeMéxico,
        Chihuahua,
        Coahuila,
        Colima,
        Durango,
        Guanajuato,
        Guerrero,
        Hidalgo,
        Jalisco,
        México,
        Michoacán,
        Morelos,
        Nayarit,
        NuevoLeón,
        Oaxaca,
        Puebla,
        Querétaro,
        QuintanaRoo,
        SanLuisPotosí,
        Sinaloa,
        Sonora,
        Tabasco,
        Tamaulipas,
        Tlaxcala,
        Veracruz,
        Yucatán, 
        Zacatecas
    }
}