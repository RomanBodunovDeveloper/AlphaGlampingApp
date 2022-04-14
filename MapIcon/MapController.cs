using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool cursorOnMap;
    void OnMouseOver()
    {
        cursorOnMap = true;
        Debug.Log("Cursor on map");
    }

    private void OnMouseExit()
    {
        cursorOnMap = false;
        Debug.Log("Cursor off map");
    }

    public bool GetCursorOnMap()
    {
        return cursorOnMap;
    }
}
