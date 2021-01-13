using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public class Class1
    {
        public static string Encrypt(string plainText)
        {
            byte[] BytesArray = Encoding.ASCII.GetBytes(plainText);
            string encryptedText = "";

            foreach (byte digit in BytesArray)
            {
                encryptedText += digit;
            }

            return encryptedText;
        }

        public static string Decrypt(string encryptedText)
        {
            string decryptedText = "";
            IEnumerable<string> array = Enumerable.Range(0, encryptedText.Length / 3).Select(i => encryptedText.Substring(i * 3, 3));
            List<byte> bytesArray = new List<byte>();
            string[] stringArray = array.ToArray();

            int[] integerArray = Array.ConvertAll(stringArray, int.Parse);

            foreach (int num in integerArray)
            {
                bytesArray.Add((byte)num);
            }

            decryptedText = Encoding.Default.GetString(bytesArray.ToArray<byte>());
            return decryptedText;
        }
    }
}

