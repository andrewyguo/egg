
using System;
using System.IO;
using System.Collections.Generic;

using static System.Console;
namespace Lyrics
{
    class Program
    {
        static string Cipher(string input)
        {
            string allchars = "abcdefghijklmnopqrstuvwxyz";
            string chiphered = ""; 
            
            foreach (char c in input.ToCharArray())
            {
                //Lowercase everything 
                char cLower = Char.ToLower(c);
                bool isUpper = (cLower != c);

                if (allchars.Contains(cLower))
                {
                    int char_index = allchars.IndexOf(cLower);
                    if (char_index + 5 > allchars.Length - 1) char_index = char_index + 5 - allchars.Length;
                    else char_index += 5; 

                    if (isUpper) chiphered = chiphered + Char.ToUpper(allchars[char_index]);
                    else chiphered = chiphered + allchars[char_index];
                }
                else chiphered = chiphered + cLower; 
            }
            return chiphered; 
        }
        static void Main(string[] args)
        {
            List<string> allLines = new List<string>();

            // TODO: Read the file
            string path = "original.txt";
            const FileMode mode = FileMode.Open;
            const FileAccess access = FileAccess.Read;
			
			int numofEs = 0; // num of lines ending in e
			int curLongestLine = 0; // index of line with most words
			int curLongestLineSize = 0; // num of words in longest line
			int curLineIndex = 0; // variables storing number of lines in the file
			int numOfContractions = 0; // num of contractions in the text
			string curLongestWord = ""; // word with most characters in the text
			int curLongestWordSize = 0; // num of characters in the longest word in the text

            using FileStream file = new FileStream(path, mode, access);
            using StreamReader reader = new StreamReader(file); 

            while (!reader.EndOfStream)
            {
                

                string line = reader.ReadLine()!;
                WriteLine(line);

                if (line.EndsWith('e')) numofEs++;

                string[] words = line.Split(' '); 

                foreach(string word in words)
                {
                    if (word.Length > curLongestWordSize)
                    {
                        curLongestWordSize = word.Length;
                        curLongestWord = word; 
                    }
                    
                    if (word.Contains("'"))
                    {
                        numOfContractions++; 
                    }
                }
                if (words.Length > curLongestLineSize) 
                {
                    curLongestLineSize = words.Length;
                    curLongestLine = curLineIndex; 
                }
                allLines.Add(line);

                curLineIndex++; 
            }
			
			WriteLine();
			WriteLine($"There are {curLineIndex} lines in the text");
			
			WriteLine();
			WriteLine($"The number of lines ending in e's is {numofEs}");
			
			WriteLine();
			WriteLine($"The line with the most words is line {curLongestLine} with {curLongestLineSize} words.");
        
			WriteLine();
			WriteLine($"There are {numOfContractions} contractions");

			WriteLine();
			WriteLine($"The longest word is '{curLongestWord}' with {curLongestWordSize} characters.");
			
			WriteLine();
			WriteLine("Saving the ciphered file");
			
			// TODO: Ciphering the text and saving it
			string pathSave = "caesar.txt";
            const FileMode fileModeSave = FileMode.OpenOrCreate;
            const FileAccess fileAccessSave = FileAccess.Write;

            using FileStream fileSave = new FileStream(pathSave, fileModeSave, fileAccessSave);
            using StreamWriter writerSave = new StreamWriter(fileSave);

            /*while (!reader.EndOfStream)
            {
                string line2 = reader.ReadLine();
                writerSave.WriteLine(Cipher(line2)); 
            }*/ 
            foreach (string line2 in allLines)
            {
                writerSave.WriteLine(Cipher(line2));
            }


        }
    }
}
