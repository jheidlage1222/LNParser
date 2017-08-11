using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListingFullJSON
{
    public class Rootobject
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
    }

    public class Listinglist
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public Address Address { get; set; }
        public Photo Photo { get; set; }
        public Otherphoto[] OtherPhotos { get; set; }
        public Detail[] Details { get; set; }
        public string PropertyDescription { get; set; }
        public string LocationDescription { get; set; }
        public string[] Highlights { get; set; }
        public Broker Broker { get; set; }
        public Lease Lease { get; set; }
        public Markettrends MarketTrends { get; set; }
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
        public string LicenseNumber { get; set; }
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
        public string LeaseType { get; set; }
        public string DateAvailable { get; set; }
        public string Description { get; set; }
    }

    public class Markettrends
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public Series[] Series { get; set; }
    }

    public class Series
    {
        public string Name { get; set; }
        public string Current { get; set; }
        public string OverMonths { get; set; }
        public string OverYear { get; set; }
    }

    public class Otherphoto
    {
        public string Id { get; set; }
        public string Ext { get; set; }
    }

    public class Detail
    {
        public string Name { get; set; }
        public string[] Value { get; set; }
    }
}
