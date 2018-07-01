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
			Room outside, theatre, pub, lab, office, hallway, hallway2, room101, room201;
            Item apple;

			// create the rooms
			outside = new Room("outside the main entrance of the university");
			theatre = new Room("in a lecture theatre");
			pub = new Room("in the campus pub");
			lab = new Room("in a computing lab");
			office = new Room("in the computing admin office");
            hallway = new Room("in the groundfloor hallway");
            hallway2 = new Room("on the first floor in the hallway");
            room101 = new Room("in a room that is almost empty, there is only a chair in the middle of the room");
            room201 = new Room("in normal a students room, its really dirty (better get out of here)");

            //create the items
            apple = new Apple();


			// initialise room exits
			outside.setExit("east", hallway);
			outside.setExit("south", lab);
			outside.setExit("west", pub);

            hallway.setExit("west", outside);
            hallway.setExit("east", theatre);
            hallway.setExit("up", hallway2);

            hallway2.setExit("down", hallway);
            hallway2.setExit("east", room101);
            hallway2.setExit("west", room201);

            room101.setExit("west", hallway2);

            room201.setExit("east", hallway2);

			theatre.setExit("west", hallway2);

			pub.setExit("east", outside);

			lab.setExit("north", outside);
			lab.setExit("east", office);

			office.setExit("west", lab);

            //add items to room
            //outside.addItem("apple", apple);

			player.currentRoom = outside;  // start game outside
		}

        //public string itemRoom()
        //{
            //return
        //}

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
            Console.WriteLine("########################################################");
            Console.WriteLine("");
            Console.WriteLine("Welcome to Dorn");
			Console.WriteLine("Dorn is a old, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
            Console.WriteLine("");
            Console.WriteLine("########################################################");
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
                Console.WriteLine("########################################################");
                Console.WriteLine("");
                Console.WriteLine("I don't know what you mean...");
                Console.WriteLine("");
                Console.WriteLine("########################################################");
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
            Console.WriteLine("########################################################");
            Console.WriteLine("");
            Console.WriteLine("You are lost. Injured and alone.");
			Console.WriteLine("You wander around at the university.");
            Console.WriteLine("You need to find help!");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
            Console.WriteLine("");
            Console.WriteLine("########################################################");
            Console.WriteLine("");
        }

        private void printLongDiscription()
        {
            string discription = player.currentRoom.getLongDescription();
            Console.WriteLine("");
            Console.WriteLine("########################################################");
            Console.WriteLine("");
            Console.WriteLine(discription);
            Console.WriteLine("");
            Console.WriteLine("########################################################");
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
                Console.WriteLine("########################################################");
                Console.WriteLine("");
                Console.WriteLine("Go where?");
                Console.WriteLine("");
                Console.WriteLine("########################################################");
                Console.WriteLine("");
                return;
			}

			string direction = command.getSecondWord();

			// Try to leave current room.
			Room nextRoom = player.currentRoom.getExit(direction);

			if (nextRoom == null) {
                Console.WriteLine("");
                Console.WriteLine("########################################################");
                Console.WriteLine("");
                Console.WriteLine("There is no door to "+direction+"!");
                Console.WriteLine("");
                Console.WriteLine("########################################################");
                Console.WriteLine("");
            } else {
				player.currentRoom = nextRoom;
                player.damage(1);
                Console.WriteLine("");
                Console.WriteLine("########################################################");
                Console.WriteLine("");
                Console.WriteLine(player.currentRoom.getLongDescription());
                Console.WriteLine("you have " + player.getHealth + " health left");
                Console.WriteLine("");
                Console.WriteLine("########################################################");
                Console.WriteLine("");
            }
		}

	}
}
