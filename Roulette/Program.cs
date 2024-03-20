namespace Roulette
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advanced Roulette!");

            Random random = new Random();
            bool playAgain = true;
            int playerBalance = 100;

         
            int[] colorOfNumbers = new int[37];
            for (int i = 1; i <= 36; i++)
            {
                if ((i <= 10 || (i >= 19 && i <= 28)) && i % 2 == 1 ||
                    (i >= 11 && i <= 18 || (i >= 29 && i <= 36)) && i % 2 == 0)
                {
                    colorOfNumbers[i] = 1; // Red
                }
                else
                {
                    colorOfNumbers[i] = 2; // Black
                }
            }

            while (playAgain)
            {
                Console.WriteLine($"Your current balance is: ${playerBalance}");
                Console.Write("How much would you like to bet? $");
                string betAmountInput = Console.ReadLine();
                int betAmount;

                if (!int.TryParse(betAmountInput, out betAmount) || betAmount <= 0)
                {
                    Console.WriteLine("Invalid bet amount. Please enter a positive number.");
                    continue;
                }

                if (betAmount > playerBalance)
                {
                    Console.WriteLine("You do not have enough balance for this bet. Please try a smaller amount.");
                    continue;
                }

                Console.WriteLine("Choose your bet type:");
                Console.WriteLine("1 - Number (0-36)");
                Console.WriteLine("2 - Color (red or black)");
                Console.WriteLine("3 - Odd or Even");
                Console.WriteLine("4 - 1st 12");
                Console.WriteLine("5 - 2nd 12");
                Console.WriteLine("6 - 3rd 12");
                string betTypeInput = Console.ReadLine();
                int betType;

                if (!int.TryParse(betTypeInput, out betType) || betType < 1 || betType > 6)
                {
                    Console.WriteLine("Invalid bet type.");
                    continue;
                }

                int betNumber = 0;
                string colorBet = "";
                bool isEvenBet = false;
                int dozenBet = 0;

                switch (betType)
                {
                    case 1:
                        Console.Write("Place your bet on a number between 0 and 36: ");
                        string input = Console.ReadLine();
                        if (!int.TryParse(input, out betNumber) || betNumber < 0 || betNumber > 36)
                        {
                            Console.WriteLine("Invalid number. Please enter a number between 0 and 36.");
                            continue;
                        }

                        break;
                    case 2:
                        Console.Write("Choose your color (red or black): ");
                        colorBet = Console.ReadLine().ToLower();
                        if (colorBet != "red" && colorBet != "black")
                        {
                            Console.WriteLine("Invalid color. Please enter 'red' or 'black'.");
                            continue;
                        }

                        break;
                    case 3:
                        Console.Write("Choose (odd or even): ");
                        string oddEvenBet = Console.ReadLine().ToLower();
                        if (oddEvenBet != "odd" && oddEvenBet != "even")
                        {
                            Console.WriteLine("Invalid choice. Please enter 'odd' or 'even'.");
                            continue;
                        }

                        isEvenBet = oddEvenBet == "even";
                        break;
                    case 4:
                    case 5:
                    case 6:
                        dozenBet = betType - 3;
                        break;
                    default:
                        Console.WriteLine("Invalid bet type.");
                        continue;
                }

                int winningNumber = random.Next(0, 37);
                Console.WriteLine($"Roulette spins... and lands on number {winningNumber} with color {(colorOfNumbers[winningNumber] == 1 ? "red" : colorOfNumbers[winningNumber] == 2 ? "black" : "green")}!");
                bool win = false;
                switch (betType)
                {
                    case 1: // Number
                        win = betNumber == winningNumber;
                        break;
                    case 2: // Color
                        int winningColor = colorOfNumbers[winningNumber]; // 1 for red, 2 for black
                        win = (colorBet == "red" && winningColor == 1) || (colorBet == "black" && winningColor == 2);
                        break;
                    case 3: // Odd or Even
                        if (winningNumber != 0) // 0 is considered neither odd nor even
                        {
                            win = isEvenBet == (winningNumber % 2 == 0);
                        }
                        break;
                    case 4: // 1st 12
                        win = winningNumber >= 1 && winningNumber <= 12;
                        break;
                    case 5: // 2nd 12
                        win = winningNumber >= 13 && winningNumber <= 24;
                        break;
                    case 6: // 3rd 12
                        win = winningNumber >= 25 && winningNumber <= 36;
                        break;
                }

                if (win)
                {
                    int payoutMultiplier = betType == 1 ? 35 : 2; 
                    if (betType > 3) { payoutMultiplier = 3; } 
                    int winnings = betAmount * payoutMultiplier;
                    playerBalance += winnings;
                    Console.WriteLine($"Congratulations, you win ${winnings}!");
                }
                else
                {
                    playerBalance -= betAmount;
                    Console.WriteLine("Sorry, you lose.");
                }

                if (playerBalance <= 0)
                {
                    Console.WriteLine("You've run out of money. Game over.");
                    break;
                }

                Console.WriteLine("Would you like to play again? (yes/no)");
                string playAgainAnswer = Console.ReadLine().ToLower();
                if (playAgainAnswer != "yes" && playAgainAnswer != "y")
                {
                    playAgain = false;
                    Console.WriteLine("Thank you for playing Roulette!");
                }
            }
        }
    }
}