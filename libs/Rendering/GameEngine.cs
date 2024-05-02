using System.Reflection.Metadata.Ecma335;
namespace libs;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

public sealed class GameEngine
{
    private static GameEngine? _instance;
        private IGameObjectFactory gameObjectFactory;

        public static GameEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameEngine();
                }
                return _instance;
            }
        }

        private GameEngine()
        {
            gameObjectFactory = new GameObjectFactory();
            gameObjects = new List<GameObject>();
            stateHistory = new Stack<List<GameObject>>();
            map = new Map();
            playerHistory = new Stack<(int, int)>();  // Stack to store player positions for undo functionality
        }

        public void Clear() {
            gameObjectFactory = new GameObjectFactory();
            gameObjects = new List<GameObject>();
            stateHistory = new Stack<List<GameObject>>();
            map = new Map();
            playerHistory = new Stack<(int, int)>();  // Stack to store player positions for undo functionality
        }

        private GameObject? _focusedObject;
        private List<GameObject> gameObjects;
        private readonly List<GameObject> _gameObjects;
        private Map map;
        private Stack<List<GameObject>> stateHistory;  // Stack to store map states for undo functionality

        private Stack<(int, int)> playerHistory;

        private int timeLeft;
        private string textBoxText = "";

        public void SaveCurrentState()
        {
            List<GameObject> gameObjectsCopy = new List<GameObject>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Type != GameObjectType.Player)
                {
                    gameObjectsCopy.Add((GameObject)gameObject.Clone());
                }
            }
            foreach (GameObject gameObject in gameObjects)
            {
               // Console.WriteLine(gameObject.Type + " " + gameObject.PosX + " " + gameObject.PosY + " " + gameObject.CharRepresentation + " " + gameObject.Color);
            }
            stateHistory.Push(gameObjectsCopy);
            playerHistory.Push((_focusedObject.PosX, _focusedObject.PosY));
            
        }

        public void SaveProgress(){
            
            JObject mapObject = new JObject(
                new JProperty("map", new JObject(
                new JProperty("width", map.MapWidth),
                new JProperty("height", map.MapHeight)
                ))
            );
            string json = mapObject.ToString(Newtonsoft.Json.Formatting.Indented);

            string json1 = "{\"gameObjects\":"+JsonConvert.SerializeObject(gameObjects)+"}";
            //string json1 = "{""map"":{""width"":"+map.MapWidth+",""height"":"+map.MapHeight+"}, ""gameObjects"":";
            
            JObject obj1 = JObject.Parse(json);
            JObject obj2 = JObject.Parse(json1);
            obj1.Merge(obj2);

            // Convert merged JObject back to JSON string
            string mergedJson = obj1.ToString();

            FileHandler.saveJson(mergedJson);
            //File.WriteAllText(@"D:\Schule\FH\4.Semester\CD\Soko\SokobanGame\Save.json", mergedJson);

        }

        
        public void UndoMove()
        {
            Console.WriteLine("UndoMove" + stateHistory.Count);
            if (stateHistory.Count > 0)
            {
                var gameObjectsCopy = new List<GameObject>(gameObjects); // Create a copy of gameObjects

                foreach (GameObject gameObject in gameObjectsCopy)
                {
                    if (gameObject.Type != GameObjectType.Player)
                    {
                        gameObjects.Remove(gameObject); // Remove elements from the original list
                    }
                }
                
                // Add the gameObjects from the previous state to the gameObjects list
                foreach (GameObject gameObject in stateHistory.Pop())
                {
                    gameObjects.Add(gameObject);
                }

                // Restore the player position from playerHistory
                (int x, int y) = playerHistory.Pop();
                _focusedObject.PosX = x;
                _focusedObject.PosY = y;
                Render(); 
            }
            else
            {
                Console.WriteLine("No more moves to undo.");
            }
        }


        public bool CanUndo()
        {
            return stateHistory.Count > 0;
        }

        public Map GetMap()
        {
            return map;
        }

        public GameObject GetFocusedObject()
        {
            return _focusedObject;
        }

        public void Setup()
        {
            Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            dynamic gameData = FileHandler.ReadJson();  // Load game data
            map.MapWidth = gameData.map.width;
            map.MapHeight = gameData.map.height;

            foreach (var gameObject in gameData.gameObjects)
            {
                AddGameObject(CreateGameObject(gameObject));
            }

            _focusedObject = gameObjects.OfType<Player>().FirstOrDefault();  // Ensure there is a player
        }

        public void SetFocused(GameObject gameObject)
        {
            _focusedObject = gameObject;
        }

        public void Render()
        {
            Console.Clear();
            

            
                map.Initialize();
                PlaceGameObjects();
            
            
            for (int i = 0; i < map.MapHeight; i++)
            {
                for (int j = 0; j < map.MapWidth; j++)
                {
                    DrawObject(map.Get(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("=========================\nYou have " + timeLeft + " Seconds left!\n=========================");
            Console.WriteLine(textBoxText + "\n=========================");
        }

        


        public GameObject CreateGameObject(dynamic obj)
        {
            return gameObjectFactory.CreateGameObject(obj);
        }

        public void AddGameObject(GameObject gameObject)
        {
            if (!gameObjects.Any(p => p.Type == GameObjectType.Player && gameObject.Type == GameObjectType.Player))
            {
                if (gameObject.Type == GameObjectType.Button)
                {
                    foreach (var obj in gameObjects)
                    {
                        // Use pattern matching to check and cast
                        if ((obj is Chest chest) && (gameObject is Button button) && chest.ChestID == button.ChestID)
                        {
                            button.ConnectedChest = chest;
                        }
                    }
                }
                gameObjects.Add(gameObject);
            }
        }


        private void PlaceGameObjects()
        {
            foreach (GameObject obj in gameObjects)
            {
                map.Set(obj);
            }
        }

        private void DrawObject(GameObject gameObject)
        {
            Console.ResetColor();
            if (gameObject != null)
            {
                Console.ForegroundColor = gameObject.Color;
                Console.Write(gameObject.CharRepresentation);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(' ');
            }
        }


    public bool CheckWinCondition() {
        foreach (GameObject obj in gameObjects) {
            if (obj is Door door && !door.IsOpen) {
                return false; // Return false if there is a closed door
            }
        }
        return true; // Door is open, return true, game won yeeeeeee
    }

    public void SetTimeLeft(int time) {
        timeLeft = time;
    }
    public void SetTextBoxText(string text) {
        textBoxText = text;
    }
}