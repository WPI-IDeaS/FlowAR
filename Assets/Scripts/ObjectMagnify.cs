using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMagnify : MonoBehaviour
{
    public Transform startTrans;
    public Transform endTrans;
    public float minspeed = 0f;
    public float maxspeed = 0.6f;
    public float magnifyTimes = 1.5f;
    private float speed;
    private bool isMag = false;
    //private bool isTrans = false;
    private Vector3 originalScale;


    // Start is called before the first frame update
    void Start()
    {
        
        originalScale = this.gameObject.transform.localScale;
        speed = minspeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMag)
        {
            this.gameObject.transform.localScale = new Vector3(magnifyTimes, magnifyTimes, magnifyTimes);
            
        }
    }

    public void OnMagnify()
    {
        isMag = true;
    }

}
