using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DevClock
{
    public class ItemDashboard
    {

       
        public FlowLayoutPanel setObject(EventHandler btnChange_Click, Item i)
        {
            FlowLayoutPanel flowItem = new FlowLayoutPanel();
            FlowLayoutPanel flowDate = new FlowLayoutPanel();
            PictureBox picProgram = new PictureBox();
            Button btnChange = new Button();
            DateTimePicker dateTimePicker1 = new DateTimePicker();
            Label lbDecription = new Label();

            // flowLayoutPanel2
            //

            flowItem.BackColor = System.Drawing.Color.ForestGreen;
            flowItem.Controls.Add(flowDate);
            flowItem.Controls.Add(picProgram);
            flowItem.Controls.Add(btnChange);
            flowItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            flowItem.Location = new System.Drawing.Point(3, 3);
            flowItem.Name = "flowLayoutPanel2";
            flowItem.Size = new System.Drawing.Size(361, 70);
            flowItem.TabIndex = 7;
            // 
            // flowLayoutPanel3
            // 
            flowDate.BackColor = System.Drawing.SystemColors.Control;
            flowDate.Controls.Add(dateTimePicker1);
            flowDate.Controls.Add(lbDecription);
            flowDate.Location = new System.Drawing.Point(3, 3);
            flowDate.Name = "flowLayoutPanel3";
            flowDate.Size = new System.Drawing.Size(227, 56);
            flowDate.TabIndex = 7;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarForeColor = System.Drawing.Color.Green;
            dateTimePicker1.CalendarMonthBackground = System.Drawing.SystemColors.HotTrack;
            dateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.Red;
            dateTimePicker1.CalendarTitleForeColor = System.Drawing.Color.DarkCyan;
            dateTimePicker1.CalendarTrailingForeColor = System.Drawing.Color.Yellow;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy HH:mm";
            dateTimePicker1.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new System.Drawing.Point(3, 3);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.RightToLeftLayout = true;
            dateTimePicker1.Size = new System.Drawing.Size(224, 28);
            dateTimePicker1.TabIndex = 7;
            dateTimePicker1.Value = Convert.ToDateTime(i.dateTime);
            // 

            // picProgram
            // 
            picProgram.Image = global::DevClock.Properties.Resources.Default;
            picProgram.Location = new System.Drawing.Point(236, 3);
            picProgram.Name = "picProgram";
            picProgram.Size = new System.Drawing.Size(58, 56);
            picProgram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            picProgram.TabIndex = 8;
            picProgram.TabStop = false;
            // 
            // btnChange
            // 
            btnChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            btnChange.ForeColor = System.Drawing.Color.White;
            btnChange.Location = new System.Drawing.Point(300, 3);
            btnChange.Name = i.id;
            btnChange.Size = new System.Drawing.Size(58, 56);
            btnChange.TabIndex = 9;
            btnChange.Text = "Change";
            btnChange.UseVisualStyleBackColor = false;
            btnChange.Click += new System.EventHandler(btnChange_Click);
            // 
            // lbDecription
            // 
            lbDecription.AutoSize = true;
            lbDecription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbDecription.Location = new System.Drawing.Point(3, 34);
            //lbDecription.Name = "lbDecription";
            lbDecription.Size = new System.Drawing.Size(115, 17);
            lbDecription.TabIndex = 8;
            lbDecription.Text = i.decription;


            // flowItem
            // 
            flowItem.BackColor = System.Drawing.Color.ForestGreen;
            flowItem.Controls.Add(flowDate);
            flowItem.Controls.Add(picProgram);
            flowItem.Controls.Add(btnChange);
            flowItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            flowItem.Location = new System.Drawing.Point(3, 3);
            flowItem.Name = "flowItem";
            flowItem.Size = new System.Drawing.Size(361, 70);
            flowItem.TabIndex = 7;
            // 
            // flowDate
            // 
            flowDate.BackColor = System.Drawing.SystemColors.Control;
            flowDate.Controls.Add(dateTimePicker1);
            flowDate.Controls.Add(lbDecription);
            flowDate.Location = new System.Drawing.Point(3, 3);
            flowDate.Name = "flowDate";
            flowDate.Size = new System.Drawing.Size(227, 56);
            flowDate.TabIndex = 7;
            try
            {
                picProgram.Image = Image.FromFile(Application.StartupPath + "\\Pics\\"+i.program+".png");
            }
            catch
            {
                try
                {
                    picProgram.Image = Image.FromFile(Application.StartupPath + "\\Pics\\" + i.program + ".jpg");
                }
                catch { }
            }
            return flowItem;
        }
    }
}
