using System.Collections;
using Camera;
using GameBoard.Dice;
using States.Player;
using UnityEngine;

namespace States.GameBoard
{
    public class GameBoardSystem : StateMachine
    {
        // Game Board
        public Route currentRoute;
        [HideInInspector] public GameObject currentTile;

        // Dice
        public GameObject[] dice;
        [HideInInspector] public int[] diceNumbers;

        // Movement / Players
        public GameObject[] players;
        public int playerSpeed = 6;
        public PlayerManager playerManager;

        // Turn Order
        public TurnManager turnManager;

        // Dice
        public DiceManager diceManager;

        // Cameras
        public GameObject[] cameras;
        public CameraManager cameraManager;

        // Data
        [HideInInspector] public PlayerData playerData;

        private void Awake()
        {
            diceNumbers = new int[dice.Length];
            turnManager = new TurnManager(true, players.Length, 10);
            playerManager = new PlayerManager(players);
            cameraManager = new CameraManager(cameras);
            diceManager = new DiceManager(dice);
            
            playerData = playerManager.GetPlayerDataFromPlayerIndex(0);
        }

        public void Start()
        {
            currentState = new StartTurn(this);
            SetState(currentState);
        }

        void Update()
        {
            currentState.Tick();
        }
        
        public IEnumerator TransitionToState(float timeToWait, GameStates state)
        {
            yield return new WaitForSeconds(timeToWait);
            SetState(state);
        }
    }
}