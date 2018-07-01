using System;

namespace ZuulCS
{
    public class Item
    {
        protected string name;
        protected string description;

        public Item()
        {
            description = "Description stuff";
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public virtual void use(object item)
        {
            System.Console.WriteLine("Item::use(item)");
        }
    }
}
