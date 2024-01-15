using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CommonUtilities
{
    public class FileHelper
    {
        public static string DefaultElectionFileName { get; set; } = "electionDefault.png";
        public static string DefaultCandidateFileName { get; set; } = "candidateDefault.jpg";
        public static string DefaultElectionFilePath { get; set; } = $"/images/{DefaultElectionFileName}";
        public static string DefaultCandidateFilePath { get; set; } = $"/images/{DefaultCandidateFileName}";
        public static string DefaultDirectory { get; set; } = "wwwroot/images";

        public static string SaveImage(IFormFile image, string path = null)
        {
            var fileName = image.FileName;
            var uniqueFileName = GetUniqueFileName(fileName);
            var imagePath = path ?? Path.Combine(DefaultDirectory, uniqueFileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            return uniqueFileName;
        }

        public static void DeleteImage(string filePath)
        {
            string path = "wwwroot/" + filePath;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private static string GetUniqueFileName(string fileName)
        {
            return Guid.NewGuid().ToString() + "_" + fileName;
        }
    }
}
