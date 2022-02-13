using Mirror;
using UnityEngine;

namespace GameBoard.Dice
{
    public class AnimateDice : NetworkBehaviour
    {
        private const float SpinSpeed = 500f;
        [SerializeField] private Vector3 axis;
        private const int LowAxisRange = 1;
        private const int HighAxisRange = 361;

        private void Start()
        {
            axis = new Vector3(Random.Range(LowAxisRange, HighAxisRange), Random.Range(LowAxisRange, HighAxisRange),
                Random.Range(LowAxisRange, HighAxisRange));
        }

        [Client]
        void Update()
        {
            Rotate();
        }

        [Client]
        private void Rotate()
        {
            transform.Rotate(axis, SpinSpeed * Time.deltaTime, Space.Self);
        }

        [Command]
        public void CmdSetDiceToNumber(int diceNumber)
        {
            RcpSetDiceToNumber(diceNumber);
        }

        [ClientRpc]
        private void RcpSetDiceToNumber(int diceNumber)
        {
            switch (diceNumber)
            {
                case 1:
                    transform.rotation = Quaternion.Euler(180, 0, 0);
                    break;
                case 2:
                    transform.rotation = Quaternion.Euler(270, 0, 0);
                    break;
                case 3:
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                    break;
                case 4:
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case 5:
                    transform.rotation = Quaternion.Euler(90, 0, 0);
                    break;
                case 6:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
        }
    }
}