﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Chat_Library.Controller
{
    public class Base64ImageConverter
    {
        // Converts an image to a base64 string.
        public static string imageToString(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[].
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String.
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        // Converts a base64 string to an image.
        public static Image stringToImage(string base64String)
        {
            // Convert Base64 String to byte[].
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image.
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}
