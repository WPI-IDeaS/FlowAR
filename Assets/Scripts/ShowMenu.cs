using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowMenu : MonoBehaviour
{
    
    public GameObject[] gameObjects;
    public Transform[] startTrans;
    public Transform[] endTrans;
    public float speed;
    private bool isOn = true;
    private bool play;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            for(int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(true);
            }
            StartCoroutine(AnimAllItems(startTrans, endTrans));
        }
        else
        {
            StartCoroutine(AnimAllItems(endTrans, startTrans));
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(false);
            }
        }
    }

    IEnumerator AnimAllItems(Transform[] _start,Transform[] _end)
    {
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].transform.position = Vector3.MoveTowards(gameObjects[i].transform.position, _end[i].position, speed * Time.deltaTime);
        }
        yield return 0;
    }

    public void OnGeneral()
    {
        if (isOn)
        {
            play = true;
            isOn = false;
        }
        else
        {
            OnHide();
        }
    }

    public void OnHide()
    {
        play = false;
        isOn = true;
    }

    
}
