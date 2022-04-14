using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookToMapTransfer : MonoBehaviour
{
    public MapIconController[] mapIconArr;
    public static BookToMapTransfer instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void FocusOnIcon(int iconIndex)
    {
        mapIconArr[iconIndex].OnVirtualClick();
    }
}
