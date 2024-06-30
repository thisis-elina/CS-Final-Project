using libs;

public sealed class TimeLord
{
    private DateTime _startingTimestamp; // Timestamp when the timer started or was reset
    private double _remainingTime; // Remaining time in seconds

    // Singleton instance
    public static TimeLord instance;

    // Public property to access the singleton instance
    public static TimeLord Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TimeLord(); // Initialize the instance if not already done
            }
            return instance;
        }
    }

    // Private constructor to prevent direct instantiation
    private TimeLord()
    {
        _startingTimestamp = DateTime.Now; 
        _remainingTime = 100; 
    }

    // Method to subtract time based on the elapsed time since the last call
    public void SubtractTime()
    {
        if (GameEngine.Instance.GameState != GameEngineState.Playing)
        {
            // Reset the starting timestamp if the game is not in the playing state
            _startingTimestamp = DateTime.Now;
            return;
        }

        // Calculate the difference in seconds between now and the last timestamp
        double difference = Math.Abs((DateTime.Now - _startingTimestamp).TotalSeconds);
        _startingTimestamp = DateTime.Now; 
        _remainingTime -= difference;

        if (_remainingTime <= 0)
        {
            // Ensure remaining time does not go below zero
            _remainingTime = 0;
        }
    }

    // Method to set the remaining time
    public void SetTime(double time)
    {
        _remainingTime = time;
    }

    // Method to get the remaining time, rounded to two decimal places
    public double GetTime()
    {
        return Math.Round(_remainingTime, 2);
    }

    // Method to check if the time has run out
    public bool HasTimeRunOut()
    {
        return _remainingTime <= 0;
    }
}
