using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject backPanel;
    private bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPress()
    {
        if (!isOn)
        {
            ShowPanel();
        }
        else
        {
            HidePanel();
        }
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        backPanel.SetActive(false);
        isOn = true;
    }

    public void HidePanel()
    {
        panel.SetActive(false);
        backPanel.SetActive(true);
        isOn = false;
    }
}
