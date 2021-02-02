using System.Xml.Serialization;

namespace LanguageRecognition
{
    [XmlRoot("AddressValidateResponse")]
    public class AddressValidateResponse{
        [XmlElement("Address")]
        public Address Address { get; set; }
         
    }

    public class Address
    {
  
        public int Id { get; set; }
        //public string Street { get; set; } = null!;
        //public string StreetNumber { get; set; } = null!;
        //public string StreetName { get; set; } = null!;
        [XmlElement(ElementName = "Address2")]
        public string Line1 { get; set; }
        [XmlElement(ElementName = "Address1")]
        public string Line2 { get; set; }
        //public string Line3 { get; set; }
        //public string Line4 { get; set; }
        [XmlElement(ElementName = "City")]
        public string City { get; set; }
        [XmlElement(ElementName = "State")]
        public string State { get; set; }
        [XmlElement(ElementName = "Zip5")]
        public string PostalCode { get; set; }
        [XmlElement("ReturnText")]
        public string ReturnText { get; set; }
    }
}
