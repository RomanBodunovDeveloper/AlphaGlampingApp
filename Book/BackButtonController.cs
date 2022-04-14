using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public void onClick()
    {
        ScrollViewAdapterv2.instance.BackButtonClick();
    }    
}
