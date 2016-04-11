using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineGeneric.Manager
{
    class CleanerManager
    {
        const int LowerEdgeMenu = 9;
        const int RightEdgeSearchMenu = 60;

        public static void ClearConsole(int line) {
            Console.CursorLeft = 0;
            Console.CursorTop = LowerEdgeMenu;
            for (int i = 1; i < line; i++) {
                Console.WriteLine("                                                                                                     ");
            }
            Console.CursorLeft = 0;
            Console.CursorTop = LowerEdgeMenu;
        }

        public static void ClearSearchMenu() {
            for (int i = 0; i < LowerEdgeMenu; i++) {
                Console.CursorLeft = RightEdgeSearchMenu;
                Console.CursorTop = i;
                Console.WriteLine("                                     ");
            }
            Console.CursorLeft = 0;
            Console.CursorTop = LowerEdgeMenu;
        }

        public static void CheckBorder() {

            if (Console.CursorTop > 42) {
                Console.CursorLeft = 0;
                Console.CursorTop = LowerEdgeMenu;
                for (int i = 1; i < 40; i++) {
                    Console.WriteLine("                                                                                                    ");
                }
                Console.CursorLeft = 0;
                Console.CursorTop = LowerEdgeMenu;
            }

        }
    }
}
