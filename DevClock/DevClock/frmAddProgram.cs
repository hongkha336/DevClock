using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevClock
{
    public partial class frmAddProgram : Form
    {

        public delegate void DelSendMsg(string msg);

        //khạ báo biến kiểu delegate
        public DelSendMsg SendMsg ;

        private String pictureLink = "";
        public frmAddProgram()
        {
            InitializeComponent();
        }
        private void frmAddProgram_Load(object sender, EventArgs e)
        {

        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fdlg.FileName;
            }
        }

        private void btnChoose2_Click(object sender, EventArgs e)
        {

            OpenFileDialog opf = new OpenFileDialog();
            // chose the images type
            opf.Filter = "Choose Image(*.jpg;*.png)|*.jpg;*.png";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                // get the image returned by OpenFileDialog 
                pictureLink = opf.FileName;
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }

            


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            MessageBox.Show("This program/thing will be saved in system");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

          

            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Link is require");
                return;
            }

            if(String.IsNullOrWhiteSpace(textBox1.Text) )
            {
                MessageBox.Show("Name is require ");
                return;
            }

            DialogResult tl = MessageBox.Show("Save and close?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.Cancel)
                return;


            if (checkBox1.Checked)
            {
                String filepath = "bin/apps.inf";
                FileStream fs = new FileStream(filepath, FileMode.Append);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 
                sWriter.WriteLine(textBox2.Text.Trim());
                sWriter.WriteLine(textBox1.Text.Trim());
                sWriter.Flush();
                fs.Close();
            }

            if (pictureBox1.Image != null)
            {
                pictureBox1.Load(pictureLink);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image.Save("pics/"+textBox2.Text.Trim()+".png", System.Drawing.Imaging.ImageFormat.Png);
            }

            if(checkBox1.Checked)
                SendMsg("1"+textBox1.Text);
            else
                SendMsg("0" + textBox1.Text);

            this.Close();
        }
    }
}
