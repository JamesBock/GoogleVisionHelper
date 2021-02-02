using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition
{
    public static class Utilities
    {
        public static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        public static int SearchWordCount(TextAnnotation text, string term)
        {
            return text.Pages
                           .SelectMany(p => p.Blocks
                           .SelectMany(b => b.Paragraphs
                           .SelectMany(p => p.Words
                           .Select(w => string.Join("", w.Symbols.Select(s => s.Text))
                           )))).Where(s => s.ToLower() == term).ToList().Count;
        }


        public static IOrderedEnumerable<(string, int)> SearchWordCollection(TextAnnotation text, IEnumerable<string> terms)
        {
            var list = new List<string>();
            foreach (var term in terms)
            {

                list.AddRange(text.Pages
                             .SelectMany(p => p.Blocks
                             .SelectMany(b => b.Paragraphs
                             .SelectMany(p => p.Words
                             .Select(w => string.Join("", w.Symbols.Select(s => s.Text)))
                             ))).Where(s => s.ToLower() == term.ToLower())
                             .ToList());
            }
            var sorted = list.GroupBy(x => x)
                .Select(g => (Value: g.Key, Found: g.Count()))
                .OrderByDescending(x => x.Found);
            return sorted;
        }
        
        public static IEnumerable<(string, float, int)> SearchWordCollectionExp(TextAnnotation text, IEnumerable<string> terms)
        {   var watch = Stopwatch.StartNew();
             var list = new List<(string,float, int)>();
            foreach (var term in terms)
            {

               list.AddRange(text.Pages
                             .SelectMany(p => p.Blocks
                             .SelectMany(b => b.Paragraphs
                             .SelectMany(p => p.Words
                             .Select(w => (Text: string.Join("", w.Symbols.Select(s => s.Text)), Confidence: w.Confidence))
                             ))).Where(str => str.Text.ToLower() == term.ToLower())
                             .GroupBy(x=>x.Text)
                             //.OrderBy(x=>x.Select(c=>c.Confidence))
                             .Select(g=> (Value: (string)g.Key, Confidence: g.Select(p=>p.Confidence).Max(), Found: g.Count())
                             ));
            }
            // var sorted =list.Select(x=> (x.Key.Count(),x. ))
            //     .GroupBy(x => x.Key)
            //     .Select(g => (Value: (string)g.Key, Confidence: g.Key , Found: g.Count()))
            //     .OrderByDescending(x => x.Found);
            watch.Stop();
            System.Console.WriteLine(watch.ElapsedMilliseconds);
            return list;
        }
        
    }
}
