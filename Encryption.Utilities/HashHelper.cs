using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Utilities
{
    public class HashHelper
    {
        public const int SALT_SIZE = 32; // size in bytes
        public const int HASH_SIZE = 32; // size in bytes
        public const int ITERATIONS = 10000; // number of pbkdf2 iterations

        public static PBKDF2Hash CreatePBKDF2Hash(string input, int iterations = ITERATIONS, int saltSize = SALT_SIZE)
        {
            byte[] salt = GenerateSalt(saltSize);
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, iterations))
            {

                return new PBKDF2Hash
                {
                    Hash = pbkdf2.GetBytes(HASH_SIZE),
                    Salt = salt,
                    Iterations = iterations,
                };
            }
        }

        public static byte[] GenerateSalt(int saltSize = SALT_SIZE)
        {
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[saltSize];
                provider.GetBytes(salt);
                return salt;
            }
        }


        public static byte[] ComputePBKDF2Hash(string input, byte[] salt, int iterations = ITERATIONS, int hashSize= HASH_SIZE )
        {
            // Generate the hash
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, iterations);
            return pbkdf2.GetBytes(hashSize);
        }

        public class PBKDF2Hash
        {
            public byte[] Salt { get; set; }

            public byte[] Hash { get; set; }

            public int Iterations { get; set; }
        }

        public string AES_Decrypt(string input, string pass)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            string decrypted = "";

            try
            {
                byte[] hash = new byte[32];
                byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass));

                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);

                AES.Key = hash;
                AES.Mode = CipherMode.ECB;
                AES.Padding = PaddingMode.Zeros;

                ICryptoTransform DESDecrypter = AES.CreateDecryptor();
                byte[] Buffer = Convert.FromBase64String(input);

                decrypted = ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));

                return decrypted;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string AES_Encrypt(string input, string pass)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            string encrypted = "";

            try
            {
                byte[] hash = new byte[32];
                byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass));

                Array.Copy(temp, 0, hash, 0, 16);
                Array.Copy(temp, 0, hash, 15, 16);

                AES.Key = hash;
                AES.Mode = CipherMode.ECB;
                AES.Padding = PaddingMode.Zeros;

                ICryptoTransform DESEncrypter = AES.CreateEncryptor();
                byte[] Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(input);

                encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));

                return encrypted;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
