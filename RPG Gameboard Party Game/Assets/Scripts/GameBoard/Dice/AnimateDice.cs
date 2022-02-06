using Mirror;
using UnityEngine;

namespace GameBoard.Dice
{
    public class AnimateDice : NetworkBehaviour
    {
        [SerializeField] private float spinSpeed = 1f;
        [SerializeField] private Vector3 axis;
        
        [SyncVar]
        public bool rotate;
        
        [Client]
        void Update()
        {
            Rotate();
        }
        
    
        [Client]
        private void Rotate()
        {
            if (rotate)
            {
                transform.Rotate(axis * spinSpeed * Time.deltaTime, Space.Self);
            }
        }
        
        [Server]
        public void SetDiceToNumber(int diceNumber)
        {
            rotate = false;
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