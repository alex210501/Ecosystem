using System;

namespace Ecosystem
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Ecosystem())
                game.Run();
        }
    }
}
