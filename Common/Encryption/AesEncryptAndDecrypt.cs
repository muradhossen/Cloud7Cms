using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common.Encryption
{
    /// <summary>
    /// login encrypt & decrypt process.
    /// </summary>
    
    public static class AesEncryptAndDecrypt
    {
        public static string TokenEncryptKey = "aSdghT54Rwwlzxcvbnmqwertyuiop123456";

        public static string GetEncryptedStringData(string data)
        {
            string EncryptedData = EncryptString(TokenEncryptKey, data);
            return EncryptedData;
        }

        private static string EncryptString(string key, string plainText)
        {
            var rnd = new Random();
            var iv = new byte[16];
            rnd.NextBytes(iv);
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                //base64IV = Convert.ToBase64String(iv);
                aes.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array) + ":" + Convert.ToBase64String(iv);
        }

        //public static string DecryptString(string key, string encryptedText)
        //{
        //    string requestIv = encryptedText[(encryptedText.LastIndexOf(":") + 1)..];
        //    string substringText = encryptedText[..(encryptedText.LastIndexOf(":"))];
        //    byte[] iv = Convert.FromBase64String(requestIv);
        //    byte[] buffer = Convert.FromBase64String(substringText);

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes(key);
        //        aes.IV = Convert.FromBase64String(requestIv);// iv;
        //        aes.Mode = CipherMode.CBC;
        //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        //        using (MemoryStream memoryStream = new MemoryStream(buffer))
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader streamReader = new StreamReader(cryptoStream))
        //                {
        //                    return streamReader.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}
        static string secretkey = "secretKeyb1DEYmhXrTYiyU65EWI8U1h";
        static string ivSecret = "ivSec1HJFhYrhcr5";
        public static string GetEncryptedObjectData(object data)
        {
            string json = JsonConvert.SerializeObject(data);
            string EncryptedData = EncryptString(secretkey, ivSecret, json);
            return EncryptedData;
        }

        public static string EncryptString(string secretkey, string ivSecret, string plainText)
        {
            byte[] cipherData;
            Aes aes = Aes.Create();
            var by = Encoding.UTF8.GetBytes(secretkey);
            aes.Key = by;
            var iv = Encoding.ASCII.GetBytes(ivSecret);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform cipher = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, cipher, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                        sw.Write(plainText);
                }
                cipherData = ms.ToArray();
            }
            string cTxt = Convert.ToBase64String(cipherData);
            return cTxt;
        }

        public static string DecryptionString(string encryptedText)
        {
            try
            {
                string plainText;
                byte[] cipherText = Convert.FromBase64String(encryptedText);
                Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(secretkey);
                var iv = new byte[16];
                iv = Encoding.ASCII.GetBytes(ivSecret);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            plainText = sr.ReadToEnd();
                        }
                    }

                    return plainText;
                }
            }
            catch(Exception e)
            {
                return "";
            }
        }
    }
}
