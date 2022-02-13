using Mirror;
using UnityEngine;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/


namespace Network.Lobby
{
    [AddComponentMenu("")]
    public class NetworkRoomManagerExtended : NetworkRoomManager
    {
        [Header("The chosen gameboard level")] public GameObject gameBoard;

        /// <summary>
        /// This is called on the server when a networked scene finishes loading.
        /// </summary>
        /// <param name="sceneName">Name of the new scene.</param>
        public override void OnRoomServerSceneChanged(string sceneName)
        {
            // spawn the initial batch of Rewards
            if (sceneName == GameplayScene)
            {
                //Spawner.InitialSpawn();
            }
        }

        /// <summary>
        /// Called just after GamePlayer object is instantiated and just before it replaces RoomPlayer object.
        /// This is the ideal point to pass any data like player name, credentials, tokens, colors, etc.
        /// into the GamePlayer object as it is about to enter the Online scene.
        /// </summary>
        /// <param name="roomPlayer"></param>
        /// <param name="gamePlayer"></param>
        /// <returns>true unless some code in here decides it needs to abort the replacement</returns>
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer,
            GameObject gamePlayer)
        {
            var playerData = gamePlayer.GetComponentInChildren<Game.Player.Player>();
            if (playerData != null)
            {
                playerData.playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                playerData.playerId = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
            }
            
            return true;
        }

        

        public override void OnRoomStopClient()
        {
            base.OnRoomStopClient();
        }

        public override void OnRoomStopServer()
        {
            base.OnRoomStopServer();
        }

        /*
        This code below is to demonstrate how to do a Start button that only appears for the Host player
        showStartButton is a local bool that's needed because OnRoomServerPlayersReady is only fired when
        all players are ready, but if a player cancels their ready state there's no callback to set it back to false
        Therefore, allPlayersReady is used in combination with showStartButton to show/hide the Start button correctly.
        Setting showStartButton false when the button is pressed hides it in the game scene since NetworkRoomManager
        is set as DontDestroyOnLoad = true.
    */

        bool showStartButton;

        public override void OnRoomServerPlayersReady()
        {
            // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
            showStartButton = true;
#endif
        }

        public override void OnGUI()
        {
            PlayerReadyGUI();

            if (!allPlayersReady || !showStartButton || !GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
            {
                return;
            }

            // set to false to hide it in the game scene
            showStartButton = false;

            ServerChangeScene(GameplayScene);
        }

        private void PlayerReadyGUI()
        {
            if (!showRoomGUI)
                return;

            if (NetworkServer.active && IsSceneActive(GameplayScene))
            {
                GUILayout.BeginArea(new Rect(Screen.width - 150f, 10f, 140f, 30f));
                if (GUILayout.Button("Return to Room"))
                    ServerChangeScene(RoomScene);
                GUILayout.EndArea();
            }

            if (IsSceneActive(RoomScene))
                GUI.Box(new Rect(10f, 180f, 520f, 150f), "PLAYERS");
        }
    }
}