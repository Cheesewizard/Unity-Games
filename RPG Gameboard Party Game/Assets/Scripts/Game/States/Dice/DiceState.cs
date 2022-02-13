using System;
using System.Collections;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using GameBoard.Dice;
using Helpers;
using Manager.Dice;
using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace Game.States.Dice
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
            DiceManager.Instance.CmdSpawnDice(2);
            NetworkHelpers.RpcToggleObjectVisibility(DiceManager.Instance.diceParent.gameObject, setup);
        }

        public override IEnumerator Exit()
        {
            DiceManager.Instance.CmdRemoveDiceFromScene();
            yield return null;
        }

        public override void Tick()
        {
            CheckInput();
        }

        [Client]
        private void CheckInput()
        {
            CmdExitDiceButton();
            HitDiceButton();
        }

        [Command]
        private void CmdExitDiceButton()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Exit Dice Option");
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new PlayerState(gameSystem)));
            }
        }

     
        [Command]
        private void HitDiceButton()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Hit the dice block");
                RpcPlayParticleEffect();
                CmdSetGetRandomDiceNumber();
                CmdSetDiceToNumber();
                GoToPlayerMoveState();
            }
        }
        
        [Command]
        private void CmdSetGetRandomDiceNumber()
        {
            gameSystem.diceNumbers.Clear();
            for (var i = 0; i < DiceManager.Instance.diceInScene.Count; i++)
            {
                gameSystem.diceNumbers.Add(GetRandomDiceNumber());
            }
        }

        [Command]
        private void CmdSetDiceToNumber()
        {
            for (var i = 0; i < DiceManager.Instance.diceInScene.Count; i++)
            {
                SetDiceToNumber(i);
            }
        }

        [ClientRpc]
        private void SetDiceToNumber(int index)
        {
            var animateDice = DiceManager.Instance.diceInScene[index].GetComponentInChildren<AnimateDice>();
            if (animateDice != null)
            {
                animateDice.CmdSetDiceToNumber(gameSystem.diceNumbers[index]);
            }
        }

        [Server]
        private int GetRandomDiceNumber()
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

        private void GoToPlayerMoveState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerMoveState(gameSystem)));
        }
    }
}