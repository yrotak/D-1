using System;
using System.Linq;
using System.Text;

namespace D_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\nEncrypted Text: " + EncryptionClass.Encrypt(RandomString(32),"Iorann est pas bo"));
            Console.ReadKey();
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/\@àçéè*-+=_#$£%ùµ§!:.;?,";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
