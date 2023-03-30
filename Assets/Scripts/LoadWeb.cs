using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWeb : MonoBehaviour
{
    public string url;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoad(string _url)
    {
        Application.OpenURL(_url);
    }
}
