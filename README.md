# TwilightAssistant

The basic idea of this application is to assist a group of players when playing a board game Twilight Imperium. This application is designed to be used in tandem with the physical game to help to streamline the process, as well as keep track of a group of players game history. It incorporates many C# techniques (e.g., inheritance, custom interfaces, events etc.) which has helped to develop my software writing skills.

Each player will create a player profile that they can use whenever they are play a game. The player profiles will keep track of basic stats, including no. of games played, wins, and a list of their game history.

When the required player profiles have been created, the create game button on the home page will take you to a page where you can select which of the players are taking part in that game. A game of twilight imperium has a player count between 3-8. Upon selection of the players, each player must select which race they will be playing. Again, there are certain constraints to the race selection, such as no duplicate races, and each player must have a race selected. Once each player has a unique race selected, the create game button will take you to the game page. As it stands, this page is used as a stopwatch for each player to keep track of turn timings, which helps to ensure all players are taking roughly equal time during gameplay, as well as subconsciously speed up each player. There is also an image button with a white symbol that has a question mark inside it. This will be used to allow players to select which strategy card they are using for that round of gameplay, and a turn order can then be calculated based on those cards.

When a game has reached its conclusion, the end game button is pressed, and a winner selected. This will update the stats of all the players involved in that game and display the winner.

This application is a work in progress; therefore, some functionality is still not implemented. A test project has been created to test certain core classes and methods, but the tests do not cover the entire application yet.
