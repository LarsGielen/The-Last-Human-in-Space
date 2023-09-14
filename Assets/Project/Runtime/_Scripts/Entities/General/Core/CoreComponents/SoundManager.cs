using Project.Entity.CoreSystem;
using UnityEngine;

namespace Project
{
    public class SoundManager : CoreComponent
    {
        public void PlayAudio(AudioClip audioClip, float volume)
        {
            if (audioClip != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, volume);
            }
            else
            {
                Debug.LogWarning("audioClip is null");
            }
        }
    }
}