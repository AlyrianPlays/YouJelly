using System;

namespace YouJelly
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new YouJelly())
                game.Run();
        }
    }
}
