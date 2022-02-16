using System;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.GameBoard.Dice
{
    public class AnimateDice : MonoBehaviour
    {
        private const float SpinSpeed = 500f;
        [SerializeField] private Vector3 axis;
        private const int LowAxisRange = 1;
        private const int HighAxisRange = 361; 
        public bool rotate = true;

        private void OnEnable()
        {
            rotate = true;
            // axis = new Vector3(Random.Range(LowAxisRange, HighAxisRange), Random.Range(LowAxisRange, HighAxisRange),
            //     Random.Range(LowAxisRange, HighAxisRange));
        }

        private void Start()
        {
            axis = new Vector3(Random.Range(LowAxisRange, HighAxisRange), Random.Range(LowAxisRange, HighAxisRange),
                Random.Range(LowAxisRange, HighAxisRange));
        }

        void Update()
        {
            Rotate();
        }
        
        private void Rotate()
        {
            if (rotate)
                transform.Rotate(axis, SpinSpeed * Time.deltaTime, Space.Self);
        }

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