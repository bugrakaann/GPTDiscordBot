namespace GptQABotFinal;

public class UserManager
{
    private static UserManager instance;
    private static readonly object lockObject = new object();
    
    private UserManager()
    {
    }

    public static UserManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new UserManager();
                    }
                }
            }
            return instance;
        }
    }
    

    public void DoSomething()
    {
        Console.WriteLine("SingletonClass is doing something.");
    }
}