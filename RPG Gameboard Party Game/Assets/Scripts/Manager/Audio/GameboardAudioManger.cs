using UnityEngine;

namespace Manager.Audio
{
    public class GameboardAudioManger : MonoBehaviour
    {
        public static GameboardAudioManger Instance { get; private set; }
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

        private void Start()
        {
            PlayBackgroundMusic();
        }

        public AudioSource backgroundMusic;

        public void PlayBackgroundMusic()
        {
            backgroundMusic.Play();
        }
    }
}
