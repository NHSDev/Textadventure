using System;
using System.Collections.Generic;

namespace ZuulCS
{


    public class Inventory
    {
        private Dictionary<string, Item> items;

        internal Dictionary<string, Item> Items { get => items; }

        public Inventory()
        {
            items = new Dictionary<string, Item>();
        }


        public void addItem(string tag, Item item)
        {
            this.items.Add(tag, item);
        }


        public Item getItem(string name)
        {
            if (items.ContainsKey(name))
            {
                return (Item)items[name];
            }
            else
            {
                return null;
            }
        }


        public string displayItems()
        {
            string returnstring = "List of items:";

            //Dictionary List
            int number = 0;
            foreach (string key in items.Keys)
            {
                if (number != 0 && number != items.Count)
                {
                    returnstring += ",";
                }
                number++;
                returnstring += " " + key;
            }
            return returnstring;
        }


        public void removeItem(string tag, Item item)
        {
            this.items.Remove(tag, out item);
        }
    }
}
