using System.Collections.Generic;
using System.Linq;
using Helpers;
using Mirror;
using UnityEngine;

namespace Manager.Dice
{
    public class DiceManager : NetworkBehaviour
    {
        public static DiceManager Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }


        public GameObject dice;
        public Transform dicePosition;
        public readonly List<GameObject> diceInScene = new List<GameObject>();

        [Client]
        public void MoveDiceToTarget(Transform target)
        {
            var position = target.transform.position;
            dicePosition.transform.position = new Vector3(position.x, position.y + 2, position.z);
        }


        [ClientRpc]
        public void RpcSpawnDice(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var die = Instantiate(dice, dicePosition.transform.position, Quaternion.identity);
                NetworkServer.Spawn(die);
                diceInScene.Add(die);
            }

            MoveDiceEqualDistanceApart();
            OrganiseDiceInScene();
            NetworkHelpers.RpcToggleObjectVisibility(dicePosition.gameObject, false);
        }

        [Client]
        private void MoveDiceEqualDistanceApart()
        {
            const float offset = 3f;

            // Arrange dice as more get added dynamically. Only needs arranging if more that 1 is added
            if (diceInScene.Count <= 1)
            {
                return;
            }

            for (var i = 0; i < diceInScene.Count; i++)
            {
                var pos = diceInScene[i].gameObject.transform.position;
                if (i == 0)
                {
                    diceInScene[i].transform.position = new Vector3(pos.x - offset, pos.y, pos.z);
                    continue;
                }

                var previousPos = diceInScene[i - 1].gameObject.transform.position;
                var width = diceInScene[i - 1].gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.x;
                diceInScene[i].transform.position = new Vector3(previousPos.x + ((width / 2) * offset), pos.y, pos.z);
            }
        }

        [Client]
        private void OrganiseDiceInScene()
        {
            var totalBounds = diceInScene[0].GetComponentInChildren<MeshRenderer>().bounds;
            foreach (var die in diceInScene.Skip(1))
            {
                var r = die.GetComponentInChildren<MeshRenderer>();
                totalBounds.Encapsulate(r.bounds);
            }

            // Set dice parent to be the center of the dice array bounds
            dicePosition.transform.position = totalBounds.center;

            foreach (var die in diceInScene)
            {
                die.transform.SetParent(dicePosition.transform);
            }
        }
    }
}