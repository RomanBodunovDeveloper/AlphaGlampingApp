using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class UIElement : UIBehaviour {

    public bool isOpen { get; private set; }
    private bool activeAnim;

    public RectTransform descriptionRect;
    private RectTransform elementRect;

    public Image DescriptionImage;
    public Text DescriptionText;

    public float speed = 4f;

    private float minHeight;
    private float maxHeight;

    public Text titleText;
    private bool delayPass;

    private int elementCategory;
    public Text categoryText;

    private bool elementFocus;
    public Text focusText;

    private int elementFocusIndex;
    public Text focusIndexText;

    public GameObject showOnMap;
    private bool animDelayActive;

    private bool activatedTargetIndex;

    protected override void Start()
    {
        base.Start();

        this.elementRect = GetComponent<RectTransform>();
        this.minHeight = this.elementRect.rect.height;

        StartCoroutine(StartDelay());
        animDelayActive = false;
    }

    public void OnClick()
    {

        if (delayPass && !activatedTargetIndex)
        {

            if (this.isOpen && !activeAnim && !animDelayActive) StartCoroutine(CloseAnim());
            else if (!this.isOpen && !activeAnim)
            {
                StartCoroutine(OpenAnim());
                StartCoroutine(OpenAnimDelay());
                //ScrollViewAdapterv2.instance.UIElementClickOpen(elementRect);
            }


            if (PlayerPrefs.GetInt($"{titleText.text}_book", 0) == 0 && elementCategory == 2)
            {
                PlayerPrefs.SetInt($"{titleText.text}_book", 1);
                PlayerPrefs.Save();
                // Debug.Log($"{playerPrefName} clicked");
               // ClickTextController.instance.SetText(playerPrefName);
                QuestTextController.instance.SetAnimalBookText();
            }

            if (PlayerPrefs.GetInt($"{titleText.text}_book", 0) == 0 && elementCategory == 3)
            {
                PlayerPrefs.SetInt($"{titleText.text}_book", 1);
                PlayerPrefs.Save();
                // Debug.Log($"{playerPrefName} clicked");
                // ClickTextController.instance.SetText(playerPrefName);
                QuestTextController.instance.SetPlantBookText();
            }

            if (PlayerPrefs.GetInt($"{titleText.text}_book", 0) == 0 && elementCategory >= 4)
            {
                PlayerPrefs.SetInt($"{titleText.text}_book", 1);
                PlayerPrefs.Save();
                // Debug.Log($"{playerPrefName} clicked");
                // ClickTextController.instance.SetText(playerPrefName);
                QuestTextController.instance.SetPlaceBookText();
            }





        }
    }

    public void OnVirtualClick()
    {
        //if (delayPass)
        //{

        StartCoroutine(VirtualClick());
        // }
        //Debug.Log("click pass");
    }

    public int GetCategory()
    {
        return elementCategory;
    }


    IEnumerator OpenAnim()
    {
        Vector2 tempVec = new Vector2();
        tempVec.x = elementRect.sizeDelta.x;
        tempVec.y = elementRect.sizeDelta.y;
        activeAnim = true;

        //while (elementRect.rect.height < (maxHeight - 0.5f))
        //{
        //    tempVec.y = Mathf.Lerp(tempVec.y, maxHeight, speed);
        //    //tempVec.y += Time.deltaTime * speed;
        //    elementRect.sizeDelta = tempVec;
        //    yield return null;
        //}

        //Vector2 tempVec = new Vector2();
        //tempVec.x = elementRect.sizeDelta.x;
        //tempVec.y = elementRect.sizeDelta.y;
        //activeAnim = true;

        tempVec.y = maxHeight;
        elementRect.sizeDelta = tempVec;


        //Debug.Log("Before kor");
        yield return null;
        //Debug.Log("After kor");
        
        activeAnim = false;
        ScrollViewAdapterv2.instance.UIElementClickOpen(elementRect);
        isOpen = true;

    }

    IEnumerator CloseAnim()
    {
        Vector2 tempVec = new Vector2();
        tempVec.x = elementRect.sizeDelta.x;
        tempVec.y = elementRect.sizeDelta.y;
        activeAnim = true;

        while (elementRect.rect.height > (minHeight + 0.5f))
        {

            tempVec.y = Mathf.Lerp(tempVec.y, minHeight, speed);

            //tempVec.y -= Time.deltaTime * speed;
            elementRect.sizeDelta = tempVec;
            yield return null;
        }
        isOpen = false;
        activeAnim = false;

    }

    IEnumerator StartDelay()
    {
        //Debug.Log("Coroutine start delay");
        elementCategory = int.Parse(categoryText.text);
        elementFocus = bool.Parse(focusText.text);
        elementFocusIndex = int.Parse(focusIndexText.text);

        showOnMap.SetActive(elementFocus);
        yield return new WaitForSeconds(2f);
        //Debug.Log("Coroutine end delay");


        maxHeight = descriptionRect.rect.height + 125f;
        Vector2 tempvec = new Vector2(descriptionRect.localPosition.x, descriptionRect.localPosition.y);
        tempvec.y = (descriptionRect.rect.height / 2f + 125) * (-1f);
        descriptionRect.localPosition = tempvec;
        delayPass = true;
    }

    IEnumerator OpenAnimDelay()
    {
        animDelayActive = true;
        yield return new WaitForSeconds(0.5f);
        animDelayActive = false;
    }

    IEnumerator VirtualClick()
    {
        //Debug.Log("Coroutine start delay");
        //yield return new WaitForSeconds(1.2f);
        yield return null;
        if (this.isOpen && !activeAnim)
        {
            Vector2 tempVec = new Vector2();
            tempVec.x = elementRect.sizeDelta.x;
            tempVec.y = minHeight;
            elementRect.sizeDelta = tempVec;
            isOpen = false;
        }
        else if (!this.isOpen && !activeAnim)
        {
            Vector2 tempVec = new Vector2();
            tempVec.x = elementRect.sizeDelta.x;
            tempVec.y = maxHeight;
            elementRect.sizeDelta = tempVec;
            isOpen = true;
        }
        //Debug.Log("Virtual click!");
        //Debug.Log("Coroutine end delay");

        if (PlayerPrefs.GetInt($"{titleText.text}_book", 0) == 0 && elementCategory == 2)
        {
            PlayerPrefs.SetInt($"{titleText.text}_book", 1);
            PlayerPrefs.Save();
            // Debug.Log($"{playerPrefName} clicked");
            // ClickTextController.instance.SetText(playerPrefName);
            QuestTextController.instance.SetAnimalBookText();
        }

        if (PlayerPrefs.GetInt($"{titleText.text}_book", 0) == 0 && elementCategory == 3)
        {
            PlayerPrefs.SetInt($"{titleText.text}_book", 1);
            PlayerPrefs.Save();
            // Debug.Log($"{playerPrefName} clicked");
            // ClickTextController.instance.SetText(playerPrefName);
            QuestTextController.instance.SetPlantBookText();
        }

        if (PlayerPrefs.GetInt($"{titleText.text}_book", 0) == 0 && elementCategory >= 4)
        {
            PlayerPrefs.SetInt($"{titleText.text}_book", 1);
            PlayerPrefs.Save();
            // Debug.Log($"{playerPrefName} clicked");
            // ClickTextController.instance.SetText(playerPrefName);
            QuestTextController.instance.SetPlaceBookText();
        }


    }

    protected override void OnDisable()
    {
        Vector2 tempVec = new Vector2();
        tempVec.x = elementRect.sizeDelta.x;
        tempVec.y = minHeight;
        elementRect.sizeDelta = tempVec;
        isOpen = false;
        animDelayActive = false;
        activatedTargetIndex = false;
    }

    public bool GetIsOpen()
    {
        return (isOpen || activeAnim);
    }

    public void PermamentClose()
    {
        StopCoroutine(OpenAnim());
        StopCoroutine(CloseAnim());
        Vector2 tempVec = new Vector2();
        tempVec.x = elementRect.sizeDelta.x;
        tempVec.y = minHeight;
        elementRect.sizeDelta = tempVec;
        isOpen = false;
        activeAnim = false;
        //StartCoroutine(VirtualClick());
    }

    public void SetActivatedTargetIndex(bool val)
    {
        activatedTargetIndex = val;
    }




}