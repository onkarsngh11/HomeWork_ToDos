using System;

namespace HomeWork_ToDos.CommonLib.Helpers
{
    public static class CommonHelper
    {
        /// <summary>
        /// Encodes password to base64
        /// </summary>
        /// <param name="password"></param>
        /// <returns> encodeed password. </returns>
        public static string EncodePasswordToBase64(string password)
        {
            byte[] encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
    }
}

