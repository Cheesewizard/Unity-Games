using UnityEngine;

public class DiceParticleEffects : MonoBehaviour
{
    [Header("Particle Effects")] 
    [SerializeField]
    private ParticleSystem _diceHitEffect;

    [SerializeField] 
    private ParticleSystem _diceIdleEffect;

    public void Start()
    {
        //diceRotate.rotate = true;
        _diceIdleEffect.Play();

        //DiceIsPlaying?.Invoke(true);k
        //DiceIsPlaying?.Invoke(true);k
    }

    public void PlayDiceHitEffect()
    {
        //diceRotate.rotate = false;
        _diceIdleEffect.Stop();
        _diceHitEffect.Play();

        //DiceIsHit?.Invoke();
        //DiceIsPlaying?.Invoke(false);
    }
}