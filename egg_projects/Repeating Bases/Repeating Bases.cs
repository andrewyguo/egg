using System;
using static System.Console;
 
namespace Bme121
{ 
	static class Program
	{
		static void Main( )
		{
    		Write("Enter a positive integer: ");
            int num = int.Parse(ReadLine()); 
            
            int n = num;
           
            for (int i = num - 1; i >= 2; i--)
            {
                int base1 = i; 
                n = num; 
               
                int b1 = n%base1; 
                n = (num/base1); 
                
                int b2 = n%base1;

                int counter = 2;
                
                while (b1 == b2 && b1 >= 1 && b2 >= 1)
                {
                    n = (n/base1);
                    if (n < 1)
                    {
                        Write(num + " in base 10 is ");

                        for (int a = 0; a  < counter - 1; a++)
                        {
                            Write(b1 + ","); 
                        }   
                        Write(b1);
                        Write(" in base " + base1); 
                        WriteLine();
                        
                        b1 = -1; 
                        b2 = -1; 
                        
                        break;
                    } 
                    b2 = n%base1;
                    counter ++;
                }
            } 
		}			
	}
}

