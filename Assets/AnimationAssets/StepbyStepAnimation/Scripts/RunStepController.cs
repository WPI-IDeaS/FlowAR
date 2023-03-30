
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class RunStepController : MonoBehaviour
    //,IMixedRealitySpeechHandler
{


    public int runstep;
    public int totalsteps=25;
    public GameObject next;
    public GameObject back;

    public class Descripition
    {
        public string run;
        public string text;
    }
    public Descripition[] description;

    public GameObject[] stepObject;
    public Material notDoneMaterial;
    public Material doneMaterial;

    public GameObject hintBtn;
    private int hintIndex=0;
    public int[] hint;
    public GameObject[] hintPanel;
    public GameObject backPanel;
    private bool hintIsOn;

    public GameObject TitleBoard;
    public GameObject DescriptionBoard;
    private Text titleText;
    private Text descriptionText;

    public GameObject[] audioManager;
    void Start()
    {
        runstep = 0;
        next.GetComponent<Interactable>().OnClick.AddListener(delegate { NextStep(); });
        back.GetComponent<Interactable>().OnClick.AddListener(delegate { BackStep(); });
        hintBtn.GetComponent<Interactable>().OnClick.AddListener(delegate { PressHint(); });
        SetButtonVisibility(runstep);
        LoadDescription();
        InitiatePanel();
    }


    public void NextStep()
    {
        runstep += 1;
        Write();
        ChangeNextMat();
        HideHintPanel();
        SetButtonVisibility(runstep);
        if (runstep >0)
            this.GetComponent<StepAnimationController>().PlayStepAnimation(runstep);
        //replay is handled in StepAnimationController
        audioManager[GeneralSetting.voiceId].GetComponent<NarratorManager>().StopPlayNarrator();
        audioManager[GeneralSetting.voiceId].GetComponent<NarratorManager>().PlayNarrator (runstep);
    }
    public void BackStep()
    {
        runstep -= 1;
        HideHintPanel();
        Write();
        ChangeBackMat();
        SetButtonVisibility(runstep);
        if (runstep <= totalsteps)
            this.GetComponent<StepAnimationController>().PlayStepAnimation(runstep);
        audioManager[GeneralSetting.voiceId].GetComponent<NarratorManager>().StopPlayNarrator();
        audioManager[GeneralSetting.voiceId].GetComponent<NarratorManager>().PlayNarrator(runstep);
    }




    private void SetButtonVisibility(int run)
    {

        if (run == 0)
        {
            back.SetActive(false);
            next.SetActive(true);

        }
        if (run < totalsteps && run > 0)
        {
            back.SetActive(true);
            next.SetActive(true);

        }
        if (run == totalsteps )
        {
            back.SetActive(true);
            next.SetActive(false);
        }
        Debug.Log("Set Object Visibility");

        if (CheckHasHint(run))
        {
            hintBtn.SetActive(true);
        }
        else
        {
            hintBtn.SetActive(false);
        }
    }

    public void Write()
    {
        if (runstep != 0)
        {
            titleText.text = "STEP   " + runstep + " / " + totalsteps;
            descriptionText.text = description[runstep].text;

        }
        if (runstep == 0 || runstep < 0)
        {
            titleText.text = "GENERAL VIEW  -  " + totalsteps + " STEPS";
            descriptionText.text = "Welcome!";
        }
    }

    private void InitiatePanel()
    {
        titleText = TitleBoard.GetComponent<Text>();
        descriptionText = DescriptionBoard.GetComponent<Text>();
    }
    public void LoadDescription()
    {
        ReadCSV temp = new ReadCSV("Steps");
        temp.Read();
        int dataLength = temp.GetLength();
        description = new Descripition[dataLength];
        for (int i = 0; i < dataLength && (temp.Data[i] != null); i++)
        {
            description[i] = new Descripition();
            description[i].run = temp.Data[i][0];
            description[i].text = temp.Data[i][1];
            //Debug.Log("++++" + description[i].text);
        }

    }

    public string FindDescriptionText(int run)
    {
        if (run >= 0) 
            return (description[run].text);
        else
            return ("Hello");
    }

    public void ChangeNextMat()
    {
        if (runstep > 0)
        {
            Renderer rend = stepObject[runstep-1].GetComponent<Renderer>();
            Debug.Log("next current" + runstep);
            if (rend != null)
            {
                rend.material = doneMaterial;
            }
        }
    }

    public void ChangeBackMat()
    {
        if (runstep <stepObject.Length)
        {
            Renderer rend = stepObject[runstep].GetComponent<Renderer>();
            Debug.Log("back current" + runstep);
            if (rend != null)
            {
                rend.material = notDoneMaterial;
            }
        }
    }

    public void ShowSpeechCommandNext()
    {
        Debug.Log("Next");
    }
    public void ShowSpeechCommandBack()
    {
        Debug.Log("Back");
    }

    public bool CheckHasHint(int _step)
    {
        for(int i = 0; i < hint.Length; i++)
        {
            if (_step == hint[i])
            {
                hintIndex = i;
                return true;
            }
        }
        return false;
    }

    public void PressHint()
    {
        if (!hintIsOn)
        {
            ShowHintPanel();
        }
        else
        {
            HideHintPanel();
        }
    }

    public void ShowHintPanel()
    {
        hintPanel[hintIndex].SetActive(true);
        backPanel.SetActive(false);
        hintIsOn = true;
    }

    public void HideHintPanel()
    {
        hintPanel[hintIndex].SetActive(false);
        backPanel.SetActive(true);
        hintIsOn = false;
    }

}
