using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorManager : MonoBehaviour
{
    public AudioClip[] stepClips;
    public int totalsteps;
    private AudioSource narratorSource;
    // Start is called before the first frame update
    void Start()
    {
        narratorSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
public void PlayNarrator(int run)
    {
        if (narratorSource.isPlaying == false && run <= totalsteps && run >=1)
        {
            narratorSource.clip = stepClips[run - 1];
            narratorSource.Play();
           //Debug.Log("Narrator play " + run);
        }
    }
    public void StopPlayNarrator()
    {
         narratorSource.Stop(); 
    }
}
