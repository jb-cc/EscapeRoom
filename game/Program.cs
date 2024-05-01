using libs;
using System;
using System.Timers;

class Program
{

    private static System.Timers.Timer gameTimer;
    private static int timeLeft = 60;

    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
        var engine = GameEngine.Instance;
        var inputHandler = InputHandler.Instance;
        var collision = Collision.Instance;
        //var player = Player.Instance;

        SetupTimer();

        engine.Setup();
        //engine.SetFocused(player);

        // Main game loop
        while (true)
        {
            if (timeLeft <= 0) {
                Console.Clear();
                Console.WriteLine("Time is up!");
                Environment.Exit(0);
            }

            Console.Clear();
            engine.Render();

            // Handle keyboard input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            inputHandler.Handle(keyInfo);

            // After a move is made
            if (engine.CheckWinCondition()) {
                Console.Clear();
                //Console.WriteLine("Congratulations! You Win!");
                FileHandler.nextLevel();

                engine.Setup();
                //Set Patth paapjwadajdw
                //Console.ReadKey(true);
                //Environment.Exit(0); // Exit the game after displaying the win message.
            }
        }
    }

    static void SetupTimer() {
        gameTimer = new System.Timers.Timer(1000);
        gameTimer.Elapsed += OnTimedEvent;
        gameTimer.AutoReset = true;
        gameTimer.Enabled = true;
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e) {
        var engine = GameEngine.Instance;

        if (timeLeft > 0) {
            timeLeft--;
            //Console.SetCursorPosition(0, 0);
            //Console.WriteLine($"Time left: {timeLeft} seconds");
            engine.SetTimeLeft(timeLeft);
            engine.Render();
        } else {
            gameTimer.Stop();
        }
    }


}