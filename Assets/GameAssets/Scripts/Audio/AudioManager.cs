using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource SFXPlayer;
    [SerializeField] private AudioSource BGMPlayer;
    const float MIN_PITCH = 0.85f;
    const float MAX_PITCH = 1.15f;
    public void PlaySFX(AudioData audioData)
    {
        SFXPlayer.PlayOneShot(audioData.audioClip, audioData.volume);
    }
    public void PlayRandomSFX(AudioData audioData)
    {
        SFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioData);
    }
    public void StopBGM()
    {
        BGMPlayer.Stop();
    }
    public void SwitchBGM(AudioData audioData)
    {
        BGMPlayer.Stop();
        BGMPlayer.clip = audioData.audioClip;
        BGMPlayer.volume = audioData.volume;
        BGMPlayer.Play();
    }
}
