using System;

namespace ZuulCS
{
    public class Player
    {
        private Room _currentRoom;
        private int health;
        private Inventory inventory;
        internal Inventory Inventory { get => inventory; }

        public Player()
        {
            inventory = new Inventory();
            health = 10;
        }


        public int getHealth
        {
            get { return health; }
        }


        public Room currentRoom {
            get { return _currentRoom; }
            set { _currentRoom = value; }
        }

        public int damage(int amount) {
            this.health -= amount;
            return this.health;
        }

        public int heal(int amount)
        {
            this.health += amount;
            return this.health;
        }

        public bool isAlive()
        {
            if(health < 1)
            {
                return false;
            } else
            {
                return true;
            }
        }


        public void setItem(string name, Item item)
        {
            inventory.addItem(name, item);
        }

        public void removeItem(string name, Item item)
        {
            inventory.removeItem(name, item);
        }

    }
}
