using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Diagnostics;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameObject bgm;
    public GameObject n_example;
    public GameObject narration;
    public AudioClip[] n_voice;//narration
    public AudioClip[] example_voice;

    private AudioSource narration_audio;
    private AudioSource n_example_audio;
    private AudioSource bgm_audio;

    void Start()
    {
        if (bgm != null)
        {
            bgm_audio = bgm.GetComponent<AudioSource>();
        }
        if (narration != null)
        {
            narration_audio = narration.GetComponent<AudioSource>();
        }
        if (n_example != null)
        {
            n_example_audio = n_example.GetComponent<AudioSource>();
        }
        
    }

    void Update()
    {
        if (bgm_audio != null)
        {
            if (GeneralSetting.isBGMOn)
            {
                bgm_audio.volume = GeneralSetting.bgmVolume;
            }
            else
            {
                bgm_audio.volume = 0;
            }
        }
        if (narration_audio != null)
        {
            narration_audio.volume = GeneralSetting.narratorVolume;
            narration_audio.clip = n_voice[GeneralSetting.voiceId];
        }
        if (n_example_audio != null)
        {
            n_example_audio.volume = GeneralSetting.narratorVolume;
        }
        
    }

    public void BGMVolumeUpdate(SliderEventData eventData)
    {
        GeneralSetting.bgmVolume = eventData.NewValue;
    }

    public void NarrationVolumeUpdate(SliderEventData eventData)
    {
        GeneralSetting.narratorVolume = eventData.NewValue;
    }

    public void SetVoice(int _i)
    {
        if (GeneralSetting.voiceId != _i)
        {
            GeneralSetting.voiceId = _i;
            n_example_audio.clip = example_voice[GeneralSetting.voiceId];
            n_example_audio.Play();
        }

    }
}
