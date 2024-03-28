Console.WriteLine("Welcome to the Guess the Number game!");

static int AskForUserInput()
{
    int guess;
    while (true)
    {
        Console.WriteLine("Please enter your guess (number between 1-20):");
        try
        {
            string input = Console.ReadLine();
            guess = int.Parse(input);

            if ((guess >= 1) && (guess <= 20))
            {
                break;
            }
            else
            {
                Console.WriteLine("The number has to be between 1 and 20.");
            }
        }
        catch
        {
            Console.WriteLine("Invalid input enterted!");
        }
    }

    return guess;
}

int correct = Random.Shared.Next(1, 21);
Console.WriteLine(correct);

for (int attempt = 1; attempt <= 3; attempt++)
{
    int guess = AskForUserInput();

    if (guess == correct)
    {
        Console.WriteLine("Great, you win the game!");
        break;
        // return;
    }
    else if (guess < correct)
    {
        Console.WriteLine("Not quite, the correct number is larger.");
    }
    else
    {
        Console.WriteLine("Not quite, the correct number is smaller.");
    }
}

Console.WriteLine("Game ends.");
