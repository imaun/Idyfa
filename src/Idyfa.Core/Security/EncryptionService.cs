using System.Security.Cryptography;
using System.Text;

namespace Idyfa.Core.Extensions;

public class EncryptionService
{
    public EncryptionService()
    {
        
    }
    
    private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
    {
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
        {
            var toEncrypt = Encoding.Unicode.GetBytes(data);
            cs.Write(toEncrypt, 0, toEncrypt.Length);
            cs.FlushFinalBlock();
        }

        return ms.ToArray();
    }

    private static string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
    {
        using var ms = new MemoryStream(data);
        using var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read);
        using var sr = new StreamReader(cs, Encoding.Unicode);
        return sr.ReadToEnd();
    }
    
    /// <summary>
    /// Create salt key
    /// </summary>
    /// <param name="size">Key size</param>
    /// <returns>Salt key</returns>
    public virtual string CreateSaltKey(int size)
    {
        //generate a cryptographic random number
        using var provider = new RNGCryptoServiceProvider();
        var buff = new byte[size];
        provider.GetBytes(buff);

        // Return a Base64 string representation of the random number
        return Convert.ToBase64String(buff);
    }

    /// <summary>
    /// Create a password hash
    /// </summary>
    /// <param name="password">Password</param>
    /// <param name="saltkey">Salk key</param>
    /// <param name="passwordFormat">Password format (hash algorithm)</param>
    /// <returns>Password hash</returns>
    public virtual string CreatePasswordHash(string password, string saltkey, string passwordFormat)
    {
        //return HashHelper.CreateHash(Encoding.UTF8.GetBytes(string.Concat(password, saltkey)), passwordFormat);
        return "";
    }

    // <summary>
    /// Encrypt text
    /// </summary>
    /// <param name="plainText">Text to encrypt</param>
    /// <param name="encryptionPrivateKey">Encryption private key</param>
    /// <returns>Encrypted text</returns>
    public virtual string EncryptText(string plainText, string encryptionPrivateKey = "")
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        if (string.IsNullOrEmpty(encryptionPrivateKey))
            encryptionPrivateKey = "";  //TODO : read from _securitySettings.EncryptionKey;

        using var provider = new TripleDESCryptoServiceProvider
        {
            Key = Encoding.ASCII.GetBytes(encryptionPrivateKey[0..16]),
            IV = Encoding.ASCII.GetBytes(encryptionPrivateKey[8..16])
        };

        var encryptedBinary = EncryptTextToMemory(plainText, provider.Key, provider.IV);
        return Convert.ToBase64String(encryptedBinary);
    }

    /// <summary>
    /// Decrypt text
    /// </summary>
    /// <param name="cipherText">Text to decrypt</param>
    /// <param name="encryptionPrivateKey">Encryption private key</param>
    /// <returns>Decrypted text</returns>
    public virtual string DecryptText(string cipherText, string encryptionPrivateKey = "")
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        if (string.IsNullOrEmpty(encryptionPrivateKey))
            encryptionPrivateKey = ""; //TODO : add _securitySettings.EncryptionKey;

        using var provider = new TripleDESCryptoServiceProvider
        {
            Key = Encoding.ASCII.GetBytes(encryptionPrivateKey[0..16]),
            IV = Encoding.ASCII.GetBytes(encryptionPrivateKey[8..16])
        };

        var buffer = Convert.FromBase64String(cipherText);
        return DecryptTextFromMemory(buffer, provider.Key, provider.IV);
    }
    
}