using System;
using System.Collections.Generic;
using System.Text;

namespace AppDevDotNetTask1
{
    class AccountSystem
    {
        private List<Account> accounts = new List<Account>();

        public void LoginMenu()
        {
            PrintMainMenu();
            /*PrintLoginMenu();
            Console.SetCursorPosition(18,5);
            string username = Console.ReadLine();
            Console.SetCursorPosition(17, 6);
            string password = ReadPrivateInput();
            Console.WriteLine(password);*/
            Console.ReadLine();
        }

        private void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║        WELCOME TO SIMPLE BANKING SYSTEM        ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║      1. Create a new account                   ║");
            Console.WriteLine("║      2. Search for an account                  ║");
            Console.WriteLine("║      3. Deposit                                ║");
            Console.WriteLine("║      4. Withdraw                               ║");
            Console.WriteLine("║      5. A/C statement                          ║");
            Console.WriteLine("║      6. Delete account                         ║");
            Console.WriteLine("║      7. Exit                                   ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║      Enter your choice (1-7):                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintCreateMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              CREATE A NEW ACCOUNT              ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║                ENTER THE DETAILS               ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      First Name:                               ║");
            Console.WriteLine("║      Last Name:                                ║");
            Console.WriteLine("║      Address:                                  ║");
            Console.WriteLine("║      Phone:                                    ║");
            Console.WriteLine("║      Email:                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintSearchMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║               SEARCH AN ACCOUNT                ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintAccountDetails()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                ACCOUNT DETAILS                 ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account No:                               ║");
            Console.WriteLine("║      Account Balance:                          ║");
            Console.WriteLine("║      First Name:                               ║");
            Console.WriteLine("║      Last Name:                                ║");
            Console.WriteLine("║      Address:                                  ║");
            Console.WriteLine("║      Phone:                                    ║");
            Console.WriteLine("║      Email:                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintDepositMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                    DEPOSIT                     ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("║      Amount:                                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintWithdrawMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                   WITHDRAW                     ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("║      Amount:                                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintStatementMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                   STATEMENT                    ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintAccountStatement()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              SIMPLE BANKING SYSTEM             ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║      Account Statement                         ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account No:                               ║");
            Console.WriteLine("║      Account Balance:                          ║");
            Console.WriteLine("║      First Name:                               ║");
            Console.WriteLine("║      Last Name:                                ║");
            Console.WriteLine("║      Address:                                  ║");
            Console.WriteLine("║      Phone:                                    ║");
            Console.WriteLine("║      Email:                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintDeleteAccountMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║               DELETE AN ACCOUNT                ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }
    }
}
