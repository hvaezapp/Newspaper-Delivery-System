using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace NDS.Utility
{
    public class HashEncryption
    {
        public static string PasswordEncode(string Data)
        {
            return SHA256Hash(MD5Hash(Data));
        }
        
        public static string MD5Hash(string Data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public static string MD5FileHash(string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (MD5 md5 = new MD5CryptoServiceProvider())
                {
                    byte[] hash = md5.ComputeHash(stream);
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in hash)
                    {
                        builder.AppendFormat("{0:x2}", b);
                    }
                    return builder.ToString();
                }
            }
        }

        public static string SHA256Hash(string Data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public static string SHA384Hash(string Data)
        {
            SHA384 sha = new SHA384Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public static string SHA512Hash(string Data)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}