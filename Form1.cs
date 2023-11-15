using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace RSA_Security_System
{
    public partial class Form1 : Form
    {
        private string key = "3356249871195716"; // 16 characters for 128 bits

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Implement your logic for the textBox1_TextChanged event, if needed
        }

        private void KeyTextBox_TextChanged(object sender, EventArgs e)
        {
            key = KeyTextBox.Text;
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string encryptedFilePath = Path.Combine(Path.GetDirectoryName(filePath), "encrypted_" + Path.GetFileName(filePath));

                EncryptFile(filePath, encryptedFilePath);

                MessageBox.Show("File encrypted successfully!");
            }
        }

        private void EncryptFile(string inputFilePath, string outputFilePath)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.Key = Encoding.UTF8.GetBytes(key);
                rijndael.Mode = CipherMode.ECB;

                ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

                using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open))
                using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                {
                    inputFileStream.CopyTo(cryptoStream);
                }
            }
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string decryptedFilePath = Path.Combine(Path.GetDirectoryName(filePath), "decrypted_" + Path.GetFileName(filePath));

                DecryptFile(filePath, decryptedFilePath);

                MessageBox.Show("File decrypted successfully!");
            }
        }

        private void DecryptFile(string inputFilePath, string outputFilePath)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.Key = Encoding.UTF8.GetBytes(key);
                rijndael.Mode = CipherMode.ECB;

                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open))
                using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, decryptor, CryptoStreamMode.Write))
                {
                    inputFileStream.CopyTo(cryptoStream);
                }
            }
        }
    }
}
