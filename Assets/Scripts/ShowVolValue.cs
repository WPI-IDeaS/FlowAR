using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using TMPro;

public class ShowVolValue : MonoBehaviour
{
    private TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = this.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnVolumeUpdate(SliderEventData eventData)
    {
        textMesh.text = $"{10*eventData.NewValue:F1}";
    }
}
