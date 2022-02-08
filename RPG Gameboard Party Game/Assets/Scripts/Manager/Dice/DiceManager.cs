using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers;
using Mirror;
using States.GameBoard.StateSystem;
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

        [Command(requiresAuthority = false)]
        public void CmdSpawnDice(int amount)
        {
            InstantiateDiceInScene(amount);
            MoveDiceEqualDistanceApart();
            NetworkHelpers.RpcToggleObjectVisibility(dicePosition.gameObject, false);
            OrganiseDiceInScene();
            SpawnDice();
            MoveDiceToTarget(GameBoardSystem.Instance.playerData.Player.transform);
        }

        [ClientRpc]
        private void InstantiateDiceInScene(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var die = Instantiate(dice, dicePosition.transform.position, Quaternion.identity);
                diceInScene.Add(die);
            }
        }

        private void SpawnDice()
        {
            // Spawn the dice that have been setup in the client worlds
            foreach (var die in diceInScene)
            {
                NetworkServer.Spawn(die);
            }
        }

        [ClientRpc]
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

        [ClientRpc]
        private void OrganiseDiceInScene()
        {
            var totalBounds = diceInScene[0].GetComponentInChildren<MeshRenderer>().bounds;

            // Skip 1 as we dont want to parent object 
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

        [ClientRpc]
        private void MoveDiceToTarget(Transform target)
        {
            var position = target.transform.position;
            dicePosition.transform.position = new Vector3(position.x, position.y + 2, position.z);
        }

        [ClientRpc]
        public void RemoveDiceFromScene()
        {
            foreach (var die in diceInScene)
            {
                NetworkServer.Destroy(die);
            }

            diceInScene.Clear();
        }
    }
}