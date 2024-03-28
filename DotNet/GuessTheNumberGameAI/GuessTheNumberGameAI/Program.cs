// select a random number between 1 and 20
int number = new Random().Next(1, 21);

// ask the user to guess the number
Console.WriteLine("Guess the number between 1 and 20");
int guess = int.Parse(Console.ReadLine());

// check if the user guessed the number, too high or too low
if (guess == number)
{
    Console.WriteLine("You guessed the number!");
}
else if (guess < number)
{
    Console.WriteLine("Too low");
}
else
{
    Console.WriteLine("Too high");
}
