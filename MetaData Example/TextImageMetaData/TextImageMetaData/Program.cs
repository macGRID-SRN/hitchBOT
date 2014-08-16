using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace TextImageMetaData
{
    class Program
    {
        static void Main(string[] args)
        {
            Image myImage = Image.FromFile("bHAGGcn.jpg");

            PropertyItem myProps = myImage.GetPropertyItem(306);

            myProps.Value = ASCIIEncoding.ASCII.GetBytes(DateTime.Now.AddDays(3).ToString());
            myProps.Len = myProps.Value.Length;
            myProps.Id = 0x9003;

            myImage.SetPropertyItem(myProps);

            myImage.Save("out.jpg");

            Console.ReadKey(true);
        }
    }
}
