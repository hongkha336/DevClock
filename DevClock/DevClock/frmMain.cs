using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevClock
{
    public partial class frmMain : Form
    {

        ItemDashboard obj = new ItemDashboard();
        ItemHelper objHelper = new ItemHelper();
        List<Item> listMyItems = new List<Item>();
        PcProgramHelper pcHelper = new PcProgramHelper();

        public frmMain()
        {
            InitializeComponent();
        }


        private void loadDateTime()
        {
            String str = DateTime.Now.ToString("HH:mm:ss");
            lbTimer.Text = str;
            lbDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            lbTimer.Left = this.Width / 2 - lbTimer.Width / 2;
            lbDate.Left = this.Width / 2 - lbDate.Width / 2;
        }

        private void Runtimer_Tick(object sender, EventArgs e)
        {
            loadDateTime();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadDateTime();
            Runtimer.Start();
            toCenter();
            loadTimeTableData();
            Checking_OpenTime.Start();
        }


        private void toCenter()
        {
            lbDashboard.Left = this.Width / 2 - lbDashboard.Width / 2;
        }



        public void loadTimeTableData()
        {
            flowDashBoard.Controls.Clear();
            listMyItems = objHelper.getListItems();
            foreach (Item i in listMyItems)
            {
                flowDashBoard.Controls.Add(obj.setObject(btnChange_Click, i));
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            String idIssues = btn.Name.ToString();
            frmManage frm = new frmManage(objHelper.getIssuesById(idIssues));
            frm.ShowDialog();
            loadTimeTableData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmManage f = new frmManage();
            f.ShowDialog();
            loadTimeTableData();
        }


        private bool isChangeingList = false;
        private void Checking_OpenTime_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (Item i in listMyItems)
                {
                    try
                    {
                        TODO_Item(i);
                    }
                    catch
                    { }
                }
            }
            catch { }
            if (isChangeingList)
            {
                isChangeingList = false;
                objHelper.RewriteListTask(listMyItems);
                loadTimeTableData();
            }
            
        }


        private void TODO_Item(Item item)
        {
            int i = 0;
            if (lbDate.Text.Equals(Convert.ToDateTime(item.dateTime).ToString("dddd, dd MMMM yyyy")))
                i++;
            if (Convert.ToDateTime(lbTimer.Text).ToString("HH:mm").Equals(Convert.ToDateTime(item.dateTime).ToString("HH:mm")))
                i++;
            if (i == 2)
            {
                // Mở app
                try
                {
                    System.Diagnostics.Process.Start(item.program);
                }
                catch
                {
                    System.Diagnostics.Process.Start(pcHelper.getLinkByName(item.program));

                }

                System.Media.SoundPlayer player = new System.Media.SoundPlayer("sound/ding.wav");
                player.Play();

                listMyItems.Remove(item);
                isChangeingList = true;
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
    }
}
