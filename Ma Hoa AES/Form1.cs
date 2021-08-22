using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ma_Hoa_AES
{
    public partial class Form1 : Form
    {
        AES Aes = new AES();
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            if (inputTextEn.Text != "")
            {
                try
                {
                    txt_Encrypt.Text = Aes.Encrypt(inputTextEn.Text, inputEnKey.Text);
                }
                catch
                {
                    MessageBox.Show("Không Mã Hóa được", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Ô Nội Dung không được rỗng", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            if (inputTextDe.Text != "")
            {
                try
                {
                    txt_Decrypt.Text = Aes.Decrypt(inputTextDe.Text, inputDeKey.Text);
                }
                catch
                {
                    MessageBox.Show("Không Giải Mã được", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Ô Nội Dung không được rỗng", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            inputTextEn.Text = "";
            inputEnKey.Text = "";
            txt_Encrypt.Text = "";
        }

        private void BtnReset1_Click(object sender, EventArgs e)
        {
            inputTextDe.Text = "";
            inputDeKey.Text = "";
            txt_Decrypt.Text = "";
        }
    }
}
