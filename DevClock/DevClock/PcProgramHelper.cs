using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevClock
{
    public class PcProgramHelper
    {
        public List<PcProgram> getListProgram()
        {
            List<PcProgram> listApps = new List<PcProgram>();
            string[] lines = File.ReadAllLines("bin/apps.inf");
            int mark = 0;
            PcProgram item = new PcProgram();
            foreach (string str in lines)
            {
                if (mark < 2)
                {
                    switch (mark)
                    {
                        case 0:
                            item.name = str;
                            break;
                        case 1:
                            item.link = str;
                            break;
                        
                    }
                    mark++;
                }
                else
                {
                    mark = 0;
                    listApps.Add(item);
                    item = new PcProgram();
                    item.name = str;
                    mark++;
                }
            }
            if (mark == 2)
                listApps.Add(item);
            return listApps;
        }


        public String getLinkByName(String Name)
        {
            List<PcProgram> ml = getListProgram();
            foreach (PcProgram PC in ml)
            {
                if (PC.name.Equals(Name))
                    return PC.link;
            }
            return "-1";
        }
    }
}
