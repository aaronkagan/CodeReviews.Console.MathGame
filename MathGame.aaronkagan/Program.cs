int [,] operands = {{1,2},{3,6},{10,5},{20,10},{4,4}};
string [] operations = ["+","*","/","-","%"];
int[] answers = [3, 18, 2, 10, 0];
int questionCount = 0;

while (questionCount < operations.Length)
{
    int firstOperand = operands[questionCount, 0];
    int secondOperand = operands[questionCount, 1];
    string operation = operations[questionCount];
    int userAnswer = 0;
    string readResult = "";
    
    do
    {
        Console.WriteLine($"What is the result of {firstOperand} {operation} {secondOperand}");
        readResult = Console.ReadLine();
        if (readResult != null)
        {
            userAnswer = Convert.ToInt32(readResult);
        } 
    } while (readResult == "");
    
    

    if (questionCount < 4)
    {
        if (userAnswer == answers[questionCount])
        {
            Console.WriteLine("Correct!. Press enter for the next question");
            Console.ReadLine();
        }
        else 
        {
            Console.WriteLine("Sorry wrong answer. Press enter for the next question");
            Console.ReadLine();
        }
    }

    if (questionCount == 4)
    {
        if (userAnswer == answers[questionCount])
        {
            Console.WriteLine("Correct!. Press enter to finish the game.");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Sorry wrong answer. Press enter to finish the game.");
            Console.ReadLine();
        }
    }
    questionCount++;
}

Console.WriteLine("Thanks for playing! Goodbye.");