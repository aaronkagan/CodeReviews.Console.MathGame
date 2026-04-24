string[] levelsOfDifficulty = ["Easy", "Hard", "Genius"];
string[] operations = ["Addition", "Subtraction", "Division", "Multiplication", "Random"];
string[] operators = ["+", "-", "*", "/"];
string[] previousGames = [];
string levelOfDifficulty = "";
var score = 0;
var gameIsRunning = true; 
var startTime = DateTime.Now;


while (gameIsRunning)
{
    
   

    
    Console.WriteLine($"The total game time was {GetTimeElapsed()}");
    Console.WriteLine("Would you like to play again? y/n");
    var playAgain = Console.ReadLine();
    if (playAgain.Trim().ToLower() == "n")
    {
        gameIsRunning = false;
    }
}

string ShowMenu()
{
}

string GetTimeElapsed()
{
    var currentTime = DateTime.Now;
    var elapsedTime = currentTime - startTime;
    var totalMinutes = (int)elapsedTime.TotalMinutes;
    var remainingSeconds = elapsedTime.Seconds;
    return $"{totalMinutes}:{(remainingSeconds < 10 ? "0" : "")}{remainingSeconds}";
}

int[] GetOperands()
{
    var firstOperand = Random.Shared.Next(1, 11);
    var secondOperand = Random.Shared.Next(1, 11);

    if (levelOfDifficulty == "Hard")
    {
        firstOperand = Random.Shared.Next(11, 101);
        secondOperand = Random.Shared.Next(11, 101);
    }
    if (levelOfDifficulty == "Genius")
    {
        firstOperand = Random.Shared.Next(101, 1001);
        secondOperand = Random.Shared.Next(101, 1001);
    }

    return [firstOperand, secondOperand];
}

int[] GenerateQuestion(string operation)
{
    switch (operation)
    {
        case "Addition":
        {
            var firstOperand = GetOperands()[0];
            var secondOperand = GetOperands()[1];
            var answer = firstOperand + secondOperand;
            return [firstOperand, secondOperand, answer];
        }
        case "Subtraction":
        {
            var firstOperand = GetOperands()[0];
            var secondOperand = GetOperands()[1];
            var answer = firstOperand - secondOperand;
            return [firstOperand, secondOperand, answer];
        }
        case "Multiplication":
        {
            var firstOperand = GetOperands()[0];
            var secondOperand = GetOperands()[1];
            var answer = firstOperand - secondOperand;
            return [firstOperand, secondOperand, answer];
        }
        case "Division":
        {
            while (true)
            {
                var firstOperand = GetOperands()[0];
                var secondOperand = GetOperands()[1];

                if (firstOperand % secondOperand != 0)
                {
                    continue;
                }

                var answer = firstOperand / secondOperand;
                return [firstOperand, secondOperand, answer];
                
            }
        }
    }
}

