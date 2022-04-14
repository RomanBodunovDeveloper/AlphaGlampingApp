using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
[DisallowMultipleComponent]
public sealed class UIElementAnimalQuest : UIBehaviour
{
    public bool isOpen { get; private set; }
    private bool activeAnim;

    public RectTransform descriptionRect;
    private RectTransform elementRect;

    public Image QuestStatusImage;
    public Slider porgressSlider;
   // public Text DescriptionText;

    public float speed = 4f;

    private float minHeight;
    private float maxHeight;

    //public Text descriptionText;
    private bool delayPass;

    //private int elementCategory;
    //public Text categoryText;
    public RectTransform prefab;
    public RectTransform content;
    public RectTransform questContent;
    public Sprite questOpenImage;
    public Sprite questDoneImage;
    // public string[] questString;
    public string[] questPlayerPref;
    public bool bookQuest;
    public bool gpsQuest;
    public Text progressCountText;
    public string questText;
    public float maxCount;

    //private bool elementFocus;
    //public Text focusText;

    //private int elementFocusIndex;
   // public Text focusIndexText;

    //public GameObject showOnMap;
    private bool animDelayActive;

    protected override void Start()
    {
        base.Start();

        this.elementRect = GetComponent<RectTransform>();
        this.minHeight = this.elementRect.rect.height;
        porgressSlider.maxValue = maxCount;
        StartCoroutine(StartDelay());
        GenerateItems();
        animDelayActive = false;
    }

    public void OnClick()
    {

        if (delayPass)
        {

            if (this.isOpen && !activeAnim && !animDelayActive) StartCoroutine(CloseAnim());
            else if (!this.isOpen && !activeAnim)
            {
                StartCoroutine(OpenAnim());
                StartCoroutine(OpenAnimDelay());
                //ScrollViewAdapterv2.instance.UIElementClickOpen(elementRect);
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

    //public int GetCategory()
    //{
    //    return elementCategory;
    //}


    IEnumerator OpenAnim()
    {
        Vector2 tempVec = new Vector2();
        tempVec.x = elementRect.sizeDelta.x;
        tempVec.y = elementRect.sizeDelta.y;
        activeAnim = true;

        //tempVec.y = maxHeight;
        //elementRect.sizeDelta = tempVec;


        while (elementRect.rect.height < (maxHeight - 0.5f))
        {

            tempVec.y = Mathf.Lerp(tempVec.y, maxHeight, speed);

            //tempVec.y -= Time.deltaTime * speed;
            elementRect.sizeDelta = tempVec;
            yield return null;
        }

        activeAnim = false;
        ScrollViewAdapterQuest.instance.UIElementClickOpen(elementRect);
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
       // elementCategory = int.Parse(categoryText.text);
       // elementFocus = bool.Parse(focusText.text);
       // elementFocusIndex = int.Parse(focusIndexText.text);

        //showOnMap.SetActive(elementFocus);
        yield return new WaitForSeconds(4f);
        //Debug.Log("Coroutine end delay");


        maxHeight = descriptionRect.rect.height + 125f;
        Vector2 tempvec = new Vector2(descriptionRect.localPosition.x, descriptionRect.localPosition.y);
        tempvec.y = (descriptionRect.rect.height / 2f + 125) * (-1f);
        descriptionRect.localPosition = tempvec;
        delayPass = true;
        //Debug.Log(minHeight);
        //Debug.Log(maxHeight);
        //QuestUpdate();
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


    }

    protected override void OnDisable()
    {
        Vector2 tempVec = new Vector2();
        tempVec.x = elementRect.sizeDelta.x;
        tempVec.y = minHeight;
        elementRect.sizeDelta = tempVec;
        isOpen = false;
        animDelayActive = false;
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

    public void GenerateItems()
    {
        int modelsCount = 0;
        modelsCount = questPlayerPref.Length;
        StartCoroutine(GetItems(modelsCount, results => OnReceivdeModels(results)));
    }

    void OnReceivdeModels(TestItemModel[] models)
    {
        // foreach (Transform child in content)

        foreach (var model in models)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            //UIElementList.Add(instance);
            instance.transform.SetParent(questContent, false);
            InitilizeItemView(instance, model);
        }
    }

    IEnumerator GetItems(int count, System.Action<TestItemModel[]> callback)
    {
        yield return new WaitForSeconds(1f);
        var results = new TestItemModel[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = new TestItemModel();
            results[i].questTextModel = $"{questText} - {questPlayerPref[i]}";
                if (bookQuest)
                {
                    if (PlayerPrefs.GetInt($"{questPlayerPref[i]}_book", 0) == 0)
                    {
                        results[i].questImageModel = questOpenImage;
                    }
                    else
                    {
                        results[i].questImageModel = questDoneImage;
                    }
                }
                else if (gpsQuest)
                    {
                    if (PlayerPrefs.GetInt($"{questPlayerPref[i]}_gps", 0) == 0)
                    {
                        results[i].questImageModel = questOpenImage;
                    }
                    else
                    {
                        results[i].questImageModel = questDoneImage;
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt(questPlayerPref[i], 0) == 0)
                    {
                        results[i].questImageModel = questOpenImage;
                    }
                    else
                    {
                        results[i].questImageModel = questDoneImage;
                }
            }
            
        }

        callback(results);
    }

    void InitilizeItemView(GameObject viewGameObject, TestItemModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        view.questTextView.text = model.questTextModel;
        view.questImageView.sprite = model.questImageModel;
    }


    public class TestItemView
    {
        public Text questTextView;
      //  public Text playerPrefTextView;
        public Image questImageView;
  

        public TestItemView(Transform rootView)
        {
            questImageView = rootView.Find("Image").GetComponent<Image>();
            questTextView = rootView.Find("DescriptionText").GetComponent<Text>();
        }
    }
    public class TestItemModel
    {
        public string questTextModel;
       // public string playerPrefTextModel;
        public Sprite questImageModel;
    }

    public void UpdateItems()
    {
        float count = 0;
        for (int i = 0; i < questPlayerPref.Length; i++)
        {
            var tempUIElement = questContent.transform.GetChild(i);
            if (bookQuest)
            {
                tempUIElement.GetComponent<QuestAnimalImageCheck>().QuestImageCheck($"{questPlayerPref[i]}_book");
                count += tempUIElement.GetComponent<QuestAnimalImageCheck>().QuestDoneCheck($"{questPlayerPref[i]}_book");
            }
            else if (gpsQuest)
            {
                tempUIElement.GetComponent<QuestAnimalImageCheck>().QuestImageCheck($"{questPlayerPref[i]}_gps");
                count += tempUIElement.GetComponent<QuestAnimalImageCheck>().QuestDoneCheck($"{questPlayerPref[i]}_gps");
            }
            else
            {
                tempUIElement.GetComponent<QuestAnimalImageCheck>().QuestImageCheck(questPlayerPref[i]);
                count += tempUIElement.GetComponent<QuestAnimalImageCheck>().QuestDoneCheck(questPlayerPref[i]);
            }
            
        }
        
        porgressSlider.value = count;
        progressCountText.text = $"{count}/{maxCount}";
        if (count >= maxCount)
        {
            QuestStatusImage.sprite = questDoneImage;
        }
        
    }

}
