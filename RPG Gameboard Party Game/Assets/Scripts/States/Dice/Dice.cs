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

        // private void Start()
        // {
        //     GameManager.Instance.AddDice(this);
        //     diceRotate = GetComponent<RotateSelf>();
        // }

        public override IEnumerator Enter()
        {
            foreach (var die in gameSystem.dice)
            {
                die.SetActive(true);
                die.GetComponentInChildren<AnimateDice>().rotate = true;
            }

            yield return null;
        }

        public override IEnumerator Exit()
        {
            foreach (var die in gameSystem.dice)
            {
                die.SetActive(false);
            }

            yield return null;
        }

        public override void Tick()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Exit Dice Option");
                gameSystem.SetState(new Player.Player(gameSystem));
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Hello from the dice hit");
                PlayParticleEffect();
                SetDiceNumber();
                gameSystem.StartCoroutine(TransitionToState(1));
            }
            
        }

        private IEnumerator TransitionToState(int timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            gameSystem.SetState(new PlayerMove(gameSystem));
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