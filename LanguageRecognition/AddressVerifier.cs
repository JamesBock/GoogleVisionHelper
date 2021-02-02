using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace LanguageRecognition
{
    public static class AddressVerficationService
    {
        //static string? address1;
        //static string? address2;
        //static string? city;
        //static string? state;
        //static string? zip5;
        //static string? zip4;

        //?API=Verify&XML=<AddressValidateRequest USERID=""737TENAN6421""><Address ID=""0"">
        const string URIBASE = @"https://secure.shippingapis.com/ShippingAPI.dll";
        public async static Task<AddressValidateResponse> VerifyAddressAsync(Address address)
        {
            var serializer = new XmlSerializer(typeof(AddressValidateResponse));
            var client = new HttpClient() { };

            var body = new Dictionary<string, string>
            {
                { "API", "Verify"},
                { "XML", $@"<AddressValidateRequest USERID = ""737TENAN6421""><Address ID = ""0""><Address1>{address.Line2}</Address1><Address2>{address.Line1}</Address2><City>{address.City}</City><State>{address.State}</State><Zip5>{address.PostalCode}</Zip5><Zip4></Zip4></Address></AddressValidateRequest>"}
            };


            var content = new FormUrlEncodedContent(body.AsEnumerable()!);

            var response = await client.PostAsync(URIBASE, content);

            var stream= await response.Content.ReadAsStreamAsync();
           
            var addressSerial = (AddressValidateResponse)serializer.Deserialize(stream);
            Console.WriteLine(stream);
            return addressSerial;
        }
    }
}

