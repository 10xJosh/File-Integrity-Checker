using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.IO.Hashing;


namespace FileIntegrityChecker
{
    internal class HashingAlgorithms
    {
        private static string HashResult { get; set; }

        public static void GetMD5Hash(string path)
        {

            try
            {
                using (FileStream sourceStream = File.Open(path, FileMode.Open))
                {
                    var MD5hash = MD5.Create();
                    var result = MD5hash.ComputeHash(sourceStream);
                    HashResult = BitConverter.ToString(result).Replace("-", "");
                    sourceStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void GetSHA1Hash(string path)
        {

            try
            {
                using (FileStream sourceStream = File.Open(path, FileMode.Open))
                {
                    var sHA1Hash = SHA1.Create();
                    var result = sHA1Hash.ComputeHash(sourceStream);
                    HashResult = BitConverter.ToString(result).Replace("-", "");
                    sourceStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void GetSHA256Hash(string path)
        {

            try
            {
                using (FileStream sourceStream = File.Open(path, FileMode.Open))
                {
                    var sHA256Hash = SHA256.Create();
                    var result = sHA256Hash.ComputeHash(sourceStream);
                    HashResult = BitConverter.ToString(result).Replace("-", "");
                    sourceStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void GetSHA512Hash(string path)
        {
            try
            {
                using (FileStream sourceStream = File.Open(path, FileMode.Open))
                {
                    var sHA512Hash = SHA512.Create();
                    var result = sHA512Hash.ComputeHash(sourceStream);
                    HashResult = BitConverter.ToString(result).Replace("-", "");
                    sourceStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void GetCRC32Hash(string path)
        {
            string hash = "";

            try
            {
                using (FileStream sourceStream = File.Open(path, FileMode.Open))
                {
                    string[] orderedHash = new string[8];

                    var test = ToByteArray(sourceStream);
                    var results = Crc32.Hash(test);

                    foreach (byte b in results)
                    {
                        hash += ReverseCRC32Output(b.ToString("X2"));
                    }

                    //Another Reverse is required to get accurate input due to Little Endian format
                    HashResult = ReverseCRC32Output(hash);
                    sourceStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string GetHashResult()
        {
            return HashResult;
        }

        private static byte[] ToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                ms.Dispose();
                return ms.ToArray();
            }
        }

        private static string ReverseCRC32Output(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
