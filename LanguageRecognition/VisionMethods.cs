using Google.Cloud.Language.V1;
using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Google.Cloud.Language.V1.AnnotateTextRequest.Types;

namespace LanguageRecognition
{
    public static class VisionMethods
    {
        public async static Task GetLabelsAsync(string title, string uri)
        {
            var image = Image.FromUri(uri);
            var client = ImageAnnotatorClient.Create();
            var safeResponse = await client.DetectSafeSearchAsync(image);
            var response = await client.DetectLabelsAsync(image);
            using (var text = File.Create($@"{Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))}\output\{title}.txt"))
            {
                Utilities.AddText(text, $"Title: {title}\n{uri}\n ");
                Utilities.AddText(text, $"Adult? : {safeResponse.Adult.ToString()} Confidence: {safeResponse.AdultConfidence.ToString("G5")}\n");
                Utilities.AddText(text, $"Spoof? : {safeResponse.Spoof.ToString()} Confidence: {safeResponse.SpoofConfidence.ToString("G6")}\n");
                Utilities.AddText(text, $"Medical? : {safeResponse.Medical.ToString()} Confidence: {safeResponse.MedicalConfidence.ToString("n6")}\n");
                Utilities.AddText(text, $"Violence? :{safeResponse.Violence.ToString()} Confidence: {safeResponse.ViolenceConfidence.ToString("N6")}\n");
                Utilities.AddText(text, $"Racy? :{safeResponse.Racy.ToString()} Confidence: {safeResponse.RacyConfidence.ToString("G6")}\n\r");
                foreach (var annotation in response)
                {
                    if (annotation.Description != null)
                        Utilities.AddText(text, $"Description: {annotation.Description}\nTopicality: {annotation.Topicality}\nScore: {annotation.Score}\n");
                    if (annotation.Description != null)
                        if (annotation.Description != null)
                            Console.WriteLine($"Title of image: {title} \nDescription: {annotation.Description} \nProperties: {annotation.Properties} \nTopicality: {annotation.Topicality} \nScore: {annotation.Score} \n{annotation.Locations} \n{annotation.BoundingPoly} \n{annotation.Locale}");
                }
            }
        }

        public async static Task GetTextAsync(string uri)
        {
            var image = Image.FromUri(uri);
            var visionClient = ImageAnnotatorClient.Create();
            var text = await visionClient.DetectDocumentTextAsync(image);

            Console.WriteLine($"Text: {text.Text}");
            Console.WriteLine(text.Pages.Select(p => p.Confidence.ToString()));

            LanguageServiceClient languageClient = LanguageServiceClient.Create();
            Document document = Document.FromPlainText(text.Text);
            AnnotateTextResponse response = languageClient.AnnotateText(document,
new Features { ExtractSyntax = true, ExtractEntities = true });
            Console.WriteLine($"Detected language: {response.Language}");
            // The Sentences and Tokens properties provide all kinds of information
            // about the parsed text.
            Console.WriteLine($"Number of sentences: {response.Sentences.Count}");
            Console.WriteLine($"Number of tokens: {response.Tokens.Count}");
            Console.WriteLine("Detected entities:");
            foreach (Entity entity in response.Entities)
            {
                Console.WriteLine($"  {entity.Name} ({(int)(entity.Salience * 100)}%)");
            }
        }
    }
}