using System;
using System.Timers;

namespace PrintFileM
{
    class Program
    {
        static void Main(string[] args)
        {
            ToWrite = new string('-', 15) + Environment.NewLine;

            if (args.Length >= 1 && System.IO.File.Exists(args[0]))
                ToWrite += System.IO.File.ReadAllText(args[0]);
            else
                ToWrite += System.IO.File.ReadAllText(@"C:\Windows\system.ini");


            Timer timer = new Timer(20);

            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Console.ReadLine();
        }

        static readonly int fullcnt = 5;

        static string ToWrite;

        static int writed = -1;
        static char goal_char;
        static int nowcnt = 0;

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (nowcnt == 0)
            {
                if (writed >= ToWrite.Length - 1)
                {
                    Environment.Exit(0);
                }

                goal_char = ToWrite[writed + 1];

                if (goal_char == '\r' || goal_char == '\n' || goal_char == ' ' || goal_char == '\t')
                {
                    WriteChar(goal_char, true);
                    writed++;
                    return;
                }

                nowcnt++;
                WriteChar(GetRandomChar());
            }
            else if (nowcnt > fullcnt)
            {
                nowcnt = 0;
                WriteChar(goal_char, true);

                writed++;
            }
            else
            {
                nowcnt++;
                WriteChar(GetRandomChar());
            }
        }

        static readonly char[] AllowedChars = {
            'a', 'A',
            'b', 'B',
            'c', 'C',
            'd', 'D',
            'e', 'E',
            'f', 'F',
            'g', 'G',
            'h', 'H',
            'i', 'I',
            'j', 'J',
            'k', 'K',
            'l', 'L',
            'm', 'M',
            'n', 'N',
            'o', 'O',
            'p', 'P',
            'q', 'Q',
            'r', 'R',
            's', 'S',
            't', 'T',
            'u', 'U',
            'v', 'V',
            'w', 'W',
            'x', 'X',
            'y', 'Y',
            'z', 'Z',
            '!', '?',
            '<', '>',
            ':', ';',
            '\'', '"',
            '/', '\\',
            '[', ']',
            '{', '}',
            '-', '+',
            '_', '=',
            '@', '#',
            '$', '%',
            '^', '&',
            '(', ')',
            '*', '`',
            ',', '.',
            '~', '|',
        };

        private static char GetRandomChar()
        {
            Random rdm = new Random();
            return AllowedChars[rdm.Next(0, AllowedChars.Length - 1)];
        }

        private static void WriteChar(char Char, bool Carry = false)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;

            if (left + 1 >= Console.WindowWidth)
            {
                Console.SetCursorPosition(0, top + 1);

                left = Console.CursorLeft;
                top = Console.CursorTop;
            }

            Console.Write(Char);
            
            if (!Carry)
                Console.SetCursorPosition(left, top);
        }
    }
}
