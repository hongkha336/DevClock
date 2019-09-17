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
    public partial class frmManage : Form
    {
        public frmManage()
        {
            InitializeComponent();
            pnOption.Visible = false;
            getProgramSource();
            btnDel.Hide();
        }

        Item tempItem = null;

        public frmManage(Item item)
        {
            InitializeComponent();
            getProgramSource();
            tempItem = item;
            txtDec.Text = item.decription;
            dateTimePicker1.Value = Convert.ToDateTime(item.dateTime);
            dateTimePicker2.Value = Convert.ToDateTime(item.dateTime);
            if(!item.program.Equals("-1"))
            {
                chkOption.CheckState = CheckState.Checked;
                chkOption.Enabled = false;
                dateTimePicker2.Enabled = true;

                pnOption.Visible = true;
                try
                {
                    comboBox1.Text = item.program;
                    button1.Text = "Choose";
                }catch
                {
                    link = item.program;
                    button1.Text = "SET";

                }

            }
        }

        PcProgramHelper Helper = new PcProgramHelper();
        List<String> myResourcelist = new List<string>();
        private void frmManage_Load(object sender, EventArgs e)
        {
            
            
        }


        private void getProgramSource()
        {
            List<PcProgram> list = Helper.getListProgram();
            myResourcelist = new List<string>();
            myResourcelist.Add("Choose a program");
            foreach (PcProgram pc in list)
            {
                myResourcelist.Add(pc.name);
            }
            comboBox1.DataSource = myResourcelist;
        }

        private void chkOption_CheckedChanged(object sender, EventArgs e)
        {
            pnOption.Visible = !pnOption.Visible;
            dateTimePicker2.Enabled = !dateTimePicker2.Enabled;
            label3.Text = "This program will be open at " + dateTimePicker2.Text;
            getProgramSource();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int index = comboBox1.SelectedIndex;
            if(index != 0)
            {
                button1.Text = "Choose";
                link = "";
                string value = myResourcelist[index];
                try
                {
                    pictureBox1.Image = Image.FromFile("pics/" + value + ".png");
                }
                catch
                {
                    try
                    {
                        pictureBox1.Image = Image.FromFile("pics/" + value + ".jpg");
                    }
                    catch
                    {
                        pictureBox1.Image = Image.FromFile("pics/Default.ico");
                    }
                   
                }
            }
            else
            {
                pictureBox1.Image = Image.FromFile("pics/Default.ico");

               
            }

        }


        

        private void btnSave_Click(object sender, EventArgs e)
        {

           

            if (string.IsNullOrWhiteSpace(txtDec.Text))
            {
                MessageBox.Show("Please fill in description box");
                return;
            }

           


            DialogResult tl = MessageBox.Show("Save and close?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.Cancel)
                return;


            if (tempItem != null)
            {
                tempItem.dateTime = dateTimePicker1.Text.ToString() + " " + dateTimePicker2.Text.ToString();
                tempItem.decription = txtDec.Text.Trim();
                if (comboBox1.Visible)
                {
                    if (!comboBox1.SelectedValue.Equals("Choose a program"))
                        tempItem.program = comboBox1.SelectedValue.ToString();
                    else
                    {
                        if (!link.Equals(""))
                        {
                            tempItem.program = link;
                        }
                        else
                            tempItem.program = "-1";
                    }
                }
                else
                    tempItem.program = "-1";

                ItemHelper itemHel = new ItemHelper();
                itemHel.replaceItem(tempItem);



            }
            else
            {

                // Lấy index và ghi index lại
                int index = 0;
                string[] lines = File.ReadAllLines("data/inf.inf");
                if (lines.Count() > 0)
                {
                    index = Convert.ToInt32(lines[0]);
                }
                index++;


                String filepath = "data/data.inf";
                FileStream fs = new FileStream(filepath, FileMode.Append);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 
                sWriter.WriteLine(index.ToString());
                sWriter.WriteLine(dateTimePicker1.Text.ToString() + " " + dateTimePicker2.Text.ToString());
                sWriter.WriteLine(txtDec.Text.ToString().Trim());
                if (comboBox1.Visible)
                {
                    if (!comboBox1.SelectedValue.Equals("Choose a program"))
                        sWriter.WriteLine(comboBox1.SelectedValue);
                    else
                    {
                        if (!link.Equals(""))
                        {
                            sWriter.WriteLine(link);
                        }
                        else
                            sWriter.WriteLine(-1);
                    }
                }
                else
                    sWriter.WriteLine(-1);


                sWriter.Flush();
                fs.Close();


                filepath = "data/inf.inf";
                fs = new FileStream(filepath, FileMode.Create);
                sWriter = new StreamWriter(fs, Encoding.UTF8);
                sWriter.WriteLine(index.ToString());
                sWriter.Flush();
                fs.Close();



                ItemHelper itemHelper = new ItemHelper();
                itemHelper.Sort();

            }
            this.Close();
        }

        private bool isSystemApp = false;

        private void ShowMsg(string msg)
        {
            isSystemApp = false;
            if (msg[0] == '1')
                isSystemApp = true;
            link = msg.Substring(1, msg.Length - 1);
        }

        static string link = "";
        private void button1_Click(object sender, EventArgs e)
        {
            //
            frmAddProgram child = new frmAddProgram();
            child.SendMsg = new frmAddProgram.DelSendMsg(ShowMsg);
            child.ShowDialog();
            button1.Text = "SET";
            getProgramSource();

            if(isSystemApp)
            {
                comboBox1.Text = myResourcelist.Last();
            }
            

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult tl = MessageBox.Show("Delete and close?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.Cancel)
                return;

                ItemHelper itemHel = new ItemHelper();
                itemHel.deleteItem(tempItem);


            this.Close();
            }
        }
    }

