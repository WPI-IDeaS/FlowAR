
using Microsoft.MixedReality.Toolkit.Diagnostics;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject rotateObject;
    public Transform aroundTrans;
    public Transform originTrans;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject resetButton;
    // private Quaternion[] targetRotation = new Quaternion[6];
    private bool onAxis = true;
    private bool rotateStatus = false;
    private Vector3 originalPos;
    private float rotateAngle = 90f;
    private Vector3 targetRotation;
    public GameObject target;
    private float curStep;
    private bool isRotating = false;
    private Vector3 aroundAxis;
    // Start is called before the first frame update
    void Start()
    {
        //target = this.gameObject.transform;
        //targetRotation = rotateObject.transform.eulerAngles;
        if (leftButton != null)
        {
            leftButton.GetComponent<Interactable>().OnClick.AddListener(delegate { LeftRotate(); });
        }
        if(rightButton != null)
        {
            rightButton.GetComponent<Interactable>().OnClick.AddListener(delegate { RightRotate(); });
        }
        if (upButton != null)
        {
            upButton.GetComponent<Interactable>().OnClick.AddListener(delegate { UpRotate(); });
        }
        if (downButton != null)
        {
            downButton.GetComponent<Interactable>().OnClick.AddListener(delegate { DownRotate(); });
        }
        if (resetButton != null)
        {
            resetButton.GetComponent<Interactable>().OnClick.AddListener(delegate { ResetAll(); });
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateStatus&&!isRotating) 
        {
            Debug.Log("RRRRROTate");
            target.transform.Rotate(aroundAxis, rotateAngle, Space.World);
            StartCoroutine(RotateObj(aroundAxis,rotateAngle));
        }
        rotateStatus = false;
        /// StartCoroutine(Rotate());
        
    }

    IEnumerator RotateObj(Vector3 _axis,float _degree)
    {
        if (isRotating)
            yield return 0;
        isRotating = true;
       
        
        //float speed = _degree / rotateTime;
        for(float i=0; Mathf.Abs(i) < Mathf.Abs(_degree); i += Time.deltaTime * speed*10)
        {
            Debug.Log("i=" + i);
            this.gameObject.transform.rotation = Quaternion.SlerpUnclamped(this.gameObject.transform.rotation, target.transform.rotation, speed * Time.deltaTime);
            //this.gameObject.transform.Rotate(_axis, Time.deltaTime * speed,Space.World);
            yield return 0;
        }

        isRotating = false;
        //rotateStatus = false;
    }

    public float[] GetEuler()
    {
        float[] _angle = new float[3];
        _angle[0] = rotateObject.transform.eulerAngles.x;
        _angle[1] = rotateObject.transform.eulerAngles.y;
        _angle[2] = rotateObject.transform.eulerAngles.z;
        for(int i = 0; i < 3; i++)
        {
            if (_angle[i] > 180)
            {
                _angle[i] -= 360;
            }
        }
        return _angle;
    }
    
    public bool IsOnAxis(float[] _angle)
    {
        float[,] _axes = new float[4,2]{ {-0.5f,0.5f},{89.5f,90.5f},{179.5f,180.5f},{ -90.5f,-89.5f} };
        bool[] _isOn = { false,false,false};
        for(int i = 0; i < _angle.Length; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if(_angle[i] > _axes[j,0] && _angle[i] < _axes[j, 1])
                {
                    _isOn[i] = true;
                    Debug.Log(i+"OnAxis" + j);
                }
                
            }
            
        }
        return _isOn[0]&&_isOn[1]&&_isOn[2];
    }

    public void LeftRotate()
    {
        float[] angle = GetEuler();
        onAxis = IsOnAxis(angle);
        Debug.Log("LeftRotate");
        if (!onAxis)
        {
            ResetRotate();
        }
        else
        {
            aroundAxis = Vector3.up;
            rotateAngle = 90;
            targetRotation = this.gameObject.transform.eulerAngles;
            targetRotation += new Vector3(0,rotateAngle,0);
            if (targetRotation.y > 180)
            {
                targetRotation.y -= 360;
            }

        }            
        rotateStatus = true;
    }

    public void RightRotate()
    {
        float[] angle = GetEuler();
        onAxis = IsOnAxis(angle);
        Debug.Log("RightRotate");
        if (!onAxis)
        {
            ResetRotate();
        }
        else
        {
            aroundAxis = Vector3.up;
            rotateAngle = -90;
            targetRotation = this.gameObject.transform.eulerAngles;
            targetRotation -= new Vector3(0, rotateAngle, 0);
            if (targetRotation.y <= -180)
            {
                targetRotation.y += 360;
            }

        }
        rotateStatus = true;
    }

    public void UpRotate()
    {
        float[] angle = GetEuler();
        onAxis = IsOnAxis(angle);
        Debug.Log("UpRotate");
        if (!onAxis)
        {
            Debug.Log("uprotatenotonAxis");
            ResetRotate();
        }
        else
        {
            aroundAxis = Vector3.right;
            rotateAngle = 90;
            targetRotation = this.gameObject.transform.eulerAngles;
            float _z = this.gameObject.transform.eulerAngles.z;
            //Debug.Log("target0 is" + targetRotation.x);
            _z += 90f;
            if (_z > 180)
            {
                _z -= 360;
            }
            targetRotation = new Vector3(targetRotation.x, targetRotation.y, _z);
           // Debug.Log("target2 is" + targetRotation.x);
        }
        rotateStatus = true;
    }

    public void DownRotate()
    {
        float[] angle = GetEuler();
        onAxis = IsOnAxis(angle);
        Debug.Log("DownRotate");
        if (!onAxis)
        {
            Debug.Log("downrotatenotonAxis");
            ResetRotate();
        }
        else
        {
            aroundAxis = Vector3.right;
            rotateAngle = -90;
            targetRotation = this.gameObject.transform.eulerAngles;
            Debug.Log("target0 is" + targetRotation.x);
            targetRotation -= new Vector3(0, 0, rotateAngle);
            Debug.Log("target1 is" + targetRotation.x);
            if (targetRotation.z <- 180)
            {
                targetRotation.z += 360;
            }
            Debug.Log("target2 is" + targetRotation.z);
        }
        rotateStatus = true;
    }

    public void ResetRotate()
    {
        Debug.Log("ResetRotate");
        this.gameObject.transform.rotation = originTrans.rotation;
    }

    public void ResetAll()
    {
        this.gameObject.transform.localPosition = originalPos;
        this.gameObject.transform.rotation = aroundTrans.rotation;
    }
}
