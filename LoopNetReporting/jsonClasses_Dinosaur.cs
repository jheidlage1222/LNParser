using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopNetReportingJSON
{

    public class Rootobject
    {
        public string ID { get; set; }
        public Option[] Options { get; set; }
        public Features Features { get; set; }
        public Data Data { get; set; }
    }

    public class Features
    {
        public Map[] Maps { get; set; }
    }

    public class Map
    {
        public string JsonPath { get; set; }
        public int Provider { get; set; }
        public int Type { get; set; }
    }

    public class Data
    {
        public Report Report { get; set; }
        public Listinglist[] ListingList { get; set; }
    }

    public class Report
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string PhotoUriRoot { get; set; }
        public Creator Creator { get; set; }
    }

    public class Creator
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public Logo Logo { get; set; }
    }

    public class Logo
    {
        public string Id { get; set; }
        public string Ext { get; set; }
    }

    public class Listinglist
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public Address Address { get; set; }
        public Photo Photo { get; set; }
        public Detail[] Details { get; set; }
        public string Status { get; set; }
        public string PropertyDescription { get; set; }
        public Broker Broker { get; set; }
        public Lease Lease { get; set; }
    }

    public class Address
    {
        public string CountryCode { get; set; }
        public string StateProvName { get; set; }
        public string StateProvCode { get; set; }
        public string CityName { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public Geopoint Geopoint { get; set; }
    }

    public class Geopoint
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class Photo
    {
        public string Id { get; set; }
        public string Ext { get; set; }
    }

    public class Broker
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public Logo1 Logo { get; set; }
        public string LicenseNumber { get; set; }
    }

    public class Logo1
    {
        public string Id { get; set; }
        public string Ext { get; set; }
    }

    public class Lease
    {
        public string SpaceType { get; set; }
        public Space[] Spaces { get; set; }
    }

    public class Space
    {
        public string Number { get; set; }
        public string SpaceAvailable { get; set; }
        public string RentalRateMo { get; set; }
        public string RentalRate { get; set; }
        public string MinDivisible { get; set; }
        public string MaxContiguous { get; set; }
        public string LeaseType { get; set; }
        public string DateAvailable { get; set; }
        public string Description { get; set; }
        public string Sublease { get; set; }
    }

    public class Detail
    {
        public string Name { get; set; }
        public string[] Value { get; set; }
    }

    public class Option
    {
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public bool ReadOnly { get; set; }
        public int Type { get; set; }
    }

}
