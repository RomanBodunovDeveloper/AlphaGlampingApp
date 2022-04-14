using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScrollViewAdapterv2 : MonoBehaviour
{
    public static ScrollViewAdapterv2 instance;

    [Serializable]
    public struct BookRecord
    {
        public int iconImageIndex;
        public string titleText;
        public int descriptionImageIndex;
        public string descriptionText;
        public int rarityImageIndex;
        public int[] region;
        public int category; // 1 - animal, 2 - plant, 3 - place
        public bool focus;
        public int focusIndex;
    }

    [Serializable]
    public class BookData
    {
        public List<BookRecord> Items = new List<BookRecord>();
    }

    // public GameObject blackScreen;
    // private Image blackScreenImage;
    public BookData bookData;

    public RectTransform prefab;
    public RectTransform prefabHairSpace;
    public RectTransform prefabTitleAnimal;
    public RectTransform prefabTitlePlant;
    public RectTransform prefabTitlePlace;
    public RectTransform prefabTitleVillage;
    public RectTransform prefabTitleAll;
    public RectTransform prefabEnd;
    public RectTransform prefabDownSpace;
    public Text countText;
    public RectTransform content;
    public Sprite[] imageIconArr;
    public Sprite[] imageDesciptionArr;
    public Sprite[] imageRarityArr;

    //public bool delayStart;
    public ScrollRect scroll;
    public BlackScreenController blackScreenController;
    // private List<GameObject> UIElementList;

    public GameObject controlPanel;
    public GameObject gridPanel;
    private int activeRecordCount;
    private RectTransform contentPanelRect;
    private bool needStopCor;

    //private bool tergaetIndexActivated;
    //public Image CLoseRecordButton;
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        // UIElementList = new List<GameObject>();
        //blackScreen.SetActive(true);
       // blackScreenController.SetBlack();
        var jsonTextFile = Resources.Load<TextAsset>("Text/BookRecords");
        bookData = new BookData();
        bookData = JsonUtility.FromJson<BookData>(jsonTextFile.text);
        UpdateItems();
        StartCoroutine(LoadingScene());

        contentPanelRect = GetComponent<RectTransform>();
        needStopCor = false;
        //CLoseRecordButton.gameObject.SetActive(false);
    }
    public void UpdateItems()
    {
        int modelsCount = 0;
        modelsCount = bookData.Items.Count;
        StartCoroutine(GetItems(modelsCount, results => OnReceivdeModels(results)));
    }

    void OnReceivdeModels(TestItemModel[] models)
    {
        // foreach (Transform child in content)
        // {
        //      Destroy(child.gameObject);
        //  }
        var instanceFreeSpace = GameObject.Instantiate(prefabHairSpace.gameObject) as GameObject;
        instanceFreeSpace.transform.SetParent(content, false);
        var instanceTitleAnimal = GameObject.Instantiate(prefabTitleAnimal.gameObject) as GameObject;
        instanceTitleAnimal.transform.SetParent(content, false);
        var instanceTitlePlant = GameObject.Instantiate(prefabTitlePlant.gameObject) as GameObject;
        instanceTitlePlant.transform.SetParent(content, false);
        var instanceTitlePlace = GameObject.Instantiate(prefabTitlePlace.gameObject) as GameObject;
        instanceTitlePlace.transform.SetParent(content, false);
        var instanceTitleVillage = GameObject.Instantiate(prefabTitleVillage.gameObject) as GameObject;
        instanceTitleVillage.transform.SetParent(content, false);
        var instanceTitleAll = GameObject.Instantiate(prefabTitleAll.gameObject) as GameObject;
        instanceTitleAll.transform.SetParent(content, false);
        


        foreach (var model in models)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            //UIElementList.Add(instance);
            instance.transform.SetParent(content, false);
            InitilizeItemView(instance, model);
        }

        //var instanceEnd = GameObject.Instantiate(prefabEnd.gameObject) as GameObject;
        //instanceEnd.transform.SetParent(content, false);

        var instanceDownSpace = GameObject.Instantiate(prefabDownSpace.gameObject) as GameObject;
        instanceDownSpace.transform.SetParent(content, false);
    }

    void InitilizeItemView(GameObject viewGameObject, TestItemModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        view.contentTextView.text = model.contentTextModel;
        view.descriptionTextView.text = model.descriptionTextModel;
        view.iconView.sprite = model.iconModel;
        view.descriptionImageView.sprite = model.descriptionImageModel;
        view.rarityImageView.sprite = model.rarityImageModel;
        view.categoryView.text = model.categoryModel.ToString();
        view.focusView.text = model.focusModel.ToString();
        view.focusIndexView.text = model.focusIndexModel.ToString();
    }

    IEnumerator GetItems(int count, System.Action<TestItemModel[]> callback)
    {
        yield return new WaitForSeconds(1f);
        var results = new TestItemModel[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = new TestItemModel();
            results[i].contentTextModel = bookData.Items[i].titleText;
            results[i].descriptionTextModel = bookData.Items[i].descriptionText;
            results[i].iconModel = imageIconArr[bookData.Items[i].iconImageIndex - 1];
            results[i].descriptionImageModel = imageDesciptionArr[bookData.Items[i].descriptionImageIndex - 1];
            results[i].rarityImageModel = imageRarityArr[bookData.Items[i].rarityImageIndex - 1];
            results[i].categoryModel = bookData.Items[i].category;
            results[i].focusModel = bookData.Items[i].focus;
            results[i].focusIndexModel = bookData.Items[i].focusIndex;

        }

        callback(results);
    }

    public class TestItemView
    {
        public Image iconView;
        public Text contentTextView;
        public Text descriptionTextView;
        public Image descriptionImageView;
        public Image rarityImageView;
        public Text categoryView;
        public Text focusView;
        public Text focusIndexView;

        public TestItemView(Transform rootView)
        {
            iconView = rootView.Find("Icon").GetComponent<Image>();
            contentTextView = rootView.Find("Content").GetComponent<Transform>().Find("ContentText").GetComponent<Text>();
            rarityImageView = rootView.Find("Content").GetComponent<Transform>().Find("RarityImage").GetComponent<Image>();
            descriptionTextView = rootView.Find("Description").GetComponent<Transform>().Find("DescriptionText").GetComponent<Text>();
            descriptionImageView = rootView.Find("Description").GetComponent<Transform>().Find("DescriptionImage").GetComponent<Image>();
            categoryView = rootView.Find("DataTransfer").GetComponent<Transform>().Find("Category").GetComponent<Text>();
            focusView = rootView.Find("DataTransfer").GetComponent<Transform>().Find("Focus").GetComponent<Text>();
            focusIndexView = rootView.Find("DataTransfer").GetComponent<Transform>().Find("FocusIndex").GetComponent<Text>();
        }
    }

    public class TestItemModel
    {
        public string contentTextModel;
        public string descriptionTextModel;
        public Sprite iconModel;
        public Sprite descriptionImageModel;
        public Sprite rarityImageModel;
        public int categoryModel;
        public bool focusModel;
        public int focusIndexModel;
    }

    

    private IEnumerator LoadingScene()
    {

        //blackScreenImage = blackScreen.GetComponent<Image>();
        yield return new WaitForSeconds(10);
        blackScreenController.PermamentMapOpen();
        blackScreenController.DownBlackScreen();
        CameraController.instance.SetStartDelayPass(true);
        //Debug.Log("delay 1");
       
    }





    void ShowInTabControl(float recordIndex)
    {
        //Debug.Log($"normalizePosition {normalizePosition},  transform.GetSiblingIndex() {transform.GetSiblingIndex()},  scroll.content.transform.childCount {scroll.content.transform.childCount},  scroll.verticalNormalizedPositio {scroll.verticalNormalizedPosition}  ");

        float normalizePosition = recordIndex / (float)scroll.content.transform.childCount;
        // Debug.Log($"normalizePosition {normalizePosition},  transform.GetSiblingIndex() {transform.GetSiblingIndex()},  scroll.content.transform.childCount {scroll.content.transform.childCount},  scroll.verticalNormalizedPositio {scroll.verticalNormalizedPosition}  ");

        scroll.verticalNormalizedPosition = 1 - normalizePosition;
        // Debug.Log($"normalizePosition {normalizePosition},  transform.GetSiblingIndex() {transform.GetSiblingIndex()},  scroll.content.transform.childCount {scroll.content.transform.childCount},  scroll.verticalNormalizedPositio {scroll.verticalNormalizedPosition}  ");
    }

    public void ClickOnInfoElement(int recordIndexTransfer)
    {
        var tempUIElement = this.transform.GetChild(recordIndexTransfer);
        tempUIElement.GetComponent<UIElement>().OnVirtualClick();
        //CLoseRecordButton.gameObject.SetActive(true);
    }


    public void ActivateAll()
    {
        for (int i = 0; i < scroll.content.transform.childCount - 1; i++)
        {
            var tempUIElement = this.transform.GetChild(i);

            if (i < 5)
            {
                tempUIElement.gameObject.SetActive(false);
            }
            else
            {
                tempUIElement.gameObject.SetActive(true);
            }
        }
        scroll.verticalNormalizedPosition = 1;
        //CLoseRecordButton.gameObject.SetActive(false);
    }

    public void ActivateAnimal()
    {
        for (int i = 0; i < scroll.content.transform.childCount - 1; i++)
        {
            var tempUIElement = this.transform.GetChild(i);

            if (i == 1 || (i > 5 && tempUIElement.GetComponent<UIElement>().GetCategory() == 2))
            {
                tempUIElement.gameObject.SetActive(true);
            }
            else
            {
                tempUIElement.gameObject.SetActive(false);
            }
        }
        scroll.verticalNormalizedPosition = 1;
        //CLoseRecordButton.gameObject.SetActive(false);
    }

    public void ActivatePlant()
    {
        for (int i = 0; i < scroll.content.transform.childCount - 1; i++)
        {
            var tempUIElement = this.transform.GetChild(i);

            if (i == 2 || (i > 5 && tempUIElement.GetComponent<UIElement>().GetCategory() == 3))
            {
                tempUIElement.gameObject.SetActive(true);
            }
            else
            {
                tempUIElement.gameObject.SetActive(false);
            }
        }
        scroll.verticalNormalizedPosition = 1;
        //CLoseRecordButton.gameObject.SetActive(false);
    }

    public void ActivatePlace()
    {
        for (int i = 0; i < scroll.content.transform.childCount - 1; i++)
        {
            var tempUIElement = this.transform.GetChild(i);

            if (i == 3 || (i > 5 && tempUIElement.GetComponent<UIElement>().GetCategory() == 4))
            {
                tempUIElement.gameObject.SetActive(true);
            }
            else
            {
                tempUIElement.gameObject.SetActive(false);
            }
        }
        scroll.verticalNormalizedPosition = 1;
        //CLoseRecordButton.gameObject.SetActive(false);
    }

    public void ActivateVillage()
    {
        for (int i = 0; i < scroll.content.transform.childCount - 1; i++)
        {
            var tempUIElement = this.transform.GetChild(i);

            if (i == 4 || (i > 5 && tempUIElement.GetComponent<UIElement>().GetCategory() == 5))
            {
                tempUIElement.gameObject.SetActive(true);
            }
            else
            {
                tempUIElement.gameObject.SetActive(false);
            }
        }
        scroll.verticalNormalizedPosition = 1;
        //CLoseRecordButton.gameObject.SetActive(false);
    }

    public void ActivateTargetIndex(int index)
    {

        for (int i = 0; i < scroll.content.transform.childCount - 1; i++)
        {
            var tempUIElement = this.transform.GetChild(i);

            if (i == 0 || i == index + 5)
            {
                tempUIElement.gameObject.SetActive(true);
                if (i == index + 5)
                {
                    tempUIElement.GetComponent<UIElement>().SetActivatedTargetIndex(true);
                }
            }
            else
            {
                tempUIElement.gameObject.SetActive(false);
            }
        }
        scroll.verticalNormalizedPosition = 1;
        //tergaetIndexActivated = true;
    }

    public void BackButtonClick()
    {
        gridPanel.SetActive(true);
        controlPanel.SetActive(false);
        //tergaetIndexActivated = false;
       // Debug.Log("Click!");
    }

    public void UIElementClickOpen(RectTransform target)
    {
        activeRecordCount = 0;
        for (int i = 0; i < scroll.content.transform.childCount - 1; i++)
        {

            var tempUIElement = this.transform.GetChild(i);
            if (tempUIElement.gameObject.activeSelf)
            {
                activeRecordCount += 1;
            }

            if (i > 5 && tempUIElement.GetComponent<UIElement>().GetIsOpen())
            {

                tempUIElement.GetComponent<UIElement>().PermamentClose();
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
        ////Vector2 tempVec = (Vector2)scroll.transform.InverseTransformPoint(contentPanelRect.position)
        ////    - (Vector2)scroll.transform.InverseTransformPoint(target.position) - new Vector2(0, 70);

        ////scroll.enabled = false;
        ////while (Math.Abs(contentPanelRect.anchoredPosition.y - tempVec.y) > 2f)
        ////{
        ////    if (needStopCor)
        ////    {
        ////        needStopCor = false;
        ////        yield break;
        ////    }

        ////    contentPanelRect.anchoredPosition = Vector2.Lerp(contentPanelRect.anchoredPosition, (Vector2)scroll.transform.InverseTransformPoint(contentPanelRect.position)
        ////    - (Vector2)scroll.transform.InverseTransformPoint(target.position) - new Vector2(0, 70), Time.deltaTime*10);
        ////    yield return null;


        ////}
        ////scroll.enabled = true;
        ///


        Vector2 tempVec = (Vector2)scroll.transform.InverseTransformPoint(contentPanelRect.position)
            - (Vector2)scroll.transform.InverseTransformPoint(target.position) - new Vector2(0, 70);

        if (tempVec.y > contentPanelRect.sizeDelta.y)
        {
            tempVec.y = contentPanelRect.sizeDelta.y - 70;
        }
        //else if (tergaetIndexActivated)
       // {
        //    tempVec.y = 70;
        //}

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


    IEnumerator StopCorAnim()
    {
        scroll.enabled = false;
        yield return new WaitForSeconds(0.5f);
        needStopCor = true;
        scroll.enabled = true;


    }


}

