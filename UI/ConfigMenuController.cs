using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject configMenuPanel;
    private bool isOpen;

    public void ClosePanel()
    {
        isOpen = false;
        configMenuPanel.SetActive(false);
    }

    public void Click()
    {
        if (!isOpen)
        {
            isOpen = true;
            configMenuPanel.SetActive(true);
        }
        else
        {
            isOpen = false;
            configMenuPanel.SetActive(false);
        }
    }

    private void OnDisable()
    {
        ClosePanel();
    }
}
