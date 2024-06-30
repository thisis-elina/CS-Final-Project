using libs;

public sealed class TimeLord {


    private DateTime _startingTimestamp;

    private double _remainingTime;

    public static TimeLord instance;

    public static TimeLord Instance{
        get{ 
            if(instance == null){
                instance = new TimeLord();
            }
            return instance;
        }
    }

    private TimeLord(){
        _startingTimestamp = DateTime.Now;
        //TODO: Change to GameSettings.JSON page
        _remainingTime = 100;
    }

    public void SubtractTime(){

        if(GameEngine.Instance.GameState != GameEngineState.Playing){
            _startingTimestamp = DateTime.Now;
            return;
        }

        double difference = System.Math.Abs((DateTime.Now - _startingTimestamp).TotalSeconds);
        _startingTimestamp = DateTime.Now;
        _remainingTime -= difference;

        if( _remainingTime <= 0){
            _remainingTime = 0;
        }

    }

    public void SetTime(double time){
        _remainingTime = time;
    }

    public double GetTime(){
        return Math.Round(_remainingTime, 2);;
    }

    public bool HasTimeRunOut(){
        return _remainingTime <= 0;
    }

}