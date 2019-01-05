using System;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new SpaceInvaders())
                game.Run();
        }
    }
#endif
}
