namespace Server;

public enum Difficulties { Easy, Medium, Hard };

public class Question
{
    private Difficulties _difficulty;


    public string Word { get; }


    public Question(Difficulties difficulty)
    {
        _difficulty = difficulty;
        Word = GetWord(_difficulty);
    }


    private string GetWord(Difficulties difficulty)
    {
        // TODO read words from files with different difficulties
        return string.Empty;
    }
}