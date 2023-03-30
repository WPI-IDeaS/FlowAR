using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using UnityEngine.XR.ARSubsystems;

public class ReadCSV 
{
    public string fileName;
    public List<string[]> Data;
    public ReadCSV(string name)
    {
        fileName = name;
        Data = new List<string[]>();
    }
    // Start is called before the first frame update

    public void Read()
    {
        Data = new List<string[]>();
        TextAsset data = Resources.Load<TextAsset>(fileName);
        string[] datarow = data.text.Split(new char[] { '\r' });
        for (int i = 0; i < datarow.Length; i++)
        {
                Data.Add(datarow[i].Split('\t'));
        }

    }
    public int GetLength()
    {
        return Data.Count;
    }

}
