using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;

namespace CodeforcesTemplate
{
    class Program
    {
        private const string FILENAME = "distance4";

        private const string INPUT = FILENAME + ".in";

        private const string OUTPUT = FILENAME + ".out";

        private static Stopwatch _stopwatch = new Stopwatch();

        private static StreamReader _reader = null;
        private static StreamWriter _writer = null;

        private static string[] _curLine;
        private static int _curTokenIdx;

        private static char[] _whiteSpaces = new char[] { ' ', '\t', '\r', '\n' };

        private static string ReadNextToken()
        {
            if (_curTokenIdx >= _curLine.Length)
            {
                //Read next line
                string line = _reader.ReadLine();
                if (line != null)
                    _curLine = line.Split(_whiteSpaces, StringSplitOptions.RemoveEmptyEntries);
                else
                    _curLine = new string[] { };
                _curTokenIdx = 0;
            }

            if (_curTokenIdx >= _curLine.Length)
                return null;

            return _curLine[_curTokenIdx++];
        }

        private static int ReadNextInt()
        {
            return int.Parse(ReadNextToken());
        }

        private static void RunTimer()
        {
#if (DEBUG)

            _stopwatch.Start();

#endif
        }

        private static void Main(string[] args)
        {
#if (DEBUG)
            double before = GC.GetTotalMemory(false);
#endif

            string s = Console.ReadLine();

            RunTimer();

            int countLeadZero = CountLeadZeros(s);
            

            string ss = s.Substring(0, s.Length - countLeadZero);

            string reverse = ReverseString(ss);

            if (reverse == ss)
            {
                Console.WriteLine("YES");
            }

            else
            {
                Console.WriteLine("NO");
            }


#if (DEBUG)
            _stopwatch.Stop();

            double after = GC.GetTotalMemory(false);

            double consumedInMegabytes = (after - before) / (1024 * 1024);

            Console.WriteLine($"Time elapsed: {_stopwatch.Elapsed}");

            Console.WriteLine($"Consumed memory (MB): {consumedInMegabytes}");

            Console.ReadKey();
#endif


        }

        private static int CountLeadZeros(string source)
        {
            int countZero = 0;

            for (int i = source.Length - 1; i >= 0; i--)
            {
                if (source[i] == '0')
                {
                    countZero++;
                }

                else
                {
                    break;
                }

            }

            return countZero;
        }

        private static string ReverseString(string source)
        {
            char[] reverseArray = source.ToCharArray();

            Array.Reverse(reverseArray);

            string reversedString = new string(reverseArray);

            return reversedString;
        }


        private static int[] CopyOfRange(int[] src, int start, int end)
        {
            int len = end - start;
            int[] dest = new int[len];
            Array.Copy(src, start, dest, 0, len);
            return dest;
        }

        private static string StreamReader()
        {

            if (_reader == null)
                _reader = new StreamReader(INPUT);
            string response = _reader.ReadLine();
            if (_reader.EndOfStream)
                _reader.Close();
            return response;


        }

        private static void StreamWriter(string text)
        {

            if (_writer == null)
                _writer = new StreamWriter(OUTPUT);
            _writer.WriteLine(text);


        }





        private static int BFS(int start, int end, int[,] arr)
        {
            Queue<KeyValuePair<int, int>> queue = new Queue<KeyValuePair<int, int>>();
            List<bool> visited = new List<bool>();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                visited.Add(false);
            }

            queue.Enqueue(new KeyValuePair<int, int>(start, 0));
            KeyValuePair<int, int> node;
            int level = 0;
            bool flag = false;

            while (queue.Count != 0)
            {
                node = queue.Dequeue();
                if (node.Key == end)
                {
                    return node.Value;
                }
                flag = false;
                for (int i = 0; i < arr.GetLength(0); i++)
                {

                    if (arr[node.Key, i] == 1)
                    {
                        if (visited[i] == false)
                        {
                            if (!flag)
                            {
                                level = node.Value + 1;
                                flag = true;
                            }
                            queue.Enqueue(new KeyValuePair<int, int>(i, level));
                            visited[i] = true;
                        }
                    }
                }

            }
            return 0;

        }



        private static void Write(string source)
        {
            File.WriteAllText(OUTPUT, source);
        }

        private static string ReadString()
        {
            return File.ReadAllText(INPUT);
        }

        private static void WriteStringArray(string[] source)
        {
            File.WriteAllLines(OUTPUT, source);
        }

        private static string[] ReadStringArray()
        {
            return File.ReadAllLines(INPUT);
        }

        private static int[] GetIntArray()
        {
            return ReadString().Split(' ').Select(int.Parse).ToArray();
        }

        private static double[] GetDoubleArray()
        {
            return ReadString().Split(' ').Select(double.Parse).ToArray();
        }

        private static int[,] ReadINT2XArray(List<string> lines)
        {
            int[,] arr = new int[lines.Count, lines.Count];

            for (int i = 0; i < lines.Count; i++)
            {
                int[] row = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                for (int j = 0; j < lines.Count; j++)
                {
                    arr[i, j] = row[j];
                }
            }

            return arr;
        }
    }
}
