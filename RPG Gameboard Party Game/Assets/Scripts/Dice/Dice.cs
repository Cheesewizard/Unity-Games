using System;
using UnityEngine;

public class Dice : MonoBehaviour, IDice
{
    [Header("Particle Effects")]
    [SerializeField]
    private ParticleSystem hitBlock;

    [SerializeField]
    private ParticleSystem diceEffect;

    private RotateSelf diceRotate;

    // Events
    public Action<bool> DiceIsPlaying;
    public Action DiceIsHit;

    private void OnEnable()
    {
        DiceIsPlaying += DiceAudioManager.Instance.PlayDiceIdleSound;
        DiceIsHit += DiceAudioManager.Instance.PlayDiceHitSound;
    }

    private void OnDisable()
    {
        DiceIsPlaying -= DiceAudioManager.Instance.PlayDiceIdleSound;
        DiceIsHit -= DiceAudioManager.Instance.PlayDiceHitSound;
    }

    private void Start()
    {
        GameManager.Instance.AddDice(this);
        diceRotate = GetComponent<RotateSelf>();
    }

    public int GetDiceNumber()
    {
        var die = UnityEngine.Random.Range(1, 7);

        switch (die)
        {
            case 1:
                transform.rotation = Quaternion.Euler(180, 0, 0);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(270, 0, 0);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            case 4:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case 5:
                transform.rotation = Quaternion.Euler(90, 0, 0);
                break;
            case 6:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }

        Debug.Log("Dice = " + die);
        return die;
    }

    public void SetDiceState(bool playerMoving)
    {
        if (!playerMoving)
        {          
            diceRotate.rotate = true;
            diceEffect.Play();

            DiceIsPlaying?.Invoke(true);
            return;
        }

        
        diceRotate.rotate = false;
        diceEffect.Stop();
        hitBlock.Play();

        DiceIsHit?.Invoke();  
        DiceIsPlaying?.Invoke(false);
    }

}
