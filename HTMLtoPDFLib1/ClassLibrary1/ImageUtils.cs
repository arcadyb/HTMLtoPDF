using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STHtmlToPdf
{
    public class ImageUtils
    {
        public  static byte[] GetBytesFromImage(String imageFile)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                System.Drawing.Image img = System.Drawing.Image.FromFile(imageFile);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return ms.ToArray();
            }
            catch (Exception)
            {

                return null; 
            }
        }
        public static List<byte[]> FromDirectory(string directorypath,bool includSubdirs)
        {
            List<byte[]> outlist= new List<byte[]>();
            try
            {

                if (System.IO.Directory.Exists(directorypath))
                {
                    DirectoryInfo oOriginal = new DirectoryInfo(directorypath);
                    foreach (FileInfo oFile in oOriginal.GetFiles("*", includSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)) 
                    {
                        byte[] bytes = GetBytesFromImage(oFile.FullName);
                        if (bytes != null)
                            outlist.Add(bytes);
                    }//foreach
                }
            }
            catch (Exception e)
            {
               
            }//catch
            return outlist;
        }
    }
}
