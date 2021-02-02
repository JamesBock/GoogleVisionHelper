using Google.Cloud.Vision.V1;
using LanguageRecognition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition
{
    public static class OCRMethods
    {
        public static async Task GetTextAsync(string uri)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var image = Image.FromUri(uri);
            var client = ImageAnnotatorClient.Create();
            var text = await client.DetectDocumentTextAsync(image);
            var searchTerms = new string[] { "Lead", "1978", "Protection", "EPA's", "Phoenix" };
            Console.WriteLine($"Text: {text.Text}");
            Console.WriteLine(text.Pages.Select(p => p.Confidence.ToString()));

            var symbolsText = text.Pages
                            .SelectMany(p => p.Blocks
                            .SelectMany(b => b.Paragraphs
                            .SelectMany(p => p.Words
                            .SelectMany(w => string.Join("", w.Symbols
                            .Select(s => s.Text))))))
                            ;
            Console.WriteLine($"symbolsText: {symbolsText}");


            //var tenantBlcok = new Block();
            //tenantBlcok.BoundingBox.Vertices
            var sorted = symbolsText.GroupBy(x => x)
                .Select(g => new { Value = g.Key, Found = g.Count() })
                .OrderByDescending(x => x.Found);

            foreach (var page in text.Pages)
            {
                foreach (var block in page.Blocks)
                {
                    string box = string.Join(" - ", block.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                    Console.WriteLine($"Block {block.BlockType} at {box}");
                    foreach (var paragraph in block.Paragraphs)
                    {
                        box = string.Join(" - ", paragraph.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                        Console.WriteLine($"  Paragraph at {box}");
                        foreach (var word in paragraph.Words)
                        {
                            Console.WriteLine($"    Word: {string.Join("", word.Symbols.Select(s => s.Text))}");
                        }
                    }
                }
            }
            using (var fs = File.Create(Path.Combine(path, $@"output\Text{DateTime.Now.Ticks}.txt")))
            {
                Utilities.AddText(fs, $"Text: {text.Text}");
                foreach (var thing in sorted)
                {
                    Utilities.AddText(fs, $"{thing.Value} appeared {thing.Found}\n");
                }
                Utilities.AddText(fs, "\n\n\n *******************End of symbols********************\n\n\n");

                Utilities.AddText(fs, $"texas appeared {Utilities.SearchWordCount(text, "texas")} times \n\n\n");

                var keywords = Utilities.SearchWordCollection(text, searchTerms);

                Utilities.AddText(fs, $"*************Sensitive keywords found*********\n\n");
                foreach (var item in keywords)
                {
                    Utilities.AddText(fs, $"{item.Item1} found {item.Item2} times\n");
                }
                var keywordsExp = Utilities.SearchWordCollectionExp(text, searchTerms );

                Utilities.AddText(fs, $"*************Experiment keywords found*********\n\n");
                foreach (var item in keywordsExp)
                {
                    Utilities.AddText(fs, $"{item.Item1} found {item.Item3} times with {item.Item2} confidence\n");
                }

            }

            using (var fs = File.Create(Path.Combine(path , $@"output\Paragraphs{DateTime.Now.Ticks}.txt")))
            {

                foreach (var page in text.Pages)
                {
                    foreach (var block in page.Blocks)
                    {
                        string box = string.Join(" - ", block.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                        Utilities.AddText(fs, $"Block {block.BlockType} at {box}");
                        Utilities.AddText(fs, "\n");
                        foreach (var paragraph in block.Paragraphs)
                        {
                            box = string.Join(" - ", paragraph.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                            Utilities.AddText(fs, $"  Paragraph at {box}");
                            Utilities.AddText(fs, "\n");
                            foreach (var word in paragraph.Words)
                            {
                                Utilities.AddText(fs, $"    Word: {string.Join("", word.Symbols.Select(s => s.Text))}");
                                Utilities.AddText(fs, "\n");
                            }
                        }
                    }
                }

            }



            Console.ReadLine();
        }
    }
}