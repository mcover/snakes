using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {


    public AudioClip success;
    public AudioClip error;
    public AudioClip move;

    private AudioSource soundPlayer;
	// Use this for initialization
	void Start () {
        soundPlayer = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void PlaySuccessSound()
    {
        soundPlayer.clip = success;
        soundPlayer.Play();
    }

    public void PlayErrorSound()
    {
        soundPlayer.clip = error;
        soundPlayer.Play();
    }

    public void PlayMoveSound()
    {
        soundPlayer.clip = move;
        soundPlayer.Play();
    }
}
