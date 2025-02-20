using EShop.Application.Utilities.Convertors;
using EShop.Application.Utilities.Extensions.Upload;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Utilities.Extensions
{
    public static class UploadFileExtension
    {


        public static bool AddFileToServer(this IFormFile file, string fileName, string originalPath, string deleteFileName = null)
        {
            // Validate the uploaded file type  
            if (file != null && (file.IsImage() || file.IsPdf()))
            {
                // Ensure the original directory exists  
                if (!Directory.Exists(originalPath))
                {
                    Directory.CreateDirectory(originalPath);
                }

                // Delete existing file if specified  
                if (!string.IsNullOrEmpty(deleteFileName) && File.Exists(Path.Combine(originalPath, deleteFileName)))
                {
                    File.Delete(Path.Combine(originalPath, deleteFileName));
                }

                // Prepare the path for the new file  
                string originalFilePath = Path.Combine(originalPath, fileName);

                // Save the file to the specified path  
                using (var stream = new FileStream(originalFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return true; // Return true indicating the file was uploaded successfully  
            }

            return false; // Return false if the file fails validation  
        }

        public static void DeleteFile(this string fileName, string originalPath)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(Path.Combine(originalPath, fileName)))
            {
                File.Delete(Path.Combine(originalPath, fileName));
            }
        }


        public static bool AddImageToTheServer(this IFormFile image, string fileName, string orginalPath, int? width, int? height, string thumbPath = null, string deletefileName = null)
        {
            if (image != null && image.IsImage())
            {
                if (!Directory.Exists(orginalPath))
                    Directory.CreateDirectory(orginalPath);

                if (!string.IsNullOrEmpty(deletefileName))
                {
                    if (File.Exists(orginalPath + deletefileName))
                        File.Delete(orginalPath + deletefileName);

                    if (!string.IsNullOrEmpty(thumbPath))
                    {
                        if (File.Exists(thumbPath + deletefileName))
                            File.Delete(thumbPath + deletefileName);
                    }
                }

                string OriginPath = orginalPath + fileName;

                using (var stream = new FileStream(OriginPath, FileMode.Create))
                {
                    if (!Directory.Exists(OriginPath)) image.CopyTo(stream);
                }


                if (!string.IsNullOrEmpty(thumbPath))
                {
                    if (!Directory.Exists(thumbPath))
                        Directory.CreateDirectory(thumbPath);

                    ImageOptimizer resizer = new ImageOptimizer();

                    if (width != null && height != null)
                        resizer.ImageResizer(orginalPath + fileName, thumbPath + fileName, width, height);
                }
                return true;

            }
            return false;
        }
        public static bool AddVideoFileToServer(this IFormFile videoFile, string fileName, string orginalPath, string deletefileName = null)
        {
            if (videoFile != null && videoFile.IsFile())
            {
                if (!Directory.Exists(orginalPath))
                    Directory.CreateDirectory(orginalPath);

                if (!string.IsNullOrEmpty(deletefileName))
                {
                    if (File.Exists(orginalPath + deletefileName))
                        File.Delete(orginalPath + deletefileName);
                }

                string OriginPath = orginalPath + fileName;

                using (var stream = new FileStream(OriginPath, FileMode.Create))
                {
                    if (!Directory.Exists(OriginPath)) videoFile.CopyTo(stream);
                }

                return true;

            }
            return false;
        }


        public static void DeleteImage(this string imageName, string OriginPath, string ThumbPath)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                if (File.Exists(OriginPath + imageName))
                    File.Delete(OriginPath + imageName);

                if (!string.IsNullOrEmpty(ThumbPath))
                {
                    if (File.Exists(ThumbPath + imageName))
                        File.Delete(ThumbPath + imageName);
                }
            }
        }
    }

}

