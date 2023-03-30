using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public GameObject videoObj;
    public GameObject playText;
    public GameObject pauseText;
    private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressPlay()
    {
        //count++;
        if (!isPlaying)
        {
            //play
            PlayVideo();
            GeneralSetting.isBGMOn = false;
            playText.SetActive(false);
            pauseText.SetActive(true);
            Debug.Log("isPlaying ==" + isPlaying);
        }
        else
        {
            //pause
            PauseVideo();
            playText.SetActive(true);
            pauseText.SetActive(false);
            Debug.Log("isPlaying ==" + isPlaying);
        }

    }

    public void PlayVideo()
    {
        if(!isPlaying)
        {
            videoObj.GetComponent<VideoPlayer>().Play();
            isPlaying = true;
        }          
    }

    public void PauseVideo()
    {
        if (isPlaying)
        {
            videoObj.GetComponent<VideoPlayer>().Pause();
            isPlaying = false;
        }
    
    }

    public void StopVideo()
    {
        videoObj.GetComponent<VideoPlayer>().Stop();
        GeneralSetting.isBGMOn = true;
        playText.SetActive(true);
        pauseText.SetActive(false);
    }

}
