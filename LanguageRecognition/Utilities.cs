using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
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
                           .Select(w => string.Join("", w.Symbols
                           .Select(s => s.Text)
                           ))))).Where(s => s.ToLower() == term).ToList().Count;
        }
    }
}
