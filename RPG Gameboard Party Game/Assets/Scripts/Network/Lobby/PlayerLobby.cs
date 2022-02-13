using Mirror;
using UnityEngine;

namespace Network.Lobby
{
    public class PlayerLobby : NetworkRoomPlayer
    {
        public override void OnClientEnterRoom()
        {
            //Debug.Log($"OnClientEnterRoom {SceneManager.GetActiveScene().path}");
        }

        public override void OnClientExitRoom()
        {
            //Debug.Log($"OnClientExitRoom {SceneManager.GetActiveScene().path}");
        }
    
        public override void OnStartClient()
        {
            Debug.Log($"OnStartClient {gameObject}");
            
            var player = GetComponent<NetworkRoomPlayer>();
            if (player != null)
            {
                GetComponent<NetworkRoomPlayer>().CmdChangeReadyState(true); // force ready on start (debug)
            }
        }

        public override void IndexChanged(int oldIndex, int newIndex)
        {
            //Debug.Log($"IndexChanged {newIndex}");
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
            Debug.Log($"ReadyStateChanged {newReadyState}");
        }

        public override void OnGUI()
        {
            base.OnGUI();
        }
    
    

        private void Update()
        {
            //CheckInput();
        }

        private void CheckInput()
        {
            if (!Input.GetKeyDown(KeyCode.Space))
            {
                return;
            }

            var player = GetComponent<NetworkRoomPlayer>();
            if (player != null)
            {
                GetComponent<NetworkRoomPlayer>().CmdChangeReadyState(!player.readyToBegin);
            }
        }

        [Command(requiresAuthority = false)]
        private void CmdDisplayPlayerJoinedMessage()
        {
            RpcDisplayPlayerJoinedMessage();
        }
        
        [Command(requiresAuthority = false)]
        private void RpcDisplayPlayerJoinedMessage()
        {
            Debug.Log($"Player id {netId} has joined");
        }

        [Command(requiresAuthority = false)]
        private void CmdDisplayPlayerExitMessage()
        {
            RpcDisplayPlayerExitMessage();
        }
        
        [Command(requiresAuthority = false)]
        private void RpcDisplayPlayerExitMessage()
        {
            Debug.Log($"Player id {netId} has left the game");
        }
    }
}