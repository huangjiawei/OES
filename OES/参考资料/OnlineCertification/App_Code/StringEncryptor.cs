using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SecurityLib
{
  public static class StringEncryptor
  {
    public static string EncryptASCII(string sourceData)
    {
      // set key and initialization vector values
      byte[] key = new byte[] { 11, 24, 73, 84,12, 6,27, 84 };
      byte[] iv = new byte[] { 51, 62, 83, 24, 25, 46, 57, 58 };
      try
      {
        // convert data to byte array
        byte[] sourceDataBytes =
           System.Text.ASCIIEncoding.ASCII.GetBytes(sourceData);

        // get target memory stream
        MemoryStream tempStream = new MemoryStream();

        // get encryptor and encryption stream
        DESCryptoServiceProvider encryptor =
           new DESCryptoServiceProvider();
        CryptoStream encryptionStream =
           new CryptoStream(tempStream,
              encryptor.CreateEncryptor(key, iv),
              CryptoStreamMode.Write);

        // encrypt data
        encryptionStream.Write(sourceDataBytes, 0,
           sourceDataBytes.Length);
        encryptionStream.FlushFinalBlock();

        // put data into byte array
        byte[] encryptedDataBytes = tempStream.GetBuffer();

        // convert encrypted data into string
        return Convert.ToBase64String(encryptedDataBytes, 0,
           (int)tempStream.Length);
      }
      catch
      {
        throw new StringEncryptorException(
           "Unable to encrypt data.");
      }
    }

    public static string DecryptASCII(string sourceData)
    {
      // set key and initialization vector values
        byte[] key = new byte[] { 11, 24, 73, 84, 12, 6, 27, 84 };
        byte[] iv = new byte[] { 51, 62, 83, 24, 25, 46, 57, 58 };
      try
      {
        // convert data to byte array
        byte[] encryptedDataBytes =
           Convert.FromBase64String(sourceData);
          

        // get source memory stream and fill it 
        MemoryStream tempStream =
           new MemoryStream(encryptedDataBytes, 0,
              encryptedDataBytes.Length);

        // get decryptor and decryption stream 
        DESCryptoServiceProvider decryptor =
           new DESCryptoServiceProvider();
        CryptoStream decryptionStream =
           new CryptoStream(tempStream,
              decryptor.CreateDecryptor(key, iv),
              CryptoStreamMode.Read);

        // decrypt data 
        StreamReader allDataReader =
           new StreamReader(decryptionStream);
        return allDataReader.ReadToEnd();
      }
      catch
      {
        throw new StringEncryptorException(
           "Unable to decrypt data.");
      }
    }

    public static string EncryptUTF(string plainText)
    {

        string PasswordHash = "34DFDS#$^DF324w0rd";
        string SaltKey = "DFA@LKJFG(*&_lkEY";
        string VIKey = "@1B2^%hc3D4e5F6g7H8";
        try
        {

            byte[] plainTextBytes = Encoding.UTF32.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }

            return Convert.ToBase64String(cipherTextBytes);
        }
        catch (Exception e)
        {
            Utilities.LogError(e);
            return "NULL";
        }
    }
    public static string DecryptUTF(string encryptedText)
    {

        string PasswordHash = "34DFDS#$^DF324w0rd";
        string SaltKey = "DFA@LKJFG(*&_lkEY";
        string VIKey = "@1B2^%hc3D4e5F6g7H8";
        try
        {


            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF32.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        catch (Exception e)
        {
            Utilities.LogError(e);
            return "NULL";
        }
    }
  }
}