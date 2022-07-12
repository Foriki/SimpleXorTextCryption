using System;
using System.IO;
using System.Security.Cryptography;

namespace Xor
{
    class Program
    {
        public static char[] RandomIVgen(int length)
        {
            string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            Random rand = new Random();
            char[] a = new char[length];
            for(int i=0; i<length;i++)
            {
                int num = rand.Next(0, chars.Length);
                a[i]=chars[num];
            }
            return a;
        }
        //I just made slight adjustment to @KyleBank's xor encryption src
        //thank you kylebanks - https://github.com/KyleBanks
        private static string CopiedOffSomeWhere(string input, string pass) 
        {
            char[] key = new char[pass.Length];
            for(int i=0; i<pass.Length;i++)
            {
                key[i]=pass[i];
            }
            char[] output = new char[input.Length];
                    
            for(int i = 0; i < input.Length; i++) {
                output[i] = (char) (input[i] ^ key[i % key.Length]);
            }
            
            return new string(output);
	    }
        public static string Gen(int len)
        {
            char[] key = RandomIVgen(len);
            string output="";
            for(int i=0; i<key.Length;i++)
            {
                output=output+key[i];
            }
            return output;
        }
        
        public static void Main(string[] args)
        {
            bool quit=false;
            while(!quit)
            {
                Console.Clear();
                Console.WriteLine("Simple xor text crypter nothing out of the oridnary");
                Console.WriteLine("1. Gen key");
                Console.WriteLine("2. Test it out");
                Console.WriteLine("Enter anything else to quit");
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    if(input==1)
                    {
                        Console.WriteLine("Enter Key Length: ");
                        int length = int.Parse(Console.ReadLine());
                        string generated=Gen(length);
                        Console.WriteLine(generated);
                        
                        Console.WriteLine("\nUse made key?[y/n]");
                        string done = Console.ReadLine();
                        if(done.ToLower()=="y")
                        {
                            Console.WriteLine("Enter a phrase: ");
                            string phrase = Console.ReadLine();
                            string encrypted = CopiedOffSomeWhere(phrase,generated);
                            Console.WriteLine ("Encrypted: " + encrypted);
                            string decrypted = CopiedOffSomeWhere(encrypted,generated);
                            Console.WriteLine ("Decrypted: " + decrypted);
                        }
                        Console.WriteLine("\n\nquit?[y/n]");
                        done = Console.ReadLine();
                        if(done.ToLower()=="y")
                            quit=true;
                    }
                    else if(input==2)
                    {
                        Console.WriteLine("Enter a phrase: ");
                        string phrase = Console.ReadLine();
                        Console.WriteLine("Enter pass: ");
                        string pass = Console.ReadLine();
                        string encrypted = CopiedOffSomeWhere(phrase,pass);
                        Console.WriteLine ("Encrypted: " + encrypted);
                        string decrypted = CopiedOffSomeWhere(encrypted,pass);
                        Console.WriteLine ("Decrypted: " + decrypted);

                        Console.WriteLine("\n\nquit?[y/n]");
                        string done = Console.ReadLine();
                        if(done.ToLower()=="y")
                            quit=true;
                    }
                }
                catch(FormatException)
                {
                    quit=true;
                }
            }
        }
    }
}
