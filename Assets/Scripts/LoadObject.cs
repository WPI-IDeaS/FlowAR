using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObject : MonoBehaviour
{
    public GameObject[] objectPrefab;
    //public int object_id = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadTheObject(ObjectIDBoard.object_id,ObjectIDBoard.scale);
    }

    public void LoadTheObject(int _id, float[] _scale)
    {
        objectPrefab[_id].SetActive(true);
        objectPrefab[_id].transform.localScale = new Vector3(_scale[0], _scale[1], _scale[2]);
    }
}
