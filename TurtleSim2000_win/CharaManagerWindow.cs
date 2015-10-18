using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TurtleSim2000_Linux
{
    public partial class CharaManagerWindow : Form
    {
        public bool bRESETCM = false;
        public string runStamp = "1";

        public CharaManagerWindow()
        {
            InitializeComponent();
        }



        private void CharaManagerWindow_Load(object sender, EventArgs e)
        {
            
        }

        public void updateActiveCharaList(Chara[] list, int totalProfiles, int[] active)
        {
            lstActiveChara.Items.Clear();

            for (int a = 0; a < active.Length; a++)
            {
                if (active[a] >= 0)
                {
                    for (int c = 0; c < totalProfiles; c++)
                    {
                        if(list[c].getID() == active[a])
                        {
                            lstActiveChara.Items.Add("Slot " + a + ": " + list[c].getName() + " | " + list[c].getID());
                        }
                    }
                }
                else
                {
                    lstActiveChara.Items.Add("Slot " + a + ":");
                }
            }
      
        }

        public void updateCommandsList(string command)
        {
            lstCommands.Items.Add(command);
            if (chkAutoScroll.Checked) lstCommands.SelectedIndex = lstCommands.Items.Count -1;
            if (chkDumpOnExit.Checked)
            {
                if (lstCommands.Items[lstCommands.SelectedIndex].ToString().Contains("Chara Exit:")) btnDumpLog.PerformClick();
            }
        }

        private void btnResetCharaManager_Click(object sender, EventArgs e)
        {
            bRESETCM = true;
        }

        private void btnDumpLog_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/log"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/log");
            }
            
            StreamWriter sW = new StreamWriter(Directory.GetCurrentDirectory() + "/log/charaManager " + runStamp + ".log", true);
            for (int i = 0; i < lstCommands.Items.Count; i++)
            {
                lstCommands.SelectedIndex = i;
                sW.WriteLine(lstCommands.Items[i].ToString());
            }
            sW.WriteLine("-----------------------------------------------------------------------------------------------------------------");
            sW.Close();
        }
    }
}
