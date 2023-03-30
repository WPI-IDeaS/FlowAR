using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StepByStep : MonoBehaviour
{
    // Start is called before the first frame update

    //public Animator StepAnimControl;
    public GameObject[] stepComponents;
    //private bool isAnimating = false;
    //private int animStatus = 0;

    public GameObject start;
    public GameObject next;
    public GameObject back;
    public GameObject board;
    [HideInInspector]
    public int curStep;
    [HideInInspector]
    public int totalSteps;
    private int runstep;
    public float speed;

    void Awake()
    {

    }
    void Start()
    {
        //    StepAnimControl=this.GetComponent<Animator>();
        curStep = 0;
        runstep = 0;
        totalSteps = stepComponents.Length - 1;

        next.GetComponent<Interactable>().OnClick.AddListener(delegate { NextStep(); });
        start.GetComponent<Interactable>().OnClick.AddListener(delegate { NextStep(); });
        back.GetComponent<Interactable>().OnClick.AddListener(delegate { BackStep(); });

        SetButtonVisibility(curStep);
        WriteBoard_Step(FindChildBoard(board, "Title"));
        InitiateAnimator();
        //    StepAnimControl.SetInteger("StepNo", curStep);
    }

    public void NextStep()
    {
        runstep += 1;
        curStep = runstep;
        /*if (curStep > 1)
        {
            setObjectPosition(curStep);
        }*/
        stepComponents[curStep].SetActive(true);
        Debug.Log("runstep" + curStep);
        SetButtonVisibility(curStep);
        /* if (curStep == 1)
         {
             initiateActive();
         }*/
        WriteBoard_Step(FindChildBoard(board, "Title"));
        GetPlayAnimation(curStep);
        //    StepAnimControl.SetInteger("StepNo", curStep);
    }
    public void BackStep()
    {
        runstep -= 1;
        curStep = runstep;
        /*if (curStep > 0)
        {
            ResetObjectPositon(curStep);
            //       StepAnimControl.SetInteger("StepNo", curStep);
        }*/
        if (stepComponents[curStep + 1].GetComponent<Animator>() != null)
        {
            stepComponents[curStep + 1].GetComponent<Animator>().SetInteger("animStatus", 2);
            /*if (curStep > 0)
            {
                stepComponents[curStep].GetComponent<Animator>().SetBool("isAnimating", false);
            }*/
        }

     //   Invoke("DelayDestroy", 0.03f);
        //stepComponents[curStep + 1].SetActive(false);
        Debug.Log("runstepback" + curStep);

        //if (curStep == 0)
        // {
        //    BackToBegin();
        // }
        SetButtonVisibility(curStep);
        WriteBoard_Step(FindChildBoard(board, "Title"));
    }

  /*  private void DelayDestroy()
    {
        stepComponents[curStep + 1].SetActive(false);
    }*/

    private void GetPlayAnimation(int run)
    {

        /* if (run > 0 && stepComponents[run].GetComponent<Animator>() != null)
         {
             stepComponents[run].GetComponent<Animator>().SetBool("isAnimating", true);
             Debug.Log("Play Rotation");

         }*/
        if (run > 0 && stepComponents[run].GetComponent<Animator>() != null)
        {
            stepComponents[run].GetComponent<Animator>().SetInteger("animStatus", 1);
        }
       /* if(run>1 && stepComponents[run-1].GetComponent<Animator>() != null)
        {
            stepComponents[run-1].GetComponent<Animator>().SetBool("isAnimating", true);
        }*/
    }

    private void InitiateAnimator()
    {
        for (int i = 0; i < stepComponents.Length; i++)
        {
            if (stepComponents[i].GetComponent<Animator>() != null)
                stepComponents[i].GetComponent<Animator>().SetBool("isAnimating", false);
        }
    }

    void Update()
    {
        //RunTransformAnimation(curStep);
        // checkStopPlay(curStep);
    }



    public GameObject FindChildBoard(GameObject parent, string childName)
    {

        {
            if (parent.name == childName)
            {
                return parent;
            }
            if (parent.transform.childCount < 1)
            {
                return null;
            }
            GameObject obj = null;
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject go = parent.transform.GetChild(i).gameObject;
                obj = FindChildBoard(go, childName);
                if (obj != null)
                {
                    break;
                }
            }
            return obj;
        }
    }
    public void WriteBoard_Step(GameObject theboard)
    {
        Text information;
        information = theboard.GetComponent<Text>();
        if (curStep != 0)
        {
            information.text = "STEP   " + curStep + " / " + totalSteps;
        }
        if (curStep == 0)
        {
            information.text = "GENERAL VIEW  -  " + totalSteps + " STEPS";
        }
    }

    private void SetButtonVisibility(int run)
    {

        if (run == 0)
        {
            back.SetActive(false);
            next.SetActive(false);
            start.SetActive(true);

        }
        if (run < stepComponents.Length - 1 && run > 0)
        {
            back.SetActive(true);
            next.SetActive(true);
            start.SetActive(false);
        }
        if (run == stepComponents.Length - 1)
        {
            back.SetActive(true);
            next.SetActive(false);
            start.SetActive(false);
        }
        Debug.Log("Set Object Visibility");
    }
}
    /*private void setObjectPosition(int run)
   {

       stepComponents[run - 1].transform.position = CopystepComponents[run - 1].transform.position;
       Debug.Log("Set position for " + curStep);
   }*/
   /* private void BackToBegin()
    {
        for (int i = 0; i < stepComponents.Length; i++)
        {
            // stepComponents[i].transform.position = CopystepComponents[i].transform.position;
            stepComponents[i].SetActive(true);
        }
    }*/
    //  private void ResetObjectPositon(int run)
    //  {
    // stepComponents[run+1].transform.position += new Vector3(change.X, change.Y, change.Z);
    //  stepComponents[run + 1].transform.position = OriginalComponents[run + 1].transform.position;
    //  }

    /*  private void initiateActive()
      {

          for (int i = 0; i < stepComponents.Length; i++)
          {
              stepComponents[i].transform.position = OriginalComponents[i].transform.position;
          }
          stepComponents[0].SetActive(true);
          stepComponents[1].SetActive(true);

          for (int i = 2; i < stepComponents.Length; i++)
              stepComponents[i].SetActive(false);
          /* for (int i = 0; i < stepComponents.Length; i++)
           {
               print("Copy " + CopystepComponents [i].transform.position.y + "&&&& Comp" + stepComponents[i].transform.position.y);
           }*/
    //}

    // private void checkStopPlay(int run)
    // {
    /*if (stepComponents[run].GetComponent<Animator>() != null && stepComponents[run].GetComponent<Animator>() != false)
    {
        if (Math.Abs(stepComponents[run].transform.position.y - CopystepComponents[run].transform.position.y) < 0.01f &&
    Math.Abs(stepComponents[run].transform.position.x - CopystepComponents[run].transform.position.x) < 0.01f)
        {
            stepComponents[run].GetComponent<Animator>().SetBool("PlayStep", false);
            //   Debug.Log("TOO NEAR");
        }
    }*/
    // }
    // private void RunTransformAnimation(int run)
    // {

    // stepComponents[run].transform.position = Vector3.MoveTowards(stepComponents[run].transform.position, CopystepComponents[run].transform.position, speed * Time.deltaTime);
    // }





