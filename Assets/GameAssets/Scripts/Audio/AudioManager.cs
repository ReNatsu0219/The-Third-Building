using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource SFXPlayer;
    [SerializeField] private AudioSource BGMPlayer;

    public AudioData Jump;
    public AudioData Step;
    public AudioData Bite;
    public AudioData DoubleJump;
    public AudioData Break;
    public AudioData Wind;


    public AudioData BGM;

    const float MIN_PITCH = 0.85f;
    const float MAX_PITCH = 1.15f;

    protected override void Awake()
    {
        base.Awake();
        Jump = Resources.Load<AudioData>("Jump");
        Step = Resources.Load<AudioData>("Step");
        Bite = Resources.Load<AudioData>("Bite");
        DoubleJump = Resources.Load<AudioData>("DoubleJump");
        Break = Resources.Load<AudioData>("Break");

        Wind = Resources.Load<AudioData>("Wind");
        BGM = Resources.Load<AudioData>("BGM");
    }
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
