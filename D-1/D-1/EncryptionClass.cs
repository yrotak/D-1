using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_1
{
    class EncryptionClass
    {
        public static string finalEncrypted;
        public static string Encrypt(string key, string plain)
        {
            string result = "";
            while(true)
            { 
                if(key.Length == 32)
                {
                    string[] alphabetArray =
                    {
                        createAlphabet(key[0]),createAlphabet(key[1]),createAlphabet(key[2]),createAlphabet(key[3]),createAlphabet(key[4]),createAlphabet(key[5]),createAlphabet(key[6]),createAlphabet(key[7]),createAlphabet(key[8]),createAlphabet(key[9]),createAlphabet(key[10]),createAlphabet(key[11]),createAlphabet(key[12]),createAlphabet(key[13]),createAlphabet(key[14]),createAlphabet(key[15]),createAlphabet(key[16]),createAlphabet(key[17]),createAlphabet(key[18]),createAlphabet(key[19]),createAlphabet(key[20]),createAlphabet(key[21]),createAlphabet(key[22]),createAlphabet(key[23]),createAlphabet(key[24]),createAlphabet(key[25]),createAlphabet(key[26]),createAlphabet(key[27]),createAlphabet(key[28]),createAlphabet(key[29]),createAlphabet(key[30]),createAlphabet(key[31])
                    };
                    int o = 0;
                    for (int i = 0; i < plain.Length; i++)
                    {
                        o++;
                        char c = plain[i];
                        string alphabetBase = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        int at = alphabetBase.IndexOf(c, 0, 61);
                        string alphabetused;
                        if (i > 31)
                        {
                            o = 0;
                            alphabetused = alphabetArray[o];
                        } else
                        {
                            alphabetused = alphabetArray[i];
                        }
                        if (c == ' ')
                        {
                            finalEncrypted = finalEncrypted + "%";
                        } else
                        {
                            finalEncrypted = finalEncrypted + alphabetused[at];
                        }
                    }
                    result = finalEncrypted;
                    break;
                } else
                {
                    key = keyFormating(key);
                    string[] alphabetArray =
                    {
                        createAlphabet(key[0]),createAlphabet(key[1]),createAlphabet(key[2]),createAlphabet(key[3]),createAlphabet(key[4]),createAlphabet(key[5]),createAlphabet(key[6]),createAlphabet(key[7]),createAlphabet(key[8]),createAlphabet(key[9]),createAlphabet(key[10]),createAlphabet(key[11]),createAlphabet(key[12]),createAlphabet(key[13]),createAlphabet(key[14]),createAlphabet(key[15]),createAlphabet(key[16]),createAlphabet(key[17]),createAlphabet(key[18]),createAlphabet(key[19]),createAlphabet(key[20]),createAlphabet(key[21]),createAlphabet(key[22]),createAlphabet(key[23]),createAlphabet(key[24]),createAlphabet(key[25]),createAlphabet(key[26]),createAlphabet(key[27]),createAlphabet(key[28]),createAlphabet(key[29]),createAlphabet(key[30]),createAlphabet(key[31])
                    };
                    int o = 0;
                    for (int i = 0; i < plain.Length; i++)
                    {
                        o++;
                        char c = plain[i];
                        string alphabetBase = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        int at = alphabetBase.IndexOf(c, 0, 61);
                        string alphabetused;
                        if (i > 31)
                        {
                            o = 0;
                            alphabetused = alphabetArray[o];
                        }
                        else
                        {
                            alphabetused = alphabetArray[i];
                        }
                        if (c == ' ')
                        {
                            finalEncrypted = finalEncrypted + "%";
                        }
                        else
                        {
                            finalEncrypted = finalEncrypted + alphabetused[at];
                        }
                    }
                    result = finalEncrypted;
                    break;
                }
            }
            return result;
        }
        public static string createAlphabet(char key)
        {
            string alphabetBase = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int at = 0;
            at = alphabetBase.IndexOf(key, 0, 62);
            string alphabetResult = ShiftString(alphabetBase, at);
            return alphabetResult;
        }
        public static string keyFormating(string key)
        {
            string result = key;
            for(int i = 0; result.Length < 32; i++)
            {
                result = result + key;
                result = ShiftString(result, i);
            }
            if (result.Length > 32)
            {
                result = result.Remove(result.Length - (result.Length - 32));
            }
            return result;
        }
        public static string ShiftString(string t, int length)
        {
            return t.Substring(length, t.Length - length) + t.Substring(0, length);
        }
    }
}
