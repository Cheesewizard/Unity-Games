using UnityEngine;

namespace GameBoard.Dice
{
    public class DiceManager
    {
        private GameObject[] _dices;

        public DiceManager(GameObject[] dices)
        {
            _dices = dices;
        }

        public void MoveDiceToTarget(Transform target)
        {
            for (int i = 0; i < _dices.Length; i++)
            {
                var position = target.transform.position;
                
                _dices[i].transform.parent.transform.position =
                    new Vector3(position.x,position.y + 2, position.z);
            }
        }
    }
}