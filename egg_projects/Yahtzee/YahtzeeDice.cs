using System;
using static System.Console;
using static System.Math;
using System.Collections; 

namespace Bme121
{
    class YahtzeeDice
    {
        int[] faceValue;
        public YahtzeeDice()
        {
            faceValue = new int[5]; //array to store the 5 dice faces 
        }
        public override string ToString()
        {
            string valueOutput = "";

            foreach (int faces in faceValue)
            {
                valueOutput = valueOutput + faces.ToString() + " ";
            }
            return $"Dice faces: {valueOutput}";
        }
        Random rGen = new Random();

        public void Roll()
        {
            for (int i = 0; i < faceValue.Length; i++)
            {
                if(faceValue[i] == 0)
                {
                    faceValue[i] = rGen.Next(1, 7);
                }
            }
        }
        public void Unroll(string faces)
        {
            if (faces == "all")
            {
                for (int i = 0; i < faceValue.Length; i++)
                {
                    faceValue[i] = 0;
                }
            } 
            for (int a = 0; a < faces.Length; a++) //unrolls the specific value in 'faces'
            {
                for (int i = 0; i < faceValue.Length; i++)
                {
                    if (faces.Substring(a, 1) == faceValue[i].ToString()) faceValue[i] = 0;
                }
            } 
        }
        public int Sum()
        {
            int allValueSum = 0;

            for (int i = 0; i < faceValue.Length; i++)
            {
                allValueSum += faceValue[i];
            }
            return allValueSum; 
        }
        public int Sum(int face)
        {
            int faceValueSum = 0;

            for (int i = 0; i < faceValue.Length; i++)
            {  
                if (face == faceValue[i]) faceValueSum += faceValue[i];
            }
            return faceValueSum;
        }
        public bool IsRunOf(int length)
        {
            int[] tempFaceValue = faceValue;
            Array.Sort(tempFaceValue); //sorting array allows for simplier operations to be done for this method 

            int counterIsRunOf = 0;
            int maxRun = 0;
            
            for (int d = 1; d < tempFaceValue.Length; d++)
            {
                if ((tempFaceValue[d] - tempFaceValue[d - 1]) == 1) counterIsRunOf++;
                if (maxRun < counterIsRunOf) maxRun = counterIsRunOf;
                if ((tempFaceValue[d] - tempFaceValue[d - 1]) > 1) counterIsRunOf = 0; 
            }
            if (maxRun + 1 >= length) return true;
            else return false;
        }
        public bool IsSetOf(int size)
        {
            int[] tempFaceValue = faceValue; //temporary array is created so the original array will not be tampered with 
            Array.Sort(tempFaceValue); //sorting array allows for simplier operations to be done for this method 

            int counterIsSetOf = 0;
            int maxSet = 0;

            for (int d = 1; d < tempFaceValue.Length; d++)
            {
                if (tempFaceValue[d] == tempFaceValue[d - 1]) counterIsSetOf++;
                if (maxSet < counterIsSetOf) maxSet = counterIsSetOf;
                if (tempFaceValue[d] != tempFaceValue[d - 1]) counterIsSetOf = 0;
            }
            if (maxSet + 1 >= size) return true;
            else return false;
        }
        public bool IsFullHouse() 
        {
            int[] tempFaceValue = faceValue;
            Array.Sort(tempFaceValue);

            if (tempFaceValue[0] == tempFaceValue[1] && tempFaceValue[2] == tempFaceValue[4]) return true;
            if (tempFaceValue[0] == tempFaceValue[2] && tempFaceValue[3] == tempFaceValue[4]) return true;
            else return false;
        }
    }
}
