using States.Player;
using UnityEngine;

namespace States.GameBoard
{
    public class GameBoardSystem : StateMachine
    {
        // Game Board
        public Route currentRoute;
        public GameObject currentTile;

        // Dice
        public GameObject[] dice;
        public int[] diceNumbers;

        // Movement / Players
        public GameObject[] players;
        public int playerSpeed = 6;
        public PlayerManager playerManager;

        // Turn Order
        public TurnManager turnManager;

        private void Awake()
        {
            
            // Set player id - Needs changing later
            for (var i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<PlayerId>().Id = i;
            }
            
            diceNumbers = new int[dice.Length];
            turnManager = new TurnManager(true, players.Length, 10);
            playerManager = new PlayerManager(players);
        }

        public void Start()
        {
            currentState = new Player.Player(this);
            SetState(currentState);
        }

        void Update()
        {
            currentState.Tick();
        }
    }
}