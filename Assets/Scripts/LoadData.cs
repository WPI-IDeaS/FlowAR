using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public GameObject prefab;

    private Vector3[] v_original;
    private Vector3[] v_target;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset data = Resources.Load<TextAsset>("data");

        string[] d = data.text.Split(new char[] { '\n' });
        Debug.Log("d="+d.Length);
        v_original = new Vector3[d.Length - 2];
        v_target = new Vector3[d.Length - 2];

        for (int i = 1; i < d.Length - 1; i++)
        {
            string[] row = d[i].Split(new char[] { ',' });
            int _id = int.Parse(row[0])-1;
            v_original[_id] = new Vector3(float.Parse(row[1]), float.Parse(row[2]), float.Parse(row[3]));
            v_target[_id] = new Vector3(float.Parse(row[4]), float.Parse(row[5]), float.Parse(row[6]));
        }

        for(int i = 0; i < d.Length - 2; i++)
        {
            Instantiate(prefab, v_original[i], Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
