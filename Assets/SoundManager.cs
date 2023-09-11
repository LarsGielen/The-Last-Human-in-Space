using Project.Entity.Player.Core;
using UnityEngine;

namespace Project
{
    public class SoundManager : CoreComponent
    {
        public void PlayAudio(AudioSource audioSource)
        {
            if (audioSource != null) { audioSource.Play(); }
            else { Debug.LogWarning("AudioSource is null"); }
        }


        //Test
        [SerializeField] GameObject defaultAudio;
        [ContextMenu("Play audio Test")]
        public void PlayAudioTest()
        {
            GameObject audioObject = Instantiate(defaultAudio);
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            PlayAudio(audioSource);
            Destroy(audioObject, audioSource.clip.length * 2);
        }
    }
}