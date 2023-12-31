using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CommonUtilities
    {
        public static string SaveImage(IFormFile image,  string path = null)
        {
            var fileName = image.FileName;
            var uniqueFileName = GetUniqueFileName(fileName);
            var imagePath = path ?? Path.Combine("wwwroot/images", uniqueFileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            return uniqueFileName;
        }

        private static string GetUniqueFileName(string fileName)
        {
            return Guid.NewGuid().ToString() + "_" + fileName;
        }
    }
}
