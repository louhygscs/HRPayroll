using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ERP.Common
{
    public static class SecurityHelper
    {
        /// <summary>
        /// ENCRYPT INPUT STRING WITH RijndaelManaged ALGORITHM
        /// </summary>
        /// <param name="InputText">Input plain string</param>
        /// <returns>string</returns>    
        public static string EncryptString(string InputText)
        {
            try
            {
                // We are now going to create an instance of the 
                // Rihndael class.  
                RijndaelManaged RijndaelCipher = new RijndaelManaged();

                string Password = SecurityHelper.GetKey();
                // First we need to turn the input strings into a byte array.
                byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
                // We are using salt to make it harder to guess our key
                // using a dictionary attack.
                byte[] S_key = Encoding.ASCII.GetBytes(Password.Length.ToString());
                // The (Secret Key) will be generated from the specified 
                // password and salt.
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, S_key);
                // Create a encryptor from the existing SecretKey bytes.
                // We use 32 bytes for the secret key 
                ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                // Create a MemoryStream that is going to hold the encrypted bytes 
                MemoryStream memoryStream = new MemoryStream();
                // Create a CryptoStream through which we are going to be processing our data. 
                // use of  write mode for encryption
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
                // Start the encryption process.
                cryptoStream.Write(PlainText, 0, PlainText.Length);
                // Finish encrypting.
                cryptoStream.FlushFinalBlock();
                // Convert our encrypted data from a memoryStream into a byte array.
                byte[] CipherBytes = memoryStream.ToArray();
                // Close both streams.

                memoryStream.Close();
                cryptoStream.Close();

                string EncryptedData = Convert.ToBase64String(CipherBytes);
                return EncryptedData;
            }
            catch { return ""; }
        }

        /// DECRYPT INPUT STRING WHICH IS IN RijndaelManaged FORMAT
        /// </summary>
        /// <param name="InputText">Encrypted string</param>
        /// <returns>string</returns>      
        public static string DecryptString(string InputText)
        {
            InputText = InputText.Replace(" ", "+");
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string Password = SecurityHelper.GetKey();
            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            // Create a decryptor from the existing SecretKey bytes.
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(EncryptedData);

            // Create a CryptoStream. (always use Read mode for decryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold EncryptedData;
            // DecryptedData is never longer than EncryptedData.
            byte[] PlainText = new byte[EncryptedData.Length];

            // Start decrypting.
            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

            // Return decrypted string.   
            return DecryptedData;
        }

        /// <summary>
        /// READ ENCRYPTION KEY
        /// </summary>
        /// <returns>string</returns>     
        private static string GetKey()
        {
           return "B25373951A129AC8CC8FD7070E55070DA310A8654CDD2B83E0D533EBFA449437";
        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public static string CreateRandomString(int p_Length)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[p_Length];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < p_Length; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
}
