using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelController : MonoBehaviour
{
    public GameObject toolTipObject;
    public bool isOn = false;
    [SerializeField]
    [Range(0f, 10f)]
    public float lifeTime = 5f;
    private float curTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //toolTipObject = this.gameObject;
        curTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (isOn)
        {

            curTime += Time.deltaTime;
            if (curTime >= lifeTime)
            {
                OnSelectObject();
            }
        }


    }

    public void OnSelectObject()
    {
        if (isOn)
        {
            isOn = false;
            curTime = 0f;
            toolTipObject.SetActive(false);
        }
        else
        {
            isOn = true;
            toolTipObject.SetActive(true);
        }

    }

    public void OnSpeechSelectObject()
    {
        if (!isOn)
        {
            isOn = true;
            toolTipObject.SetActive(true);
        }

    }

    public void OnSpeechUnSelectObject()
    {
        if (isOn)
        {
            isOn = false;
            curTime = 0f;
            toolTipObject.SetActive(false);
        }

    }
}
