using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary.Utility
{
    public class Utility
    {
        public static void WriteToFile(string text)
        {
            StreamWriter writer = null;
            try
            {
                string path = ConfigurationManager.AppSettings["LOG_FILE"];
                using (writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public static byte[] GetFileHash(string fileName)
        {
            HashAlgorithm sha1 = HashAlgorithm.Create();
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                return sha1.ComputeHash(stream);
        }

        public static bool compareFileHash(byte[] tmpHash, byte[] tmpNewHash)
        {
            bool bEqual = false;
            if (tmpHash == null || tmpNewHash == null)
                return bEqual;
            if (tmpNewHash.Length == tmpHash.Length)
            {
                int i = 0;
                while ((i < tmpNewHash.Length) && (tmpNewHash[i] == tmpHash[i]))
                {
                    i += 1;
                }
                if (i == tmpNewHash.Length)
                {
                    bEqual = true;
                }
            }
            return bEqual;
        }

        public static bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

      

    }
}
