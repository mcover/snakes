using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{


    public AudioClip success;
    public AudioClip error;
    public AudioClip move;
    [HideInInspector]
    public bool soundOn = true;
    [HideInInspector]
    public float volume = 1f;
    public Toggle toggle;
    public Slider volumeControl;

    private AudioSource soundPlayer;
    // Use this for initialization
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        toggle.onValueChanged.AddListener((value) =>
        {
            OnSoundToggle(value);
        });//Do this in Start() for example
        soundOn = true;
        volumeControl.onValueChanged.AddListener((value) =>
        {
            OnVolumeChange(value);
        });
        volumeControl.value = volume;
        soundPlayer.volume = volume;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlaySuccessSound()
    {
        if (soundOn)
        {
            soundPlayer.clip = success;
            soundPlayer.Play();
        }
    }

    public void PlayErrorSound()
    {
        if (soundOn)
        {
            soundPlayer.clip = error;
            soundPlayer.Play();
        }
    }

    public void PlayMoveSound()
    {
        if (soundOn)
        {
            soundPlayer.clip = move;
            soundPlayer.Play();
        }
    }

    public void OnSoundToggle(bool on)
    {
        soundOn = !soundOn;
    }

    public void OnVolumeChange(float num)
    {
        volume = num;
        soundPlayer.volume = volume;
    }



}