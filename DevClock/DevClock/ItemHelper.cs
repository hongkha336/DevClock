using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevClock
{
    class ItemHelper
    {

        public void RewriteListTask(List<Item> listItem)
        {
            String filepath = "data/data.inf";
            FileStream fs = new FileStream(filepath, FileMode.Create);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);

            foreach (Item i in listItem)
            {
                sWriter.WriteLine(i.id);
                sWriter.WriteLine(i.dateTime);
                sWriter.WriteLine(i.decription);
                sWriter.WriteLine(i.program);
            }

            sWriter.Flush();
            fs.Close();
        }

        public List<Item> getListItems()
        {
            List<Item> listItems = new List<Item>();
            string[] lines = File.ReadAllLines("data/data.inf");
            int mark = 0;
            Item item = new Item();
            foreach (string str in lines)
            {
                if (mark < 4)
                {
                    switch (mark)
                    {
                        case 0:
                            item.id = str;
                            break;
                        case 1:
                            item.dateTime = str;
                            break;
                        case 2:
                            item.decription = str;
                            break;
                        case 3:
                            item.program = str;
                            break;
                    }
                    mark++;
                }
                else
                {
                    mark = 0;
                    listItems.Add(item);
                    item = new Item();
                    item.id = str;
                    mark++;
                }
            }
            if (mark == 4)
                listItems.Add(item);
            return listItems;
        }


        public Item getIssuesById(String id)
        {
            List<Item> myList = getListItems();
            foreach (Item item in myList)
            {
                if (item.id.Equals(id))
                    return item;
            }
            return null;
        }


        public void replaceItem(Item Newitem)
        {
            List<Item> myList = getListItems();
            Item old = getIssuesById(Newitem.id);
            myList = Remove(old, myList);
            myList.Add(Newitem);
            RewriteListTask(myList);
            Sort();

        }

        public void deleteItem(Item Newitem)
        {
            List<Item> myList = getListItems();
            Item old = getIssuesById(Newitem.id);
            myList = Remove(old, myList);
            //myList.Add(Newitem);
            RewriteListTask(myList);
            Sort();

        }

        public void Sort()
        {
            List<Item> myList = getListItems();
            for (int k = 0; k < myList.Count; k++)

                for (int i = 0; i<myList.Count-1; i++)
                for(int j =i+1; j<myList.Count; j++)
                    if(Convert.ToDateTime(myList[i].dateTime) > Convert.ToDateTime(myList[j].dateTime))
                    {
                        Item t = myList[i];
                        myList[i] = myList[j];
                        myList[j] = t;
                    }
            RewriteListTask(myList);
        }

        private List<Item> Remove(Item i, List<Item> myList)
        {

            int index = 0;
            for(index = 0; index < myList.Count; index ++)
            {
                if (myList[index].id.Equals(i.id))
                    break;
            }
            myList.RemoveAt(index);
            return myList;

        }
    }
}
