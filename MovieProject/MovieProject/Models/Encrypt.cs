using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MovieProject.Models {
    public class Encrypt {
        //Salt Hash
        public static string GeneratePassword(int length)
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";

            var rnd = new Random();
            var chars = new char[length];
            var allowedCharCount = allowedChars.Length;

            for (var i = 0; i <= length - 1; i++)
            {
                chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * rnd.NextDouble())];
            }

            return new string(chars);
        }

        //Encrypt Password
        public static string EncodePassword(string pass, string salt)   
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];

            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");

            return EncodePasswordMd5(Convert.ToBase64String(sha1.ComputeHash(dst)));
        }

        //Encrypt with Md5
        public static string EncodePasswordMd5(string pass)   
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[]  originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
               
            return BitConverter.ToString(encodedBytes);
        }
    }
}