using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_1
{
    class AlphabetBase
    {
        public byte[] alphabet = { };
        public string rest = "";
        public AlphabetBase(byte[] alphabet, string rest)
        {
            this.alphabet = alphabet;
            this.rest = rest;
        }
    }
    class DOne
    {
        private static byte[] alphabetBase = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
        private static string restBase = "";
        private static AlphabetBase CreateAlphabetBase(byte[] data)
        {
            alphabetBase = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
            restBase = "";
            List<byte> result = alphabetBase.ToList<byte>();
            List<byte> rest = new List<byte>();
            foreach (byte byteofdata in data)
            {
                if (!result.Contains<byte>(byteofdata))
                {
                    result.Add(byteofdata);
                    rest.Add(byteofdata);
                }
            }
            return new AlphabetBase(result.ToArray(), Convert.ToBase64String(rest.ToArray()));
        }
        private static AlphabetBase CreateAlphabetBaseFromRest(string rest)
        {
            alphabetBase = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");
            restBase = "";
            List<byte> result = alphabetBase.ToList<byte>();
            foreach (byte byteofdata in Convert.FromBase64String(rest))
            {
                if (!result.Contains<byte>(byteofdata))
                {
                    result.Add(byteofdata);
                }
            }
            return new AlphabetBase(result.ToArray(), "");
        }
        public static string Encrypt(byte[] key, byte[] plain)
        {

            List<byte> result = new List<byte>();

            AlphabetBase alphabetBase1 = CreateAlphabetBase(plain);

            alphabetBase = alphabetBase1.alphabet;
            restBase = alphabetBase1.rest;
            foreach (byte keybyte in key)
                if (!alphabetBase.Contains<byte>(keybyte))
                    throw new Exception("Key contain byte that are not in alphabetBase");

            byte[] formatedkey = keyFormating(key);
            List<byte[]> alphabetList = new List<byte[]>();
            foreach (byte keybyte in formatedkey)
                alphabetList.Add(CreateAlphabet(keybyte));


            for (int i = 0, o = 0; i < plain.Length; i++, o++)
            {
                int at = alphabetBase.ToList<byte>().IndexOf(plain[i]);
                if (i >= 32)
                    o = 0;
                result.Add(alphabetList[o][at]);
            }

            return Convert.ToBase64String(result.ToArray())+(restBase == "" ? "" : ".")+restBase;
        }
        public static byte[] Decrypt(byte[] key, string encrypted_b64)
        {
            string rest = "";
            byte[] encrypted = { };
            if(encrypted_b64.Split('.').Length == 2)
            {
                rest = encrypted_b64.Split('.')[1];
                encrypted = Convert.FromBase64String(encrypted_b64.Split('.')[0]);
            } else
            {
                encrypted = Convert.FromBase64String(encrypted_b64);
            }

            List<byte> result = new List<byte>();

            alphabetBase = CreateAlphabetBaseFromRest(rest).alphabet;
            foreach (byte keybyte in key)
                if (!alphabetBase.Contains<byte>(keybyte))
                    throw new Exception("Key contain byte that are not in alphabetBase");

            byte[] formatedkey = keyFormating(key);
            List<byte[]> alphabetList = new List<byte[]>();
            foreach (byte keybyte in formatedkey)
                alphabetList.Add(CreateAlphabet(keybyte));

            for (int i = 0, o = 0; i < encrypted.Length; i++, o++)
            {
                if (i >= 32)
                    o = 0;
                int at = alphabetList[o].ToList<byte>().IndexOf(encrypted[i]);
                result.Add(alphabetBase[at]);
            }
            return result.ToArray();
        }

        private static byte[] CreateAlphabet(byte key)
        {
            return ShiftString(alphabetBase, alphabetBase.ToList<byte>().IndexOf(key));
        }
        private static byte[] keyFormating(byte[] key)
        {
            List<byte> result = key.ToList<byte>();
            for (int i = 0; result.Count < 32; i++)
            {
                foreach (byte keybyte in key)
                {
                    result.Add(keybyte);
                }
                result = ShiftString(result.ToArray(), i).ToList<byte>();
            }
            return Subsequence(result.ToArray(), 0, 32);
        }
        private static byte[] ShiftString(byte[] t, int length)
        {
            List<byte> result = Subsequence(t, length, t.Length - length).ToList<byte>();
            foreach (byte result2byte in Subsequence(t, 0, length).ToList<byte>())
                result.Add(result2byte);
            return result.ToArray();
        }
        private static byte[] Subsequence(byte[] arr, int startIndex, int length)
        {
            return arr.Skip(startIndex).Take(length).ToArray();
        }
    }
}
