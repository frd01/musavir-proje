using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using Tacmin.Core;
using Tacmin.Core.Interface;

namespace Tacmin.Service
{
    public class EncryptionService : IEncryptionService
    {
        private readonly Encoding encoding = Encoding.UTF8;

        public string Encrypt(string plainText)
        {
            return Encrypt(plainText, Engine.Settings.key);
        }

        public string Encrypt(string plainText, string key)
        {
            try
            {
                var aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                aes.Key = encoding.GetBytes(key);
                aes.GenerateIV();

                var AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                var buffer = encoding.GetBytes(plainText);

                var encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));

                var mac = "";

                mac = BitConverter.ToString(HmacSHA256(Convert.ToBase64String(aes.IV) + encryptedText, key)).Replace("-", "").ToLower();

                var keyValues = new Dictionary<string, object>
                {
                    { "iv", Convert.ToBase64String(aes.IV) },
                    { "value", encryptedText },
                    { "mac", mac },
                };

                var serializer = new JavaScriptSerializer();

                return Convert.ToBase64String(encoding.GetBytes(serializer.Serialize(keyValues)));
            }
            catch (Exception e)
            {
                throw new Exception("Error encrypting: " + e.Message);
            }
        }

        public string Decrypt(string plainText)
        {
            return Decrypt(plainText, Engine.Settings.key);
        }

        public string Decrypt(string plainText, string key)
        {
            try
            {
                var aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.Key = encoding.GetBytes(key);

                // Base 64 decode
                var base64Decoded = Convert.FromBase64String(plainText);
                var base64DecodedStr = encoding.GetString(base64Decoded);

                // JSON Decode base64Str
                var ser = new JavaScriptSerializer();
                var payload = ser.Deserialize<Dictionary<string, string>>(base64DecodedStr);

                aes.IV = Convert.FromBase64String(payload["iv"]);

                var AESDecrypt = aes.CreateDecryptor(aes.Key, aes.IV);
                var buffer = Convert.FromBase64String(payload["value"]);

                return encoding.GetString(AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception e)
            {
                throw new Exception("Error decrypting: " + e.Message);
            }
        }

        private byte[] HmacSHA256(String data, String key)
        {
            using (var hmac = new HMACSHA256(encoding.GetBytes(key)))
            {
                return hmac.ComputeHash(encoding.GetBytes(data));
            }
        }
    }
}
