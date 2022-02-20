using System.Collections.Generic;
using Game.GameBoard.Dice;
using Manager.Movement;
using Manager.Player;
using Manager.Turns;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

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


        public List<GameObject> dicePrefabs = new List<GameObject>();
        [SyncVar] public GameObject currentDice;
        [SyncVar] public int previousDiceAmount;
        public int diceAmountOverride;
        public int[] debugDiceNumbers;
        public bool debug;

        private void Start()
        {
            MovementManager.Instance.serverMovement.Callback += MovementChange;
        }


        // Spawning Dice

        public void DiceSetup(int amount)
        {
            if (currentDice != null)
            {
                CmdToggleDiceEnabledInScene(true);
                return;
            }

            // Override if dice amount set in the inspector (debugging)
            amount = diceAmountOverride != 0 ? diceAmountOverride : amount;

            SpawnDice(amount);
        }

        private void SpawnDice(int amount)
        {
            if (previousDiceAmount == amount) return;

            CmdSpawnDice(amount);
            CmdMoveDiceToTarget();
        }

        [Command(requiresAuthority = false)]
        private void CmdMoveDiceToTarget()
        {
            var target = PlayerDataManager.Instance.clientPlayerData[TurnManager.Instance.currentPlayerTurnOrder]
                .Identity.gameObject;

            RpcMoveDiceToTarget(target.transform, currentDice);
        }

        [ClientRpc]
        private void RpcMoveDiceToTarget(Transform target, GameObject dice)
        {
            var position = target.transform.position;
            dice.transform.position = new Vector3(position.x, position.y + 2, position.z);
        }

        [Command(requiresAuthority = false)]
        private void CmdSpawnDice(int amount)
        {
            var dice = Instantiate(dicePrefabs[amount - 1], dicePrefabs[amount - 1].transform.position,
                Quaternion.identity);

            NetworkServer.Spawn(dice);
            previousDiceAmount = amount;
            currentDice = dice;
        }


        [Command(requiresAuthority = false)]
        public void CmdToggleDiceEnabledInScene(bool isVisible)
        {
            var target = currentDice;
            RpcToggleDiceEnabledInScene(target, isVisible);
        }

        [ClientRpc]
        private void RpcToggleDiceEnabledInScene(GameObject target, bool isVisible)
        {
            target.SetActive(isVisible);
        }


        [Command(requiresAuthority = false)]
        public void CmdRemoveDiceFromScene()
        {
            RpcRemoveDiceFromScene();
        }

        [ClientRpc]
        private void RpcRemoveDiceFromScene()
        {
            if (currentDice == null) return;

            previousDiceAmount = 0;
            NetworkServer.Destroy(currentDice);
        }


        // Dice Interactions

        public void ActivateDice()
        {
            Debug.Log("Hit the dice block");
            SetRandomDiceNumber();
        }

        private void SetRandomDiceNumber()
        {
            for (var i = 0; i < previousDiceAmount; i++)
            {
                if (!debug)
                {
                    var die = GetRandomDiceNumber();
                    MovementManager.Instance.CmdAddMovementAmount(die);
                }
                else
                {
                    MovementManager.Instance.CmdAddMovementAmount(debugDiceNumbers[i]);
                }
            }
        }

        private int GetRandomDiceNumber()
        {
            
            var die = Random.Range(1, 7);
            Debug.Log("Dice = " + die);
            return die;
        }

        private void MovementChange(SyncList<int>.Operation op, int itemindex, int oldNumber, int newNumber)
        {
            if (op == SyncList<int>.Operation.OP_ADD && currentDice != null)
            {
                CmdSetDiceToNumber(itemindex, newNumber);
            }
        }

        [Command(requiresAuthority = false)]
        private void CmdSetDiceToNumber(int index, int number)
        {
            var parent = currentDice.transform;
            var child = parent.GetChild(index);

            var animateDice = child.GetComponentInChildren<AnimateDice>();
            if (animateDice == null) return;

            animateDice.SetDiceToNumber(number);
            RpcPlayParticleEffect(index);
        }

        // Particle Effects

        [ClientRpc]
        private void RpcPlayParticleEffect(int index)
        {
            var parent = currentDice.transform;
            var child = parent.GetChild(index);

            var effect = child.GetComponentInChildren<DiceParticleEffects>();
            if (effect != null)
            {
                effect.PlayDiceHitEffect();
            }
        }
    }
}