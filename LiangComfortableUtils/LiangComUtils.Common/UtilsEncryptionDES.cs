using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LiangComUtils.Common
{
    /// <summary>
    /// DES加密解密算法。
    /// </summary>
    public sealed class UtilsEncryptionDES
    {
        private UtilsEncryptionDES()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private static string keydefault = "hehe";

        /// <summary>
        /// 对称加密解密的密钥
        /// </summary>
        public static string KeyDefault
        {
            get
            {
                return keydefault;
            }
            set
            {
                keydefault = value;
            }
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string DesEncrypt(string encryptString)
        {
            return DesEncrypt(encryptString, keydefault);
        }

        /// <summary>
        /// DES加密,自定义Key
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DesEncrypt(string encryptString,string key)
        {
            string stringRepair = "00000000";
            //判断密钥长度,不够八位,就补齐
            if (key.Length < 8)
            {
                key = key + stringRepair.Substring(0, 8 - key.Length);
            }
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string DesDecrypt(string decryptString)
        {
            return DesDecrypt(decryptString, keydefault);
        }

        /// <summary>
        /// DES解密,自定义key
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DesDecrypt(string decryptString, string key)
        {
            //判断密钥长度,不够八位,就补齐
            if (key.Length < 8)
            {
                string stringRepair = "00000000";
                key = key + stringRepair.Substring(0, 8 - key.Length);
            }
            string result = string.Empty;
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] keyIV = keyBytes;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                result = Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception eDesDecryptFailed)
            {

            }
            return result;
        }
    }
}