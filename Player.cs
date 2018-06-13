using System;

namespace ZuulCS
{
    public class Player
    {
        private Room _currentRoom;
        private int health;

        public Room currentRoom {
            get { return _currentRoom; }
            set { _currentRoom = value; }
        }

        public uint damage(uint amount) {
            this.health -= amount;
            return this.health;
        }

        public uint heal(uint amount)
        {
            this.health += amount;
            return this.health;
        }

        public bool isAlive()
        {
            if(this.health < 1)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
