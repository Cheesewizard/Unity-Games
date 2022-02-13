using Mirror;

namespace Game.Player
{
    public struct PlayerData
    {
        public string name;
        public uint networkInstanceId;
        public int boardLocationIndex;
        public int playerId;
        public int playerTurnCounter;
        public int PlayerSpeed;
        public NetworkIdentity Identity;
    }
}