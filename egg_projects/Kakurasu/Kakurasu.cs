using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
        static bool useBoxDrawingChars = true;
        static string[ ] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" };
        static int boardSize = 8; // must be in the range 1..12.

        // TO DO: Uncomment these lines to use in game initialization.
        static double cellMarkProb = 0.2;
        static Random rGen = new Random( );
		
        static void Main( )
        {
            // TO DO: Game initialization
			//double randomNum = rGen.NextDouble(); //generating random number, compare to cellMarkProb to generate squares 
			
			int[] sumRow = new int[boardSize]; 
			int[] sumCol = new int[boardSize];
			bool[ , ] hiddenCells = new bool[boardSize, boardSize];
			
			int[] sumRowUser = new int[boardSize]; 
			int[] sumColUser = new int[boardSize];
			bool[ , ] userCells = new bool[boardSize, boardSize]; //generating arrays to store the cells and sum of cells 
			
			for(int rowIndex = 0; rowIndex < boardSize; rowIndex++)
			{ 
				int tempSum = 0; 
				for(int columnIndex = 0; columnIndex < boardSize; columnIndex ++)
				{
					double randomNum = rGen.NextDouble(); 
					if(randomNum <= cellMarkProb)
					{ 
						hiddenCells[rowIndex, columnIndex] = true; 
						tempSum = tempSum + columnIndex + 1; //adding values horizontally 
					} 
				}
				sumRow[rowIndex] = tempSum; 
			} 
			
			for(int columnIndex = 0; columnIndex < boardSize; columnIndex++) //adding up the column sum values 
			{
				int tempSum2 = 0;
				for(int rowIndex = 0; rowIndex < boardSize; rowIndex++)
				{
					if(hiddenCells[rowIndex, columnIndex])
					{
						tempSum2 = tempSum2 + rowIndex + 1; 
					} 
				}
				sumCol[columnIndex] = tempSum2; 
			}
            
            // This is the main game-play loop.

            bool gameNotQuit = true;
            while( gameNotQuit )
            {
                Console.Clear( );
                WriteLine( );
                WriteLine( "    Play Kakurasu!" );
                WriteLine( );

                // Display the game board.
                // TO DO: Update code to correctly display the game state.

                if( useBoxDrawingChars )
                {
                    for( int row = 0; row < boardSize; row ++ )
                    {
                        if( row == 0 )
                        {
                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( "  {0} ", letters[ col ] );
                            WriteLine( );

                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " {0,2} ", col + 1 );
                            WriteLine( );

                            Write( "        \u250c" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u252c" );
                            WriteLine( "\u2500\u2500\u2500\u2510" );
                        }

                        Write( "   {0} {1,2} \u2502", letters[ row ], row + 1 );

                        for( int col = 0; col < boardSize; col ++ )
                        {
                           // WriteLine(" {0}  {1}   {2}   ", row, col, userCells[row, col]); //testing purposes 
                            if( userCells[row, col] ) Write( " X \u2502" ); //writing the X
                            
                            else                       Write( "   \u2502" );
                        }
                        if(sumRow[row] >= 10 && sumRowUser[row] >= 10) WriteLine( " {1} {0} ", sumRow[row], sumRowUser[row] );
                        else if(sumRowUser[row] >= 10 ) WriteLine( " {1} 0{0} ", sumRow[row], sumRowUser[row] );
                        else if(sumRow[row] >= 10 ) WriteLine( " 0{1} {0} ", sumRow[row], sumRowUser[row] );  
						else WriteLine(" 0{1} 0{0} ", sumRow[row], sumRowUser[row] ); //displaying row sums (user and target) 

                        if( row < boardSize - 1 )
                        {
                            Write( "        \u251c" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u253c" );
                            WriteLine( "\u2500\u2500\u2500\u2524" );
                        }
                        else
                        {
                            Write( "        \u2514" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u2534" );
                            WriteLine( "\u2500\u2500\u2500\u2518" );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                if(sumColUser[col] >= 10) Write( " {0} ", sumColUser[col] );
								else Write(" 0{0} ", sumColUser[col] ); 
                            WriteLine( );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                if(sumCol[col] >= 10) Write( " {0} ", sumCol[col] );
								else Write(" 0{0} ", sumCol[col] );        
                            WriteLine( );
                            
                            bool gameWon = true; //telling user they won 
                            for (int i = 0; i < boardSize; i++)
                            {
                                if (sumRowUser[i] != sumRow[i] || sumColUser[i] != sumCol[i])
                                {
                                    gameWon = false;
                                }
                            }
                            if (gameWon) WriteLine("You win!");

                        }
                    }
                }
                else // ! useBoxDrawingChars
                {
                    for( int row = 0; row < boardSize; row ++ )
                    {
                        if( row == 0 )
                        {
                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( "  {0} ", letters[ col ] );
                            WriteLine( );

                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " {0,2} ", col + 1 );
                            WriteLine( );

                            Write( "        +" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "---+" );
                            WriteLine( "---+" );
                        }

                        Write( "   {0} {1,2} |", letters[ row ], row + 1 );

                        for( int col = 0; col < boardSize; col ++ )
                        {
                            if( row == 1 && col == 1 ) Write( " X |" ); 
                            else                       Write( "   |" );
                        }
                        WriteLine( " 00 00 " );

                        if( row < boardSize - 1 )
                        {
                            Write( "        +" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "---+" );
                            WriteLine( "---+" );
                        }
                        else
                        {
                            Write( "        +" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "---+" );
                            WriteLine( "---+" );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " 00 " );
                            WriteLine( );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " 00 " );
                            WriteLine( );
                        }
                    }
                }

                // Get the next move.

                WriteLine( );
                WriteLine( "   Toggle cells to match the row and column sums." );
                Write(     "   Enter a row-column letter pair or 'quit': " );
                string response = ReadLine( );

                if( response == "quit" ) gameNotQuit = false;
                else if(response.Length != 2) gameNotQuit = true; //ignores invalid 1 character inputs 
                else 
                { 
					char rowVal = response[0]; 
                    char colVal = response[1];
                    
                    for(int rowIndex = 0; rowIndex < boardSize; rowIndex++)
					{ 	
						for(int columnIndex = 0; columnIndex < boardSize; columnIndex ++)
						{
							if(rowVal.ToString() == letters[rowIndex] && colVal.ToString() == letters[columnIndex] && userCells[rowIndex, columnIndex] == false ) 
							{
								userCells[rowIndex, columnIndex] = true; 
							} else if (rowVal.ToString() == letters[rowIndex] && colVal.ToString() == letters[columnIndex] && userCells[rowIndex, columnIndex] == true )
							{	
							userCells[rowIndex, columnIndex] = false; 
							}
						}
					}
					// adding up the sum of the userCells 
                    for(int columnIndex = 0; columnIndex < boardSize; columnIndex++) //adding up the column sum values 
					{
						int userSum2 = 0;
						for(int rowIndex = 0; rowIndex < boardSize; rowIndex++)
						{
							if(userCells[rowIndex, columnIndex])
							{
								userSum2 = userSum2 + rowIndex + 1; 
							} 
						}
						sumColUser[columnIndex] = userSum2; 
					} 
					for(int rowIndex = 0; rowIndex < boardSize; rowIndex++) //adding up the row sum values 
					{
						int userSum3 = 0;
						for(int columnIndex = 0; columnIndex < boardSize; columnIndex++)
						{
							if(userCells[rowIndex, columnIndex])
							{
								userSum3 = userSum3 + columnIndex + 1; 
							} 
						}
						sumRowUser[rowIndex] = userSum3; 
					}
                			}
            
		}	
            WriteLine( );
        }
    }
}
