string[] levelsOfDifficulty = ["Easy", "Hard", "Very Hard"];
string[] operations = ["Addition", "Subtraction", "Division", "Multiplication", "Random"];
string[] operators = ["+", "-", "*", "/"];
string[] previousGames = [];
string levelOfDifficulty = "";
var score = 0;
var gameIsRunning = true;

while (gameIsRunning)
{
    var startTime = DateTime.Now;
    
    string GetTimeElapsed()
    {
        var currentTime = DateTime.Now;
        var elapsedTime = currentTime - startTime;
        var totalMinutes = (int)elapsedTime.TotalMinutes;
        var remainingSeconds = elapsedTime.Seconds;
        return $"{totalMinutes}:{(remainingSeconds < 10 ? "0" : "")}{remainingSeconds}";
    }
}
int[] GenerateQuestion(string operation)
{
    switch (operation)
    {
        case "Addition":
        {
            var firstOperand = Random.Shared.Next(0, 11);
            var secondOperand = Random.Shared.Next(0, 11);

            if (levelOfDifficulty == "Hard")
            {
                firstOperand = Random.Shared.Next(0, 101);
                secondOperand = Random.Shared.Next(0, 101);
            }
            if (levelOfDifficulty == "Very Hard")
            {
                firstOperand = Random.Shared.Next(0, 1001);
                secondOperand = Random.Shared.Next(0, 1001);
            }
            
            var answer = firstOperand + secondOperand;
            return [firstOperand, secondOperand, answer];
        }
        case "Subtraction":
        {
            var firstOperand = Random.Shared.Next(0, 11);
            var secondOperand = Random.Shared.Next(0, 11);
            var answer = firstOperand - secondOperand;
            return [firstOperand, secondOperand, answer];
        }
        case "Multiplication":
        {
            var firstOperand = Random.Shared.Next(0, 11);
            var secondOperand = Random.Shared.Next(0, 11);
            var answer = firstOperand - secondOperand;
            return [firstOperand, secondOperand, answer];
        }
        case "Division":
        {
            while (true)
            {
                var firstOperand = Random.Shared.Next(0, 11);
                var secondOperand = Random.Shared.Next(0, 11);

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

