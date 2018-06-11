using System;

namespace ZuulCS
{
    public class Player
    {
        private Room _currentRoom;
        private int health;
        private uint damage;

        public Room currentRoom {
            get { return _currentRoom; }
            set { _currentRoom = value; }
        }

        private void Damage() { 

        }
    }
}
