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
    public class Dice : GameStates
    {
        // Events
        public Action<bool> DiceIsPlaying;
        public Action DiceIsHit;

        public Dice(GameBoardSystem gameSystem) : base(gameSystem)
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
            DiceManager.Instance.MoveDiceToTarget(gameSystem.playerData.Player.transform);
            NetworkHelpers.RpcToggleObjectVisibility(DiceManager.Instance.dicePosition.gameObject,true);

            foreach (var die in DiceManager.Instance.diceInScene)
            {
                die.GetComponent<AnimateDice>().rotate = true;
            }

            yield return null;
        }

        public override IEnumerator Exit()
        {
            NetworkHelpers.RpcToggleObjectVisibility(DiceManager.Instance.dicePosition.gameObject,false);
            yield return null;
        }


        public override void Tick()
        {
            if (gameSystem.CheckIfHasAuthority())
            {
                CheckInput();
            }
        }
        
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
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new Player.Player(gameSystem)));
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
                SetDiceNumber();
                gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerMove(gameSystem)));
            }
        }


        [Client]
        private void SetDiceNumber()
        {
            for (var i = 0; i < DiceManager.Instance.diceInScene.Count; i++)
            {
                gameSystem.diceNumbers.Add(GetDiceNumber());
                var animateDice = DiceManager.Instance.diceInScene[i].GetComponentInChildren<AnimateDice>();
                animateDice.SetDiceToNumber(gameSystem.diceNumbers[i]);
            }
        }

        [Server]
        private int GetDiceNumber()
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