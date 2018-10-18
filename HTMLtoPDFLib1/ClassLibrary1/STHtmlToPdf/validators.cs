using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace STHtmlToPdf.STHtmlToPdf
{
    public static class STValidateBytes
    {
        public static bool IsImage(string p_fileName)
        {
            using (FileStream stream = File.Open(p_fileName, FileMode.Open))
            {
                stream.Seek(0, SeekOrigin.Begin);
                List<string> jpg = new List<string> { "FF", "D8" };
                List<string> bmp = new List<string> { "42", "4D" };
                List<string> gif = new List<string> { "47", "49", "46" };
                List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
                List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

                List<string> bytesIterated = new List<string>();

                for (int i = 0; i < 8; i++)
                {
                    string bit = stream.ReadByte().ToString("X2");
                    bytesIterated.Add(bit);

                    bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                    if (isImage)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public static bool IsImage(byte[] inbytes)
        {
            using (MemoryStream stream = new MemoryStream(inbytes))
            {
                stream.Seek(0, SeekOrigin.Begin);
                List<string> jpg = new List<string> { "FF", "D8" };
                List<string> bmp = new List<string> { "42", "4D" };
                List<string> gif = new List<string> { "47", "49", "46" };
                List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
                List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

                List<string> bytesIterated = new List<string>();

                for (int i = 0; i < 8; i++)
                {
                    string bit = stream.ReadByte().ToString("X2");
                    bytesIterated.Add(bit);

                    bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                    if (isImage)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public static bool IsPdf(string p_fileName)
        {
            var pdfString = "%PDF-";
            var pdfBytes = Encoding.ASCII.GetBytes(pdfString);
            var len = pdfBytes.Length;
            var buf = new byte[len];
            var remaining = len;
            var pos = 0;
            using (var f = File.OpenRead(p_fileName))
            {
                while (remaining > 0)
                {
                    var amtRead = f.Read(buf, pos, remaining);
                    if (amtRead == 0) return false;
                    remaining -= amtRead;
                    pos += amtRead;
                }
            }
            return pdfBytes.SequenceEqual(buf);
        }
        public static bool IsPdf(byte[] inBytes)
        {
            var pdfString = "%PDF-";
            var pdfBytes = Encoding.ASCII.GetBytes(pdfString);
            var len = pdfBytes.Length;
            var buf = new byte[len];
            var remaining = len;
            var pos = 0;
            using (var MStream = new MemoryStream(inBytes))
            {
                MStream.Seek(0, SeekOrigin.Begin);
                while (remaining > 0)
                {
                    var amtRead = MStream.Read(buf, pos, remaining);
                    if (amtRead == 0) return false;
                    remaining -= amtRead;
                    pos += amtRead;
                }
            }
            return pdfBytes.SequenceEqual(buf);
        }
    }
}