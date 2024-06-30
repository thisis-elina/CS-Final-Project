using libs;

class Program
{    
    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
        var engine = GameEngine.Instance;
        TimeLord timeLord = TimeLord.Instance;
        
        Thread t = new Thread(InputHandling);
        t.Start();

        engine.Setup();

        // Main game loop
        do 
        {   
            timeLord.SubtractTime();
            engine.Render();
            
            if(engine.IsGameComplete || timeLord.HasTimeRunOut()){
                break;
            }

            Thread.Sleep(1000/engine.FrameRate);
 
        }while(true);

        if(timeLord.HasTimeRunOut()){
            GameEngine.Instance.GameState =  GameEngineState.Lost;
            Console.WriteLine("Time has run out, time to take a shower!!!!");
        }else{
            Console.WriteLine("You have won! Now touch some grass!");
        } 
    }


    public static void InputHandling(){
        var inputHandler = InputHandler.Instance;
        while(true){
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            inputHandler.Handle(keyInfo);
        }
    }
}