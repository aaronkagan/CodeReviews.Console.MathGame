using Spectre.Console;
using static MathGame.aaronkagan.Enums;

string[] operators = ["+", "-", "*", "/"];
List<string> previousGames = [];
Difficulty difficulty = Difficulty.Easy;
var gameCount = 0;
var gameIsRunning = true; 
var startTime = DateTime.Now;
string randomOperator;
var score = 0;
var divide = "----------------";

Console.WriteLine("Welcome to the math game!");

while (gameIsRunning)
{
    if (gameCount == 0)
    {   
        difficulty = AnsiConsole.Prompt(new SelectionPrompt<Difficulty>()
            .Title("Please choose an level of difficulty")
            .AddChoices(
                Difficulty.Easy,
                Difficulty.Hard,
                Difficulty.Genius
            )
        );
    }
    
    
    randomOperator = operators[Random.Shared.Next(0, 4)];
    
    var chosenOperation = AnsiConsole.Prompt(new SelectionPrompt<Operations>()
        .Title("Please choose an operation")
        .AddChoices(
            Operations.Addition,
            Operations.Subtraction,
            Operations.Multiplication,
            Operations.Division,
            Operations.Random
            )
    );
    
    var gameData = GenerateQuestion(chosenOperation);

    Console.WriteLine($"What is {gameData[0]} {GetOperator(chosenOperation)} {gameData[1]}");
    
    gameCount++;

    var userAnswer = Console.ReadLine();

    if (userAnswer != null)
    {
        userAnswer = userAnswer.Trim();
    }

    if (userAnswer == Convert.ToString(gameData[2]))
    {
        Console.WriteLine("Correct");
        score++;
    }
    else
    {
        Console.WriteLine("Wrong!");
    }
    
    previousGames.Add(@$"
Question: {gameData[0]} {GetOperator(chosenOperation)} {gameData[1]}
Your Answer: {userAnswer}
Correct Answer: {gameData[2]}");
    
    if (gameCount >= 5)
    {
        Console.WriteLine("Game Over");
        Console.WriteLine($"Your score was {score}");
        Console.WriteLine($"Your game time was {GetTimeElapsed()}");
        
        while (true)
        {
            var chosenOption = AnsiConsole.Prompt(new SelectionPrompt<MenuChoices>()
                .Title("What would you like to do?")
                .AddChoices(
                    MenuChoices.Replay,
                    MenuChoices.History,
                    MenuChoices.Quit
                )
            );

            if (chosenOption == MenuChoices.Replay)
            {
                score = 0;
                gameCount = 0;
                Console.Clear();
                break;
            }

            if (chosenOption == MenuChoices.History)
            {
                if (previousGames.Count == 0)
                {
                    Console.WriteLine("There are no previous games to show.");
                }
                else
                { 
                    ShowHistory();
                }
            }
        
            if (chosenOption == MenuChoices.Quit)
            {
                gameIsRunning = false;
                Console.WriteLine($"The total game time was {GetTimeElapsed()}");
                Console.WriteLine("Goodbye");
                break;
                
            }
        }
    }
}

void ShowHistory()
{
    Console.Clear();
    Console.WriteLine(divide);
    Console.WriteLine("Game History");
    Console.WriteLine(divide);
    
    foreach (var game in previousGames)
    {
        Console.WriteLine(game);
    }

    Console.WriteLine(divide);
}

string GetOperator(Operations chosenOperation)
{
    switch (chosenOperation)
    {
        case Operations.Addition:
            return "+";
        case Operations.Subtraction:
            return "-";
        case Operations.Multiplication:
            return "*";
        case Operations.Division:
            return "/";
        case Operations.Random:
            return randomOperator;
        default:
            return "";
    }
}

string GetTimeElapsed()
{
    var currentTime = DateTime.Now;
    var elapsedTime = currentTime - startTime;
    var totalMinutes = (int)elapsedTime.TotalMinutes;
    var remainingSeconds = elapsedTime.Seconds;
    return $"{totalMinutes}:{(remainingSeconds < 10 ? "0" : "")}{remainingSeconds}";
}

int[] GetOperands(Difficulty levelOfDifficulty)
{
    var firstOperand = Random.Shared.Next(1, 11);
    var secondOperand = Random.Shared.Next(1, 11);

    if (levelOfDifficulty == Difficulty.Hard)
    {
        firstOperand = Random.Shared.Next(11, 101);
        secondOperand = Random.Shared.Next(11, 101);
    }
    if (levelOfDifficulty == Difficulty.Genius)
    {
        firstOperand = Random.Shared.Next(101, 1001);
        secondOperand = Random.Shared.Next(101, 1001);
    }

    return [firstOperand, secondOperand];
}

int[] GenerateQuestion(Operations operation)
{
    switch (operation)
    {
        case Operations.Addition:
        {
            var operands = GetOperands(difficulty);
            var firstOperand = operands[0];
            var secondOperand = operands[1];

            var answer = firstOperand + secondOperand;
            return [firstOperand, secondOperand, answer];
        }
        case Operations.Subtraction:
        {
            var operands = GetOperands(difficulty);
            var firstOperand = operands[0];
            var secondOperand = operands[1];

            var answer = firstOperand - secondOperand;
            return [firstOperand, secondOperand, answer];
        }
        case Operations.Multiplication:
        {
            var operands = GetOperands(difficulty);
            var firstOperand = operands[0];
            var secondOperand = operands[1];

            var answer = firstOperand * secondOperand;
            return [firstOperand, secondOperand, answer];
        }
        case Operations.Division:
        {
            while (true)
            {
                var operands = GetOperands(difficulty);
                var firstOperand = operands[0];
                var secondOperand = operands[1];


                if (firstOperand % secondOperand != 0)
                {
                    continue;
                }

                var answer = firstOperand / secondOperand;
                return [firstOperand, secondOperand, answer];
            }
        }
        case Operations.Random:
        {

            while (true)
            {
                var operands = GetOperands(difficulty);
                var firstOperand = operands[0];
                var secondOperand = operands[1];
                var randomOperation = randomOperator;
            
                if (randomOperation == "/" && firstOperand % secondOperand != 0)
                {
                    continue;
                }

                switch (randomOperation)
                {
                    case "+":
                    {
                        var answer =  firstOperand + secondOperand;
                        return [firstOperand, secondOperand, answer];
                    }
                    case "-":
                    {
                        var answer =  firstOperand - secondOperand;
                        return [firstOperand, secondOperand, answer];    
                    }
                    case "*":
                    {
                        var answer =  firstOperand * secondOperand;
                        return [firstOperand, secondOperand, answer];    
                    }
                    case "/":
                    {
                        var answer =  firstOperand / secondOperand;
                        return [firstOperand, secondOperand, answer];    
                    }
                        
                }
            }
        }
        default:
        {
            return [];
        }
    }
}



