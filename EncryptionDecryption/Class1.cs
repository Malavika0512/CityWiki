using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionDecryption
{
    public class Class1
    {
        //A seed binary array for encryption
        static byte[] seed = ASCIIEncoding.ASCII.GetBytes("cse445Group80");
        // encryption using DES
        public static string Encrypt(string plainString) 
        {
            if (string.IsNullOrEmpty(plainString))
            {
                throw new ArgumentNullException("The input string for encryption cannont be empty or null");
            }
            //Lib class
            SymmetricAlgorithm sa = DES.Create(); 
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, sa.CreateEncryptor(seed, seed), CryptoStreamMode.Write);
            StreamWriter streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(plainString);
            // flush the string terminator
            streamWriter.Flush();  
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }
        // decryption using DES
        public static string Decrypt(string encryptedString)   
        {
            if (string.IsNullOrEmpty(encryptedString))
            {
                throw new ArgumentNullException("The string for decryption cannot be empty or null");
            }
            SymmetricAlgorithm sa = DES.Create();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, sa.CreateDecryptor(seed, seed), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadLine();
        }

    }
}
