using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopNetReporting
{
    /// <summary>
    /// This is the root node
    /// </summary>
    public class ListingListRoot
    {
        public Listinglist[] ListingList { get; set; }
    }
    /// <summary>
    /// This is a single Listing
    /// </summary>
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
        public Logo Logo { get; set; }
        public string LicenseNumber { get; set; }
    }

    public class Logo
    {
        public string Id { get; set; }
        public string Ext { get; set; }
    }
    /// <summary>
    /// This a struct with the indivdual lease/spot
    /// and an array of Spaces.  And a Lease type
    /// </summary>
    public class Lease
    {
        public string SpaceType { get; set; }
        public Space[] Spaces { get; set; }
    }
    /// <summary>
    /// This is the actual "Lease" itself.
    /// </summary>
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
    //
    public class JsonDataGetter
    {
        private Listinglist source;
        public JsonDataGetter(Listinglist listRoot)
        {
            source = listRoot;
        }
        /// <summary>
        /// May be deprecated.  
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            throw new NotImplementedException();
        }

    }

}

