using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScrollViewAdapterQuest : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScrollViewAdapterQuest instance;
    public ScrollRect scroll;
    private bool needStopCor;
    private RectTransform contentPanelRect;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        contentPanelRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public void UIElementClickOpen(RectTransform target)
    {
        // activeRecordCount = 0;
        for (int i = 0; i < scroll.content.transform.childCount; i++)
        {

            var tempUIElement = this.transform.GetChild(i);


            if (((i == 2) || (i >= 4 && i <= 6) || (i >= 8 && i < 11)) && tempUIElement.GetComponent<UIElementAnimalQuest>().GetIsOpen())
            {

                tempUIElement.GetComponent<UIElementAnimalQuest>().PermamentClose();
                scroll.content.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
                scroll.content.GetComponent<ContentSizeFitter>().SetLayoutVertical();

            }
        }

        Canvas.ForceUpdateCanvases();

        needStopCor = false;
        scroll.enabled = true;

        StartCoroutine(MoveAnim(target));
    }

    IEnumerator MoveAnim(RectTransform target)
    {
        Vector2 tempVec = (Vector2)scroll.transform.InverseTransformPoint(contentPanelRect.position)
            - (Vector2)scroll.transform.InverseTransformPoint(target.position) - new Vector2(0, 70);

        if (tempVec.y > contentPanelRect.sizeDelta.y)
        {
            tempVec.y = contentPanelRect.sizeDelta.y - 70;
        }

        //Debug.Log(tempVec);

        scroll.enabled = false;
        while (Math.Abs(contentPanelRect.anchoredPosition.y - tempVec.y) > 2f)
        {

            

            if (needStopCor)
            {
                needStopCor = false;
                yield break;
            }

            //contentPanelRect.anchoredPosition = Vector2.Lerp(contentPanelRect.anchoredPosition, (Vector2)scroll.transform.InverseTransformPoint(contentPanelRect.position)
            //- (Vector2)scroll.transform.InverseTransformPoint(target.position) - new Vector2(0, 70), Time.deltaTime * 10);

            contentPanelRect.anchoredPosition = Vector2.Lerp(contentPanelRect.anchoredPosition, tempVec, Time.deltaTime * 10);

            yield return null;


        }
        scroll.enabled = true;
    }
}
