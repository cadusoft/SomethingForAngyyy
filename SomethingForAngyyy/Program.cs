using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SomethingForAngyyy
{
    class Program
    {
        private string mm = "No";

        public Program()
        {
            mm = "yes";
        }

        static void Main(string[] args)
        {
            var ho = 0;
            while (true)
            {
                if (ho % 3 == 0 && ho % 5 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (ho % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (ho % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(ho);
                }

                ho++;

                if (ho > 100)
                    break;
            }
        }

        private static void ZipTest()
        {
            while (true)
            {

                string compressedpath = string.Empty;

                //TODO: Check type of compression
                if (true)
                {

                    Console.WriteLine("Enter file path:");
                    string fullpath = Console.ReadLine();
                    compressedpath = fullpath + ".zip";
                    // 'using' statements guarantee the stream is closed properly which is a big source
                    // of problems otherwise.  Its exception safe as well which is great.
                    using (ZipOutputStream s = new ZipOutputStream(File.Create(compressedpath)))
                    {

                        try
                        {

                            s.SetLevel(9); // 0 - store only to 9 - means best compression

                            byte[] buffer = new byte[4096];


                            // Using GetFileName makes the result compatible with XP
                            // as the resulting path is not absolute.
                            var entry = new ZipEntry(Path.GetFileName(fullpath));

                            // Setup the entry data as required.

                            // Crc and size are handled by the library for seakable streams
                            // so no need to do them here.

                            // Could also use the last write time or similar for the file.
                            entry.DateTime = DateTime.Now;
                            s.PutNextEntry(entry);

                            Stream inputFile = new FileStream(fullpath, FileMode.Open,
                                FileAccess.Read,
                                FileShare.None);

                            using (inputFile)
                            {
                                // Using a fixed size buffer here makes no noticeable difference for output
                                // but keeps a lid on memory usage.
                                int sourceBytes;
                                do
                                {
                                    sourceBytes = inputFile.Read(buffer, 0, buffer.Length);
                                    s.Write(buffer, 0, sourceBytes);
                                } while (sourceBytes > 0);
                            }
                            // Finish/Close arent needed strictly as the using statement does this automatically

                            // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                            // the created file would be invalid.
                            s.Finish();

                            // Close is important to wrap things up and unlock the file.
                            s.Close();


                            inputFile.Close();
                            inputFile = new FileStream(compressedpath, FileMode.Open, FileAccess.Read,
                                FileShare.None);

                            try
                            {
                                File.Delete(fullpath);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            // No need to rethrow the exception as for our purposes its handled.
                        }
                    }

                    //TODO: Implement other compression methods
                }

            }
        }

        private static void StripDigitTest()
        {
            Console.WriteLine("Enter Ref:");
            string clrref = Console.ReadLine();

            Regex rgx = new Regex(@"\D");
            string strippedclrref = rgx.Replace(clrref, "");
            strippedclrref = strippedclrref.TrimStart('0');

            Console.WriteLine(strippedclrref); // "7312315015083";
        }

        public static void NamePlay()
        {
            string answer = "";

            do
            {
                Console.WriteLine("What is your username?");
                answer = Console.ReadLine();
            } while (answer != "Angy");

            do
            {
                Console.WriteLine("What is your password?");
                answer = Console.ReadLine();
            } while (answer != "PasswordB2");

            Console.WriteLine("How old are you?");
            int age = Convert.ToInt32(Console.ReadLine());

            if (age > 18)
            {
                Console.WriteLine("Welcome to the fun house");
            }
            else
            {
                Console.WriteLine("Get out.");
            }

            //CountToSomething();
        }

        public static void CountToSomething()
        {
            Console.WriteLine("Hello, I'm going to count to ?...");
            int number = Convert.ToInt32(Console.ReadLine());

            if ("Hello".Contains("a"))
            {
                Console.WriteLine("Wtf?");
            }

            if (number > 18)
            {
                Console.WriteLine("Welcome to PornHub");
            }
            else
            {
                Console.WriteLine("get out.");
            }

            for (int i = 0; i < number; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
