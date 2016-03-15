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

    public AudioSource successPlayer;
    public AudioSource movePlayer;
    public AudioSource errorPlayer;
    // Use this for initialization
    void Start()
    {
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
        successPlayer.volume = volume;
        movePlayer.volume = volume;
        errorPlayer.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlaySuccessSound()
    {
        if (soundOn)
        {
            //successPlayer.clip = success;
            successPlayer.Play();
        }
    }

    public void PlayErrorSound()
    {
        if (soundOn)
        {
            //errorPlayer.clip = error;
            errorPlayer.Play();
        }
    }

    public void PlayMoveSound()
    {
        if (soundOn)
        {
            //movePlayer.clip = move;
            movePlayer.Play();
        }
    }

    public void OnSoundToggle(bool on)
    {
        soundOn = !soundOn;
    }

    public void OnVolumeChange(float num)
    {
        volume = num;
        successPlayer.volume = volume;
        movePlayer.volume = volume;
        errorPlayer.volume = volume;
    }



}