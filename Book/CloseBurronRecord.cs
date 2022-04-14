using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBurronRecord : MonoBehaviour
{
    // Start is called before the first frame update
  public void Click()
    {
        BlackScreenController.instance.SetNeedMapOpen();
    }
}
