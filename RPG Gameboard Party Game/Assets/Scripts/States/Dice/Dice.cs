using System;
using System.Collections;
using States.GameBoard;
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
            gameSystem.diceManager.MoveDiceToTarget(gameSystem.playerData.Player.transform);
            
            foreach (var die in gameSystem.dice)
            {
                die.transform.parent.gameObject.SetActive(true);
                die.GetComponent<AnimateDice>().rotate = true;
            }

            yield return null;
        }

        public override IEnumerator Exit()
        {
            foreach (var die in gameSystem.dice)
            {
                die.transform.parent.gameObject.SetActive(false);
            }

            yield return null;
        }

        public override void Tick()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Exit Dice Option");
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new Player.Player(gameSystem)));
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Hello from the dice hit");
                PlayParticleEffect();
                SetDiceNumber();
                gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerMove(gameSystem)));
            }
            
        }
        
        private void SetDiceNumber()
        {
            for (var i = 0; i < gameSystem.dice.Length; i++)
            {
                gameSystem.diceNumbers[i] = GetDiceNumber();
                var animateDice = gameSystem.dice[i].GetComponentInChildren<AnimateDice>();
                animateDice.SetDiceToNumber(gameSystem.diceNumbers[i]);
            }
        }

        private int GetDiceNumber()
        {
            var die = UnityEngine.Random.Range(1, 7);

            Debug.Log("Dice = " + die);
            return die;
        }

        private void PlayParticleEffect()
        {
            foreach (var die in gameSystem.dice)
            {
                die.GetComponentInChildren<DiceParticleEffects>().PlayDiceHitEffect();
            }
        }
    }
}