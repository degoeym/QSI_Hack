using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace QSI_Hack
{
    class Program
    {   
        static void Main(string[] args)
        {
            string letters = checkLetter();
            string numbers = checkNumber();
            Queue<char> possibleKey = new Queue<char>();

            foreach (char c in String.Concat(letters, numbers))
            {
                possibleKey.Enqueue(c);
            }
            
            Console.WriteLine(String.Concat("The full key is: ", makeKey(possibleKey)));
            Console.Read();
        }

        public static string WebResponse(char keyValue)
        {
            WebRequest req = WebRequest.Create(String.Concat("http://simple-snow-3171.herokuapp.com/?key=", keyValue));
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());

            string html = sr.ReadToEnd();

            html = new string((from c in html
                               where char.IsNumber(c)
                               select c
                               ).ToArray());

            return html;
        }

        public static string WebResponse(int keyValue)
        {
            WebRequest req = WebRequest.Create(String.Concat("http://simple-snow-3171.herokuapp.com/?key=", keyValue));
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());

            string html = sr.ReadToEnd();

            html = new string((from c in html
                               where char.IsNumber(c)
                               select c
                               ).ToArray());

            return html;
        }

        public static string WebResponse(string keyValue)
        {
            WebRequest req = WebRequest.Create(String.Concat("http://simple-snow-3171.herokuapp.com/?key=", keyValue));
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());

            string html = sr.ReadToEnd();

            html = new string((from c in html
                               where char.IsNumber(c)
                               select c
                               ).ToArray());

            return html;
        }

        public static string checkLetter()
        {
            string keyLetters = null;
            char c = 'A';
            
            while (c <= 'Z')
            {
                if (WebResponse(c) == "11" || WebResponse(c) == "01")
                {
                    keyLetters += c;
                }
                else if (WebResponse(Char.ToLower(c)) == "11" || WebResponse(Char.ToLower(c)) == "01")
                {
                    keyLetters += Char.ToLower(c);
                }

                c++;
            }

            return keyLetters;
        }

        public static string checkNumber()
        {
            string keyNumbers = null;
            int n = 0;

            while (n <= 9)
            {
                if (WebResponse(n) == "11" || WebResponse(n) == "01")
                {
                    keyNumbers += n;
                }

                n++;
            }

            return keyNumbers;
        }

        public static string makeKey(Queue<char> possibleKey)
        {
            string fullKey = null;
            
            while (possibleKey.Count != 0)
            {
                char possibleCharacter = possibleKey.Dequeue();

                if (WebResponse(String.Concat(fullKey, possibleCharacter)).Contains("0"))
                {
                    possibleKey.Enqueue(possibleCharacter);
                }
                else
                {
                    fullKey += possibleCharacter;

                }
            }

            return fullKey;
        }
    }
}
