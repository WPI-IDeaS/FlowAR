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

public class SetSteps : MonoBehaviour
{
    // Start is called before the first frame update

    //public Animator StepAnimControl;
    public GameObject[] StepComponents;
    public GameObject[] CopyStepComponents;
    public GameObject[] OriginalComponents;

    //only to remember location

    /*public class OriginalPosition
    {
        [Tooltip("Changes that make components to Original Position")]
        public float X;
        public float Y;
        public float Z;
    }

    public OriginalPosition change;*/

    public GameObject next;
    public GameObject back;
    public GameObject board;
    [HideInInspector]
    public int RunStep;
    [HideInInspector]
    public int TotalSteps;
    private int runstep;
    public float Speed;



    void Awake()
    {

    }
    void Start()
    {
    //    StepAnimControl=this.GetComponent<Animator>();
        RunStep = 0;
        runstep = 0;
        TotalSteps = StepComponents.Length - 1;
        //StepComponents.Length indicate the number of all components
       // Debug.Log("Read step components length " + StepComponents.Length);
     //   Debug.Log("runstep " + RunStep);
        next.GetComponent<Interactable>().OnClick.AddListener(delegate { nextStep(); });
        back.GetComponent<Interactable>().OnClick.AddListener(delegate { backStep(); });
              
        setButtonVisibility(RunStep);
        WriteBoard_Step(FindChildBoard(board, "Title"));
        initiateAnimator();
    //    StepAnimControl.SetInteger("StepNo", RunStep);
    }

    private void initiateActive()
    {

        for (int i = 0; i < StepComponents.Length; i++)
        {
            StepComponents[i].transform.position = OriginalComponents[i].transform.position;
        }
        StepComponents[0].SetActive(true);
        StepComponents[1].SetActive(true);

        for (int i = 2; i < StepComponents.Length; i++)
            StepComponents[i].SetActive(false);
       /* for (int i = 0; i < StepComponents.Length; i++)
        {
            print("Copy " + CopyStepComponents [i].transform.position.y + "&&&& Comp" + StepComponents[i].transform.position.y);
        }*/
    }
    public void nextStep()
    {
        runstep += 1;
        RunStep = runstep;
        if (RunStep > 1)
        {
            setObjectPosition(RunStep);
        }
        StepComponents[RunStep].SetActive(true);
        Debug.Log("runstep" + RunStep);
        setButtonVisibility(RunStep);
        if (RunStep  == 1)
        {
            initiateActive();
        }
        WriteBoard_Step(FindChildBoard(board, "Title"));
        getPlayAnimation(RunStep);
        //    StepAnimControl.SetInteger("StepNo", RunStep);
    }
    public void backStep()
    {
        runstep -= 1;
        RunStep = runstep;
        if (RunStep  > 0 )
        {
            resetObjectPosition(RunStep);
     //       StepAnimControl.SetInteger("StepNo", RunStep);
        }
        StepComponents[RunStep+1].SetActive(false);
        Debug.Log("runstep" + RunStep);

        if (RunStep  == 0)
        {
            backToBegin();
        }
        setButtonVisibility(RunStep);
        WriteBoard_Step(FindChildBoard(board, "Title"));
    }

    private void getPlayAnimation(int run)
    {
    
            if (run>0 && StepComponents[run].GetComponent<Animator>() != null)
                {
                    StepComponents[run].GetComponent<Animator>().SetBool("PlayStep", true);
                    Debug.Log("Play Rotation");

                }
            if (run>1 && StepComponents[run-1].GetComponent<Animator>() != null)
            {
                StepComponents[run-1].GetComponent<Animator>().SetBool("PlayStep", false);
                Debug.Log("Stop Play Rotation");
            }



    }
    private void initiateAnimator()
    {
        for (int i = 0; i < StepComponents.Length; i++)
        {
            if (StepComponents[i].GetComponent<Animator>() != null)
                StepComponents[i].GetComponent<Animator>().SetBool("PlayStep", false);
        }
    }

    private void backToBegin()
    {
        for (int i = 0; i < StepComponents.Length; i++)
        {
            StepComponents[i].transform.position = CopyStepComponents[i].transform.position;
            StepComponents[i].SetActive(true);
        }
    }

    private void setObjectPosition(int run)
    {
        
        StepComponents[run-1].transform.position = CopyStepComponents[run-1].transform.position;
        Debug.Log("Set position for " + RunStep);
    }

    private void resetObjectPosition(int run)
    {
        // StepComponents[run+1].transform.position += new Vector3(change.X, change.Y, change.Z);
        StepComponents[run + 1].transform.position = OriginalComponents[run+1].transform.position;
    }

    public GameObject  FindChildBoard(GameObject parent,string childName)
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
        if (RunStep != 0)
        {
            information.text = "STEP   " + RunStep + " / " + TotalSteps;
        }
        if(RunStep ==0)
        {
            information.text = "GENERAL VIEW  -  " + TotalSteps + " STEPS";
        }
    } 
    void Update()
    {
        RunTransformAnimation(RunStep);
        checkStopPlay(RunStep);
    }
    private void checkStopPlay(int run)
    {
        if (StepComponents[run].GetComponent<Animator>() != null && StepComponents[run].GetComponent<Animator>() != false)
        {
            if (Math.Abs(StepComponents[run].transform.position.y - CopyStepComponents[run].transform.position.y) < 0.01f &&
        Math.Abs(StepComponents[run].transform.position.x - CopyStepComponents[run].transform.position.x) < 0.01f)
            {
                StepComponents[run].GetComponent<Animator>().SetBool("PlayStep", false);
             //   Debug.Log("TOO NEAR");
            }
        }
    }
    private void RunTransformAnimation(int run)
    {

            StepComponents[run].transform.position = Vector3.MoveTowards(StepComponents[run].transform.position, CopyStepComponents[run].transform.position,Speed*Time.deltaTime );
    }


    private void setButtonVisibility(int run)
    {

        if (run == 0)
        {
            back.SetActive(false);
            next.SetActive(true);

        }
        if (run < StepComponents.Length - 1 && run > 0)
        {
            back.SetActive(true);
            next.SetActive(true);

        }
        if (run == StepComponents.Length - 1)
        {
            back.SetActive(true);
            next.SetActive(false);
        }
        Debug.Log("Set Object Visibility");
    } 

}
