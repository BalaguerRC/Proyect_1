using System.Security.Cryptography;
using System.Text;

namespace Api_User.Data
{
    public class Encryp
    {
        //private string EncryptionKey
        public string Encrypting(string password)
        {
            string EncryptionKey = "test";
            byte[] Bytes= Encoding.Unicode.GetBytes(password);
            using(Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(Bytes, 0, Bytes.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;
        }
        public string DesEncrypting(string desEnpassword)
        {
            string EncryptionKey = "test";
            byte[] Bytes= Convert.FromBase64String(desEnpassword);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(Bytes, 0, Bytes.Length);
                        cs.Close();
                    }
                    desEnpassword = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return desEnpassword;
        }
    }
}
