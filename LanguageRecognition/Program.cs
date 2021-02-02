using Google.Cloud.Language.V1;
using Google.Cloud.Vision.V1;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using static Google.Cloud.Language.V1.AnnotateTextRequest.Types;

namespace LanguageRecognition
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // var addy = await AddressVerficationService.VerifyAddressAsync(new adds.Address(){
            //     City="Greenbelt",
            //     //Line1 = "1434",
            //     Line2 = "6406 Ivy Lane",
            //     State="MD"

            // });

            var bot = new TwilioFunctions();
            bot.PrintTwilMessage();
            // Console.WriteLine(addy.Address.City);
            //await OCRMethods.GetTextAsync("gs://leases-for-basta/New lease_1-2020-06-15T04:45:31.955Z.jpg");
            //Console.ReadLine();
            //await VisionMethods.GetLabelsAsync("Racy_Horror_Pic", "gs://leases-for-basta/adult_test.jpg");
            //await VisionMethods.GetLabelsAsync("Mostly_Wall", "gs://leases-for-basta/Mostly_Wall.jpg");
            //await VisionMethods.GetLabelsAsync("Mostly_Human", "gs://leases-for-basta/Mostly_Human.jpg");
            //await VisionMethods.GetLabelsAsync("Lead_addendum_doc", "gs://leases-for-basta/New lease_1-2020-06-15T04:45:31.955Z.jpg");
            //await VisionMethods.GetLabelsAsync("Lease_Scan", "gs://leases-for-basta/IMG_0283-2020-08-18T00:52:02.273Z.JPG");
            //await VisionMethods.GetLabelsAsync("A_hand", "gs://leases-for-basta/20201028_231309.jpg");
            //await VisionMethods.GetLabelsAsync("Back_of_Toilet", "gs://leases-for-basta/20201028_231538.jpg");
            //await VisionMethods.GetLabelsAsync("Shower_Head", "gs://leases-for-basta/20201028_222816.jpg");
            //await VisionMethods.GetLabelsAsync("Running_Kitchen_Sink", "gs://leases-for-basta/20201028_224310.jpg");
        }
    }
}
