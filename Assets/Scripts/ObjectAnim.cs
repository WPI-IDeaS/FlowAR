using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectAnim : MonoBehaviour
{
    public GameObject[] goL;
    public GameObject[] goR;
    public Transform[] startTrans;
    public Transform[] endTrans;
    public GameObject invisibleObjects;
    public float minspeed = 0f;
    public float maxspeed = 0.6f;
    //public float magnifyTimes = 1.5f;
    public float speed = 0.5f;
   // public float[] step;
    private int isSplit = 0; //0=idle,1=split,2=combine
    private int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        step = (startTrans.Length + 1) / 2;
        /*for(int i = 0; i < go.Length; i++)
        {
            go[i].transform.position = startTrans[i].position;
        }*/
        
        //speed = minspeed;
        //isSplit = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSplit == 1)
        {
            invisibleObjects.SetActive(true);
            StartCoroutine(Split());
            isSplit = 0;
        }
        if(isSplit == 2)
        {
            StartCoroutine(Combine());
            
            isSplit = 0;
        }
       
    }

    IEnumerator Split()
    {
       
            for (int i = 0; i <goL.Length; i++)
            {
                Debug.Log("Split i:" + i);
                if (goL[i].GetComponent<LabelController>().isOn)
                {
                    goL[i].GetComponent<LabelController>().OnSelectObject();
                    Debug.Log("1 label off:" + i);
                }
                yield return StartCoroutine(MoveToPosition(goL[i], endTrans[i].position));
                if (goR.Length > 0 && i<goR.Length)
                {
                    if (goR[i].GetComponent<LabelController>().isOn)
                    {
                        goR[i].GetComponent<LabelController>().OnSelectObject();
                    Debug.Log("2 label off:" + i);
                    }
                    yield return StartCoroutine(MoveToPosition(goR[i], endTrans[i + step].position));
                }
           
            }
   
    }
    IEnumerator Combine()
    {

        for (int i = goL.Length - 1; i >= 0; i--)
        {
            Debug.Log("Combine i:" + i);
            if (goL[i].GetComponent<LabelController>().isOn)
            {
                goL[i].GetComponent<LabelController>().OnSelectObject();
                Debug.Log("3 label off:" + i);
            }
            yield return StartCoroutine(MoveToPosition(goL[i], startTrans[i].position));
            if (goR.Length > 0&&i<goR.Length)
            {
                if (goR[i].GetComponent<LabelController>().isOn)
                {
                    goR[i].GetComponent<LabelController>().OnSelectObject();
                    Debug.Log("4 label off:" + i);
                }
                yield return StartCoroutine(MoveToPosition(goR[i], startTrans[i + step].position));
            }
        }
        invisibleObjects.SetActive(false);
    }





    IEnumerator MoveToPosition(GameObject _g,Vector3 _target)
    {
        while(_g.transform.position!= _target)
        {
            _g.transform.position = Vector3.MoveTowards(_g.transform.position, _target, speed * Time.deltaTime);
            yield return 0;
        }

    }
    
    public void OnSplit()
    {
        //speed = minspeed;
        isSplit = 1;
    }

    public void OnCombine()
    {
        //speed = minspeed;
        isSplit = 2;
    }
}
