using System;
using UnityEngine;


namespace States.Player
{
    [Obsolete]
    public class AddPlayer : MonoBehaviour, IPlayer
    {
        public int Id { get; set; }

        private void Start()
        {
            //GameManager.Instance.PlayerSetup(this);
        }

        //Events
        public Action<bool> PlayerMoving;
        public Action<int> PlayerSteps;

        private void OnEnable()
        {
            PlayerMoving += GameManager.Instance.StartDice;
            PlayerSteps += UIManager.Instance.UpdateMovement;
        }

        private void OnDisable()
        {
            PlayerMoving -= GameManager.Instance.StartDice;
            PlayerSteps -= UIManager.Instance.UpdateMovement;
        }
    }
}
