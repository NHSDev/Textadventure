using System;

namespace ZuulCS
{
	public class Game
	{
		private Parser parser;
        private Player player;

		public Game ()
		{
            player = new Player();
            createRooms();
			parser = new Parser();
		}


		private void createRooms()
		{
            Room entrance,cave001,cave002;
            Item apple;

			// create the rooms
			entrance = new Room("I fell down into this cave, there's is no climbing back up from this!");
            cave001 = new Room("you walk down a dimly lit cave, you see a turn to the left.");
            cave002 = new Room("");

            //create the items
            apple = new Apple();


            // initialise room exits
            entrance.setExit("north", cave001);

            cave001.setExit("west", cave002);

            //add items to room
            entrance.addItem("apple", apple);

			player.currentRoom = entrance;  // start game outside
		}

        public void takeItem(Command command)
        {
            if(!command.hasSecondWord())
            {
                if(player.currentRoom.Inventory.Items.Count < 0)
                {
                    Item currentItem = player.currentRoom.Inventory.Items;
                    player.currentRoom.Inventory.addItem(player.Inventory, currentItem.name);
                }
            }
        }

        public void takeItem(string name, Item item)
        {
            player.Inventory.addItem(name, item);
            //(itemroom).Inventory.removeitem(name, item);
        }

        public void dropItem(string name, Item item)
        {
            player.Inventory.removeItem(name, item);
            //(itemroom).Inventory.addItem(name, item);
        }

        /**
	     *  Main play routine.  Loops until end of play.
	     */
        public void play()
		{
			printWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.
            
			bool finished = false;
			while (! finished) {
                Command command = parser.getCommand();
                finished = processCommand(command);
                bool alive = player.isAlive();
                if (!alive)
                {
                    finished = true;
                }
				
			}
			Console.WriteLine("Thank you for playing.");
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
            Console.WriteLine("");
            Console.WriteLine("01111001 11010010 00100000 01001111 00000111 11100000 ");
            Console.WriteLine("");
            Console.WriteLine("00011101 00010101 00100011 00111000 11100001");
            Console.WriteLine("00001100                            00001101");
            Console.WriteLine("00001100      Welcome Player-77     00001101");
            Console.WriteLine("00001100                            00001101");
            Console.WriteLine("10110001 10111110 10100111 11000010 01011000");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" # Where am I???");
            Console.WriteLine(" # I need to find help!");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" # Tip: type 'help' ");
            Console.WriteLine("");
            Console.WriteLine("00101101 00001100 10100110 00100100 11111001 10111100 ");
            Console.WriteLine("");
		}

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown()) {
                Console.WriteLine("");
                Console.WriteLine("11010010 11000011 11101010 00101101 10010001 10101000");
                Console.WriteLine("");
                Console.WriteLine(" # I don't know what you mean...");
                Console.WriteLine("");
                Console.WriteLine("00100111 00100011 11111101 01011011 10011011 11111000 ");
                Console.WriteLine("");
                return false;
			}

			string commandWord = command.getCommandWord();
			switch (commandWord) {
				case "help":
					printHelp();
					break;
                case "look":
                    printLongDiscription();
                    break;
                case "go":
					goRoom(command);
					break;
				case "quit":
					wantToQuit = true;
					break;
                case "pickup":
                    //Player.setItem();
                    break;

               

            }

			return wantToQuit;
		}

		// implementations of user commands:

		/**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
		private void printHelp()
		{
            Console.WriteLine("");
            Console.WriteLine("01000001 11011010 00001100 11001101 00110011 10101101 ");
            Console.WriteLine("");
            Console.WriteLine(" # You are lost and you don't feel so good,");
            Console.WriteLine(" # Your leg has a big cut and you are bleeding.");
            Console.WriteLine(" # Seems like you are somekind of weird cave,");
            Console.WriteLine(" # You need to find some help!");
			Console.WriteLine("");
			Console.WriteLine(" # Your command words are:");
            parser.showCommands();
            Console.WriteLine("");
            Console.WriteLine("01101001 11010101 11000001 11111000 01001000 11111101 ");
            Console.WriteLine("");
        }

        private void printLongDiscription()
        {
            string discription = player.currentRoom.getLongDescription();
            Console.WriteLine("");
            Console.WriteLine("10000100 00100101 01000100 00001110 11111010 01111000 ");
            Console.WriteLine("");
            Console.WriteLine(" # " + discription);
            Console.WriteLine("");
            Console.WriteLine("00000000 10100000 01111110 01101001 00001010 10010100 ");
            Console.WriteLine("");
        }

		/**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
		private void goRoom(Command command)
		{
			if(!command.hasSecondWord()) {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("");
                Console.WriteLine("00111110 00100111 10101101 00001101 11111010 10001010 ");
                Console.WriteLine("");
                Console.WriteLine(" # Go where?");
                Console.WriteLine("");
                Console.WriteLine("10001000 11010110 11100100 10101011 11110000 10111110 ");
                Console.WriteLine("");
                return;
			}

			string direction = command.getSecondWord();

			// Try to leave current room.
			Room nextRoom = player.currentRoom.getExit(direction);

			if (nextRoom == null) {
                Console.WriteLine("");
                Console.WriteLine("10111110 11010000 11101000 00000111 11001001 11100001 ");
                Console.WriteLine("");
                Console.WriteLine(" # There is no door to " + direction + "!");
                Console.WriteLine("");
                Console.WriteLine("11111100 00010100 11000111 01000110 01100000 00001001 ");
                Console.WriteLine("");
            } else {
				player.currentRoom = nextRoom;
                player.damage(1);
                Console.WriteLine("");
                Console.WriteLine("11101100 00110011 01100110 00110010 01000111 01101010 ");
                Console.WriteLine("");
                Console.WriteLine(" # " + player.currentRoom.getLongDescription());
                Console.WriteLine(" # you have " + player.getHealth + " health left");
                Console.WriteLine("");
                Console.WriteLine("10111110 11010000 11101000 00000111 11001001 11100001 ");
                Console.WriteLine("");
            }
		}

	}
}
