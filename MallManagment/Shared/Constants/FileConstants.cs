using MallManagment.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Shared.Constants
{
    public class FileConstants
    {
        public static string USER_ID = "userId.png";
        public static int MAX_FILE_SIZE = 5242880; // Maximum file size allowed for upload is 5 mb
        public static int MAX_IMAGE_SIZE = 2097152; // Maximum Profile images size allowed for upload is 2 mb
        public static string DEFAULT_PROFILE_IMAGE = Path.Combine("StaticFiles", "App", "profile.png");
        private static string USER_FILES_PATH = Path.Combine("StaticFiles", "Users");

        public static string GetUserFileDirectory(string id)
        {
            return Path.Combine(USER_FILES_PATH, id);
        }

        public static string GetImageContent(FileData fileData)
        {
            return $"data:{fileData.DataType};base64,{fileData.FileBase64data}";
        }
    }
}
