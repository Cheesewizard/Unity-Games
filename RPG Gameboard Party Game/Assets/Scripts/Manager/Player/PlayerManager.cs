using System;
using Manager.Player.Spawn;
using Mirror;
using States.GameBoard.StateSystem;

namespace Manager.Player
{
    public class PlayerManager : NetworkBehaviour
    {
        private NetworkIdentity _networkIdentity;

        //Events
        public Action<bool> PlayerMoving;
        public Action<int> PlayerSteps;

        public override void OnStartLocalPlayer() 
        {
            GetPlayerId();
            GameBoardSystem.Instance.playerDataManager.CmdAddPlayer(_networkIdentity, gameObject);
            PlayerSpawnManager.Instance.SpawnPlayerAt(gameObject);
        }

        private void GetPlayerId()
        {
            var networkIdentity = gameObject.GetComponent<NetworkIdentity>();
            if (networkIdentity != null)
            {
                _networkIdentity = networkIdentity;
            }
        }

        public override void OnStopClient()
        {
            GameBoardSystem.Instance.playerDataManager.CmdRemovePlayer(_networkIdentity.netId);
        }
        
        // Set up player input here. Custom buttons, controller, keyboard etc

        private void OnEnable()
        {
            //PlayerMoving += GameManager.Instance.StartDice;
            //PlayerSteps += UIManager.Instance.UpdateMovement;
        }

        private void OnDisable()
        {
            //PlayerMoving -= GameManager.Instance.StartDice;
            //PlayerSteps -= UIManager.Instance.UpdateMovement;
        }
    }
}