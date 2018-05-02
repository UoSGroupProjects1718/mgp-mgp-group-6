using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClip;

    // have created assuming 8 sounds in array:

    //index 0: bad tap
    //index 1: good tap
    //index 2: great tap
    //index 3: perfect tap

    //index 4: powerhit powerup activated
    //index 5: extrachance powerup activated
    //index 6: recover powerup activated

    //index 7: timeout, change turn

    //index 8: spit hit


    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }
	

	void Update ()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    PlayAudio(1);
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    PlayAudio(0);
        //}
	}

    public void PlayAudio(int clip)
    {
        audioSource.PlayOneShot(audioClip[clip]);
    }
}
