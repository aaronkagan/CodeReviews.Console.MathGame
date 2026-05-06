var gameController = new GameController();
gameController.StartGame();

public enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class Game
{
    public GameResult Start(Difficulty difficulty, List<Operation> allowedOperations)
    {
        var player = new Player();
        var generator = new QuestionGenerator();
        List<QuestionResult> questionResults = [];
        var startTime = DateTime.Now;
     
        for (int i = 0; i < 5; i++)
        {
            var question = generator.Generate(allowedOperations, difficulty);
            var questionText = question.GetQuestionText();
            int userAnswer;
            int correctAnswer = question.CalculateAnswer();
            
            Console.WriteLine(questionText);
            
            while (!int.TryParse(Console.ReadLine(), out userAnswer))
            {
                Console.WriteLine("Please enter a valid number:");
            }

            if (correctAnswer == userAnswer)
            {
                Console.WriteLine("Correct");
                player.IncrementScore();
            }
            else
            {
                Console.WriteLine("Wrong!");
            }
            
            QuestionResult questionResult = new QuestionResult(questionText, userAnswer, correctAnswer);
            questionResults.Add(questionResult);
        }

        var endTime = DateTime.Now;
        var duration = (endTime - startTime).TotalSeconds;
        Console.WriteLine($"Score: {player.Score}");
        
        GameResult gameResult = new GameResult(questionResults, duration);
        return gameResult;
    }
}


public class Player
{
    private int _score;

    public int Score => _score;
    
    public void IncrementScore()
    {
        _score++;
    }
    
}

public class Question
{
    private readonly int _firstOperand;
    private readonly int _secondOperand;
    private readonly Operation _operation;

    public Question(int firstOperand, int secondOperand, Operation operation)
    {
        if (operation == Operation.Divide && secondOperand == 0)
            throw new DivideByZeroException();

        _firstOperand = firstOperand;
        _secondOperand = secondOperand;
        _operation = operation;
    }

    public string GetQuestionText()
    {
        char symbol = _operation switch
        {
            Operation.Add => '+',
            Operation.Subtract => '-',
            Operation.Multiply => '*',
            Operation.Divide => '/',
            _ => throw new InvalidOperationException()

        };
        
        return $"{_firstOperand} {symbol} {_secondOperand} = ?";
    }

    public int CalculateAnswer()
    {
        return _operation switch
        {
            Operation.Add => _firstOperand + _secondOperand,
            Operation.Subtract => _firstOperand - _secondOperand,
            Operation.Multiply => _firstOperand * _secondOperand,
            Operation.Divide => _firstOperand / _secondOperand,
            _ => throw new InvalidOperationException()
        };
    }
}

public class QuestionGenerator
{
    public Question Generate(List<Operation> allowedOperations, Difficulty difficulty)
    {
        int first = 0;
        int second = 0;
        Operation operation = Operation.Add;

        switch (difficulty)
                    {
                        case Difficulty.Easy:
                        {   
                            second = Random.Shared.Next(1, 11);
                            operation = allowedOperations[Random.Shared.Next(allowedOperations.Count)];
                            if (operation == Operation.Divide)
                            {
                                first = second * Random.Shared.Next(1,11) ;
                            }
                            else
                            {
                                first = Random.Shared.Next(1, 11);
                            }
                            break;
                        }
                        case Difficulty.Medium:
                        {
                            second = Random.Shared.Next(10, 101);
                            operation = allowedOperations[Random.Shared.Next(allowedOperations.Count)];
                            if (operation == Operation.Divide)
                            {
                                first = second * Random.Shared.Next(10,101) ;
                            }
                            else
                            {
                                first = Random.Shared.Next(10, 101);
                            }
                            break;
                        }
                        case Difficulty.Hard:
                        {
                            second = Random.Shared.Next(100, 1001);
                            operation = allowedOperations[Random.Shared.Next(allowedOperations.Count)];
                            if (operation == Operation.Divide)
                            {
                                first = second * Random.Shared.Next(100,1001) ;
                            }
                            else
                            {
                                first = Random.Shared.Next(100, 1001);
                            }
                        }
                            break;
                    }
        return new Question(first, second, operation);
    }
}

public class QuestionResult
{
    private string QuestionText { get; set; }
    private int UserAnswer { get; set; }
    private int CorrectAnswer { get; set; }
    public QuestionResult(string questionText, int userAnswer, int correctAnswer)
    {
        QuestionText = questionText;
        UserAnswer = userAnswer;
        CorrectAnswer = correctAnswer;
    }

    public void PrintResult()
    {   
        Console.WriteLine($"{QuestionText}, Your Answer: {UserAnswer}, Correct Answer: {CorrectAnswer}");
    }
}

public class GameResult
{
    public  List<QuestionResult> QuestionResults { get; }
    private double Duration { get; }
    public GameResult(List<QuestionResult> gameResult, Double duration)
    {
        QuestionResults = gameResult;
        Duration = duration;
    }
    public void PrintDuration()
    {
        Console.WriteLine($"Game Duration: {Duration} seconds");
    }
    
}

public class GameController
{
    private readonly List<GameResult> _history = [];
    private readonly Game _game = new();

    private Difficulty GetDifficultyLevel()
    {
        string? input = null;
        int choice;

        while (input == null || (!int.TryParse(input, out choice) || choice < 1 || choice > 3))  
        {
            Console.WriteLine(@"Please choose a difficulty level:
1. Easy
2. Medium
3. Hard");
            input = Console.ReadLine();
        }

        Difficulty difficulty = Difficulty.Easy;
        
        switch (choice)
        {
            case 1:
                difficulty = Difficulty.Easy;
                break;
            case 2:
                difficulty =  Difficulty.Medium;
                break;
            case 3:
                difficulty = Difficulty.Hard;
                break;
        }

        return difficulty;
    }

    private List<Operation> GetAllowedOperations()
    {
        string? input = null;
        int choice;

        while (input == null || (!int.TryParse(input, out choice) || choice < 1 || choice > 5))  
        {
            Console.WriteLine(@"Please choose an operation:
1. Addition
2. Subtraction
3. Multiplication
4. Division
5. Random");
            input = Console.ReadLine();
        }

        List<Operation> allowedOperations = [];

        switch (choice)
        {
            case 1:
                allowedOperations.Add(Operation.Add);
                break;
            case 2:
                allowedOperations.Add(Operation.Subtract);
                break;
            case 3:
                allowedOperations.Add(Operation.Multiply);
                break;
            case 4:
                allowedOperations.Add(Operation.Divide);
                break;
            case 5:
                allowedOperations.Add(Operation.Add);
                allowedOperations.Add(Operation.Subtract);
                allowedOperations.Add(Operation.Multiply);
                allowedOperations.Add(Operation.Divide);
                break;
        }

        return allowedOperations;
    }
    
    public void StartGame()
    {
        var inGame = true;
        while (inGame)
        {
            Console.WriteLine(@"What would you like to do?
1. Start Game
2. View History
3. Exit");

            string? input = null;
            while (input == null)
            {
                input  = Console.ReadLine();
            }
            string userChoice = input;

            switch (userChoice)
            {
                case "1":
                    var gameResult = _game.Start(GetDifficultyLevel(), GetAllowedOperations());
                    _history.Add(gameResult);
                    break;
                case "2":
                    ViewHistory();
                    break;
                case "3":
                    Console.WriteLine("Exiting game. Goodbye");
                    inGame = false;
                    break;
            }    
        }
    }

    private void ViewHistory()
    {
        if (_history.Count == 0)
        {
            Console.WriteLine("There is no history to show.");
        }
        else
        { 
            Console.WriteLine("HISTORY");
            foreach (var gameResult in _history)
            {
                Console.WriteLine("----------------------");
                foreach (var questionResult in gameResult.QuestionResults)
                {
                    questionResult.PrintResult();
                }
                gameResult.PrintDuration();
                Console.WriteLine("----------------------");
            }
        }
        
    }
    
}



