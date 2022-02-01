using UnityEngine;

public class DiceAudioManager : MonoBehaviour
{
    public static DiceAudioManager Instance { get; private set; }
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

    public AudioSource diceIdle;
    public AudioSource diceHit;

    public void PlayDiceIdleSound(bool playing)
    {
        if (playing)
        {
            diceIdle.Play();
            return;
        }

        diceIdle.Stop();
    }

    public void PlayDiceHitSound()
    {
        Debug.Log("Playing hit sound");
        diceHit.Play();
    }
}
