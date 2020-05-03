using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\nEncrypted Text: " + EncryptionClass.Encrypt("key", "Test here plain text"));
            Console.ReadKey();
        }
    }
}
