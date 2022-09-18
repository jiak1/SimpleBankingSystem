using System;

namespace AppDevDotNetTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(61, 20);
            Console.SetBufferSize(61, 2000);

            new LoginSystem().LoginMenu();
        }


    }
}
