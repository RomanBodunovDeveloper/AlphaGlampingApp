using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementMapButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Text focusIndexText;
    private int focusIndex;
   public void OnClick()
    {
        focusIndex = int.Parse(focusIndexText.text);
        BookToMapTransfer.instance.FocusOnIcon(focusIndex);
        BlackScreenController.instance.SetNeedMapOpen();
        FilterMenuController.instance.SetPlaceShow(true);
    }
}
