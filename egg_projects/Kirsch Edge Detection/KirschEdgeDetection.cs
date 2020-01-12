using System;
using SD = System.Drawing;

using static System.Console;
using System.Linq; 

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Bme121
{
    class Retina // A Retina object holds a retina image as a 2D array of Color objects. 
    {
        public SD.Color[ , ] Pixels { get; private set; }

        // The constructor uses SixLabors ImageSharp to read the image file.
        // The Image< Rgba32 > acts like an array of pixels indexed by column and row.
        // Each pixel is stored in an Rgba32 object.
        // Representation is a 2D array of pixels indexed by row and column.
        // Each pixel is stored in a Color object.

        public Retina( string path )
        {
            using Image< Rgba32 > img6L = Image.Load< Rgba32 >( path );
            Pixels =  new SD.Color[ img6L.Height, img6L.Width ];

            for( int row = 0; row < Pixels.GetLength( 0 ); row ++ )
            {
                for( int col = 0; col < Pixels.GetLength( 1 ); col ++ )
                {
                    int x = col;
                    int y = row;

                    Rgba32 p = img6L[ x, y ];
                    SD.Color c = SD.Color.FromArgb( p.A, p.R, p.G, p.B );

                    Pixels[ row, col ] = c;
                }
            }
        }

        // The SaveToFile method essentially inverts the constructor.
        // Converts 2D array to the Image< Rgba32 > format and uses 
        // SixLabors ImageSharp to save the image in a file.

        public void SaveToFile( string path )
        {
            using Image< Rgba32 > img6L =
                new Image< Rgba32 >( Pixels.GetLength( 1 ), Pixels.GetLength( 0 ) );

            for( int row = 0; row < Pixels.GetLength( 0 ); row ++ )
            {
                for( int col = 0; col < Pixels.GetLength( 1 ); col ++ )
                {
                    int x = col;
                    int y = row;

                    SD.Color c = Pixels[ row, col ];
                    Rgba32 p = new Rgba32( c.R, c.G, c.B, c.A );

                    img6L[ x, y ] = p;
                }
            }
            img6L.Save( path );
        }
    }
    static class Program
    {
        static int[,] RotateKernel(int[,] kernel) // Method to rotate the 3x3 Kirsch Edge operator by 90 degrees
        {
            if (kernel.GetLength(0) != 3 || kernel.GetLength(1) != 3) return kernel; // Ensures input kernel is 3x3  

            int[,] ret = new int[3, 3]; 

            for (int row = 0; row < 3; row ++)
            {
                for (int col = 0; col < 3; col ++)
                {
                    ret[col, 2 - row] = kernel[row, col]; 
                }
            }
            return ret; 
        }
        static int Convolution(int row, int col, string colour, Retina retina)
        {
            int[,] kernel_1 =
            {
                { 5,  5,  5},
                {-3,  0, -3},
                {-3, -3, -3}
            };

            int[,] kernel_2 =
            {
                {-3,  5,  5},
                {-3,  0,  5},
                {-3, -3, -3}
            };

            int[] convolutionValues = new int[8]; // Array to store the sums of convolution 

            switch (colour)
            {
                case "red": 
                    for (int convolution = 0; convolution < 8; convolution += 2)
                    {
                        int sum1 = 0;
                        int sum2 = 0;

                        sum1 += retina.Pixels[row - 1, col - 1].R * kernel_1[0, 0];
                        sum1 += retina.Pixels[row - 1,     col].R * kernel_1[0, 1];
                        sum1 += retina.Pixels[row - 1, col + 1].R * kernel_1[0, 2];
                        sum1 += retina.Pixels[    row, col - 1].R * kernel_1[1, 0];
                        sum1 += retina.Pixels[    row, col + 1].R * kernel_1[1, 2];
                        sum1 += retina.Pixels[row + 1, col - 1].R * kernel_1[2, 0];
                        sum1 += retina.Pixels[row + 1,     col].R * kernel_1[2, 1];
                        sum1 += retina.Pixels[row + 1, col + 1].R * kernel_1[2, 2];

                        sum2 += retina.Pixels[row - 1, col - 1].R * kernel_2[0, 0];
                        sum2 += retina.Pixels[row - 1,     col].R * kernel_2[0, 1];
                        sum2 += retina.Pixels[row - 1, col + 1].R * kernel_2[0, 2];
                        sum2 += retina.Pixels[    row, col - 1].R * kernel_2[1, 0];
                        sum2 += retina.Pixels[    row, col + 1].R * kernel_2[1, 2];
                        sum2 += retina.Pixels[row + 1, col - 1].R * kernel_2[2, 0];
                        sum2 += retina.Pixels[row + 1,     col].R * kernel_2[2, 1];
                        sum2 += retina.Pixels[row + 1, col + 1].R * kernel_2[2, 2];

                        convolutionValues[convolution] = sum1;
                        convolutionValues[convolution + 1] = sum2;

                        kernel_1 = RotateKernel(kernel_1);
                        kernel_2 = RotateKernel(kernel_2);
                    }
                    break;

                case "green":
                    for (int convolution = 0; convolution < 8; convolution += 2)
                    {
                        int sum1 = 0;
                        int sum2 = 0;

                        sum1 += retina.Pixels[row - 1, col - 1].G * kernel_1[0, 0];
                        sum1 += retina.Pixels[row - 1,     col].G * kernel_1[0, 1];
                        sum1 += retina.Pixels[row - 1, col + 1].G * kernel_1[0, 2];
                        sum1 += retina.Pixels[    row, col - 1].G * kernel_1[1, 0];
                        sum1 += retina.Pixels[    row, col + 1].G * kernel_1[1, 2];
                        sum1 += retina.Pixels[row + 1, col - 1].G * kernel_1[2, 0];
                        sum1 += retina.Pixels[row + 1,     col].G * kernel_1[2, 1];
                        sum1 += retina.Pixels[row + 1, col + 1].G * kernel_1[2, 2];
                                                                
                        sum2 += retina.Pixels[row - 1, col - 1].G * kernel_2[0, 0];
                        sum2 += retina.Pixels[row - 1,     col].G * kernel_2[0, 1];
                        sum2 += retina.Pixels[row - 1, col + 1].G * kernel_2[0, 2];
                        sum2 += retina.Pixels[    row, col - 1].G * kernel_2[1, 0];
                        sum2 += retina.Pixels[    row, col + 1].G * kernel_2[1, 2];
                        sum2 += retina.Pixels[row + 1, col - 1].G * kernel_2[2, 0];
                        sum2 += retina.Pixels[row + 1,     col].G * kernel_2[2, 1];
                        sum2 += retina.Pixels[row + 1, col + 1].G * kernel_2[2, 2];

                        convolutionValues[convolution] = sum1;
                        convolutionValues[convolution + 1] = sum2;

                        kernel_1 = RotateKernel(kernel_1);
                        kernel_2 = RotateKernel(kernel_2);
                    }
                    break;

                case "blue":
                    for (int convolution = 0; convolution < 8; convolution += 2)
                    {
                        int sum1 = 0;
                        int sum2 = 0;

                        sum1 += retina.Pixels[row - 1, col - 1].B * kernel_1[0, 0];
                        sum1 += retina.Pixels[row - 1,     col].B * kernel_1[0, 1];
                        sum1 += retina.Pixels[row - 1, col + 1].B * kernel_1[0, 2];
                        sum1 += retina.Pixels[    row, col - 1].B * kernel_1[1, 0];
                        sum1 += retina.Pixels[    row, col + 1].B * kernel_1[1, 2];
                        sum1 += retina.Pixels[row + 1, col - 1].B * kernel_1[2, 0];
                        sum1 += retina.Pixels[row + 1,     col].B * kernel_1[2, 1];
                        sum1 += retina.Pixels[row + 1, col + 1].B * kernel_1[2, 2];
                                                                
                        sum2 += retina.Pixels[row - 1, col - 1].B * kernel_2[0, 0];
                        sum2 += retina.Pixels[row - 1,     col].B * kernel_2[0, 1];
                        sum2 += retina.Pixels[row - 1, col + 1].B * kernel_2[0, 2];
                        sum2 += retina.Pixels[    row, col - 1].B * kernel_2[1, 0];
                        sum2 += retina.Pixels[    row, col + 1].B * kernel_2[1, 2];
                        sum2 += retina.Pixels[row + 1, col - 1].B * kernel_2[2, 0];
                        sum2 += retina.Pixels[row + 1,     col].B * kernel_2[2, 1];
                        sum2 += retina.Pixels[row + 1, col + 1].B * kernel_2[2, 2];

                        convolutionValues[convolution] = sum1;
                        convolutionValues[convolution + 1] = sum2;

                        kernel_1 = RotateKernel(kernel_1);
                        kernel_2 = RotateKernel(kernel_2);
                    }
                    break;

                default:
                    return 0;
                    break; 
            }
            int maxSum = convolutionValues.Max();

            if (maxSum > 255) return 255;
            else if (maxSum < 0) return 0;
            else return maxSum;
        }
        static void Main( )
        {
            // Load the input image.
            Write("Enter file name: ");
            string pathIn = ReadLine(); 
            
            Retina retina = new Retina(pathIn + ".png");
            Retina outputRetina = new Retina(pathIn + ".png"); 

            for (int row = 1; row < retina.Pixels.GetLength(0) - 1; row ++)
            {
                for (int col = 1; col < retina.Pixels.GetLength(1) - 1; col++)
                {
                    int red   = Convolution(row, col, "red",   retina);
                    int green = Convolution(row, col, "green", retina);
                    int blue  = Convolution(row, col, "blue",  retina);

                    outputRetina.Pixels[row, col] = SD.Color.FromArgb(red, green, blue); // Original image is not modified 
                }
            }
            // Store the resulting image.
            string pathOut = pathIn + "_output.png";
            outputRetina.SaveToFile( pathOut );
        }
    }
}