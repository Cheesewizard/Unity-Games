using System;
using System.Collections;
using GameBoard.Dice;
using Helpers;
using Manager.Dice;
using Mirror;
using States.GameBoard.StateSystem;
using States.Player;
using UnityEngine;

namespace States.Dice
{
    public class DiceState : GameStates
    {
        // Events
        public Action<bool> DiceIsPlaying;
        public Action DiceIsHit;

        public DiceState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }


        // private void OnEnable()
        // {
        //     DiceIsPlaying += DiceAudioManager.Instance.PlayDiceIdleSound;
        //     DiceIsHit += DiceAudioManager.Instance.PlayDiceHitSound;
        // }
        //
        // private void OnDisable()
        // {
        //     DiceIsPlaying -= DiceAudioManager.Instance.PlayDiceIdleSound;
        //     DiceIsHit -= DiceAudioManager.Instance.PlayDiceHitSound;
        // }

        public override IEnumerator Enter()
        {
            DiceSetup(true);
            yield return null;
        }

        private void DiceSetup(bool setup)
        {
            if (!gameSystem.IsPlayerTurn())
            {
                return;
            }

            DiceManager.Instance.CmdSpawnDice(2);
            NetworkHelpers.RpcToggleObjectVisibility(DiceManager.Instance.dicePosition.gameObject, setup);
        }

        public override IEnumerator Exit()
        {
            DiceManager.Instance.RemoveDiceFromScene();
            yield return null;
        }


        public override void Tick()
        {
            if (gameSystem.IsPlayerTurn())
            {
                CheckInput();
            }
        }

        [Client]
        private void CheckInput()
        {
            CmdExitDiceButton();
            CmdHitDiceButton();
        }

        [Command]
        private void CmdExitDiceButton()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Exit Dice Option");
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new Player.PlayerState(gameSystem)));
                return;
            }
        }

        [Command]
        private void CmdHitDiceButton()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Hello from the dice hit");
                RpcPlayParticleEffect();
                RpcSetDiceNumber();
                gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerMove(gameSystem)));
            }
        }


        [ClientRpc]
        private void RpcSetDiceNumber()
        {
            for (var i = 0; i < DiceManager.Instance.diceInScene.Count; i++)
            {
                gameSystem.diceNumbers.Add(RpcGetDiceNumber());
                var animateDice = DiceManager.Instance.diceInScene[i].GetComponentInChildren<AnimateDice>();
                animateDice.SetDiceToNumber(gameSystem.diceNumbers[i]);
            }
        }

        [Server]
        private int RpcGetDiceNumber()
        {
            var die = UnityEngine.Random.Range(1, 7);
            Debug.Log("Dice = " + die);
            return die;
        }


        [ClientRpc]
        private void RpcPlayParticleEffect()
        {
            foreach (var die in DiceManager.Instance.diceInScene)
            {
                die.GetComponentInChildren<DiceParticleEffects>().PlayDiceHitEffect();
            }
        }
    }
}