using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebSite : MonoBehaviour
{
    public string urlInput;
    public bool phoneCall;


    public void UrlOpen(string url)
    {
        //UNITY_WEBGL
        Application.OpenURL(url);
    }

    public void Click()
    {
        if (phoneCall)
        {
            //urlInput = urlInput;
            UrlOpen($"tel:{urlInput}");

        }
        else
        {
           // urlInput = urlInput;
            // Debug.Log(urlInput);
            UrlOpen(urlInput);
        }
        
    }


    //Application.OpenURL($"tel:{stringToEdit}") ;
}
