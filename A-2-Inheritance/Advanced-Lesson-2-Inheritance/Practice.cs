using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Advanced_Lesson_2_Inheritance.Practice.Printer;


namespace Advanced_Lesson_2_Inheritance
{
    public static partial class Practice
    {
        /// <summary>
        /// A.L2.P1/1. Создать консольное приложение, которое может выводить 
        /// на печатать введенный текст  одним из трех способов: 
        /// консоль, файл, картинка. 
        /// </summary>
        public static void A_L2_P1_1()
        {   
            Console.WriteLine("Insert the text to print");
            string text = Console.ReadLine();
            Console.WriteLine("Choose print type");
            Console.WriteLine("1 - Console");
            Console.WriteLine("2 - File");
            Console.WriteLine("3 - Image");
            string resultOfUserChoice = Console.ReadLine();
            switch (resultOfUserChoice)
            {
                case "1":
                    // Console.WriteLine("You've chosen the printing into console");
                    // ConsolePrinter consolePrinter = new ConsolePrinter();
                    var printer = new ConsolePrinter(text, ConsoleColor.Blue);
                    printer.Print();
                    break;
                case "2":
                    //Console.WriteLine("You've chosen the printing into file");
                    var printer2 = new FilePrinter(text, "Test");
                    printer2.Print();
                    break;
                case "3":
                    var printer3 = new ImagePrinter(text,"ImagePrinterExample");
                    printer3.Print();
                    break;
            }
        }

        public abstract class Printer
        {
            public string PrintingText { get; set; }
            public abstract void Print();

            public Printer(string str)
            {
                PrintingText = str;
            }

            public class ConsolePrinter : Printer
            {
                public override void Print()
                {
                    Console.ForegroundColor = _color;
                    Console.WriteLine(PrintingText);
                    Console.ResetColor();
                }

                public ConsolePrinter(string str, ConsoleColor color) : base(str)
                {
                    _color = color;
                }

                private ConsoleColor _color;
            }

            public class FilePrinter : Printer
            {
                public override void Print()
                {
                   File.AppendAllText($@"D:\{_fileName}.tht", PrintingText);
                }

                public FilePrinter(string str, string fileName) : base(str)
                {
                    _fileName = fileName;
                }

                private string _fileName;
            }

            public class ImagePrinter : Printer
            {
                public string ImageName;

                public ImagePrinter(string str, string imageName) : base(str)
                {
                    ImageName = imageName;
                }

                public override void Print()
                {
                    System.Drawing.Bitmap bi = new System.Drawing.Bitmap(width: 500, height:500);

                    // Create font and brush.
                    Font drawFont = new Font("Arial", 16);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);

                    // Create point for upper-left corner of drawing.
                    float x = 150.0F;
                    float y = 50.0F;

                    // Set format of string.
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;

                    // Draw string to screen.
                    Graphics graphics = Graphics.FromImage(bi);
                    graphics.DrawString(PrintingText, drawFont, drawBrush, x, y, drawFormat);
                    bi.Save($@"D:\{ImageName}.png");
                }

                //public object imageName { get; private set; }
            }

        }
    }
}
