using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAnimationController : MonoBehaviour
{
    public GameObject[] stepsGameObjects;
    [HideInInspector]
    public Animator[] stepsAnimations;
    private int stepLength;
    // public GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {
        stepLength = stepsGameObjects.Length;
        stepsAnimations = new Animator[stepLength];
        InitiateAnimators();
    }

    private void InitiateAnimators()
    {
        for(int i = 0; i < stepLength ; i++)
        {
            if(stepsGameObjects[i]!=null && stepsGameObjects[i].GetComponent <Animator>() != null)
            {
                stepsAnimations[i] = new Animator();
                stepsAnimations[i] = stepsGameObjects[i].GetComponent<Animator>();
                stepsGameObjects[i].SetActive(false);
            }
            stepsGameObjects[0].SetActive(true);

        }
    }
    // Update is called once per frame
    public void PlayStepAnimation(int run)
    {
        if(run<stepLength )
            stepsGameObjects[run].SetActive(false);
        if (run >= 2)
            stepsGameObjects[run - 2].SetActive(false);
        if (run >= 1)
        {
            stepsGameObjects[run - 1].SetActive(true);
            if (stepsGameObjects[run - 1].GetComponent<Animator>() != null)
            {
                stepsAnimations[run - 1].SetBool("PlayAnim", true);
            }
            // audioManager.GetComponent<NarratorManager>().PlayNarrator(run);
        }
    }

}
