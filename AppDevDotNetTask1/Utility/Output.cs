using System;

namespace AppDevDotNetTask1
{
    class Output
    {
        /// <summary>
        /// Outputs a customised boxed window with headings.
        /// </summary>
        /// <param name="title">The window title</param>
        /// <param name="subheading">The window subheading</param>
        /// <param name="content">The content of the window, what to display on each line</param>
        /// <param name="boxPrompt">Whether a third box with a prompt needs to be displayed</param>
        public static void PrintWindow(string title, string subheading, string[] content, string boxPrompt = "")
        {
            // Display the heading for the box + center the title
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║{CenterPadding(title, 58)}║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════╣");

            // Display a centered subheading if required
            if (subheading != "")
            {
                Console.WriteLine($"║{CenterPadding(subheading, 58)}║");
                Console.WriteLine("║                                                          ║");
            }

            // Loop through each each line & print it in the window
            foreach (string line in content)
            {
                Console.WriteLine($"║      {line.PadRight(52)}║");
            }

            // If a third box prompt is required, display it accordingly
            if (boxPrompt != "")
            {
                Console.WriteLine("╠══════════════════════════════════════════════════════════╣");
                Console.WriteLine($"║      {boxPrompt.PadRight(52)}║");
            }

            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        }

        /// <summary>
        /// This function takes a given string & pads it either side up to the total fill length,
        /// whilst ensuring the given value stays centered.
        /// </summary>
        /// <param name="val">The value to pad</param>
        /// <param name="fillLength">The total length of the string when padded</param>
        /// <returns>The value, padded up to fill length & centered</returns>
        private static string CenterPadding(string val, int fillLength)
        {
            // Calculate how many spaces are required on the left based on the length of the value & the total length
            // Divide by two, so that equal spaces can go to the left & the right
            int padLeft = (fillLength - val.Length) / 2 + val.Length;
            return val.PadLeft(padLeft).PadRight(fillLength);
        }
    }
}