using System;
using System.Net.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace LanguageRecognition
{


    class TwilioFunctions
    {
        public TwilioFunctions()
        {
            var client = new HttpClient();
            client.PostAsync("")


            // Find your Account Sid and Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
         

        }
        public void PrintTwilMessage()
        {
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            TwilioClient.Init(accountSid, authToken);

            var incomingPhoneNumber = IncomingPhoneNumberResource.Create(
                phoneNumber: new Twilio.Types.PhoneNumber("+15005550006"),
                voiceUrl: new Uri("http://demo.twilio.com/docs/voice.xml"));

            var message = MessageResource.Create(
                body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                from: new Twilio.Types.PhoneNumber("+15005550006"),
                to: new Twilio.Types.PhoneNumber("+14126136129")
            );
            Console.WriteLine(message.Body);
            Console.WriteLine(message.To);
            Console.WriteLine(message.Direction);
            Console.WriteLine(message.ApiVersion);
        }

    }
}