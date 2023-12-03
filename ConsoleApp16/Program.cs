using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var players = new List<string> { "Player1", "Player2" };
            var game = new Game(players);
            game.Play();
            game.break;
        }
    }
}
