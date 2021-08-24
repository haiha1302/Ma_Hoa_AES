using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Ma_Hoa_AES
{
    class AES
    {
        public AES()
        { }

        // Mã hóa mảng byte bằng khóa và vector khởi tạo
        private byte[] Encrypt(byte[] data, byte[] Key, byte[] IV)
        { 
            MemoryStream memoryStream = new MemoryStream(); // Tạo bộ nhớ
            Rijndael newRijndael = Rijndael.Create(); // Tạo một đối tượng để thực hiện thuật toán 
            newRijndael.Key = Key;
            newRijndael.IV = IV;

            // Tạo luồng lưu file mã hóa
            CryptoStream cryptoStream = new CryptoStream(memoryStream, newRijndael.CreateEncryptor(), CryptoStreamMode.Write);

            // Ghi dữ liệu vào cryptoStream
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.Close();

            // Chuyển về dạng array
            byte[] encryptedData = memoryStream.ToArray();
            return encryptedData;
        }

        public string Encrypt(string Data, string Password)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(Data); // Chuyển string sang bytes

            // Tạo khóa đối xứng từ chuỗi password
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x00, 0x01, 0x02, 0x1C, 0x1D, 0x1E, 0x03, 0x04, 0x05, 0x0F, 0x20 });
            
            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(16), pdb.GetBytes(16));

            // Chuyển từ array thành chuỗi
            return Convert.ToBase64String(encryptedData);
        }

        private byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream memoryStream = new MemoryStream();
            Rijndael newRijndael = Rijndael.Create();
            newRijndael.Key = Key;
            newRijndael.IV = IV;

            // Tạo luồng lưu file giải mã
            CryptoStream cryptoStream = new CryptoStream(memoryStream, newRijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherData, 0, cipherData.Length);
            cryptoStream.Close();

            // Chuyển về dạng array
            byte[] decryptedData = memoryStream.ToArray();
            return decryptedData;
        }
 
        public string Decrypt(string Data, string Password)
        {
            byte[] cipherBytes = Convert.FromBase64String(Data);

            // Tạo khóa đối xứng từ chuỗi password
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x00, 0x01, 0x02, 0x1C, 0x1D, 0x1E, 0x03, 0x04, 0x05, 0x0F, 0x20 });
            
            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(16), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }
    }
}
