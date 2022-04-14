using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenController : MonoBehaviour
{
    public GameObject blackScreenAll;
    public GameObject blackScreenPart;
    private Image blackScreenAllImage;
    private Image blackScreenPartImage;
    private Color tempColor;
    private Color tempColorText1;
    private Color tempColorText2;
    private Color tempColorText3;
    private Color tempColorLoadingImage;
    public float speedAnim;
    public float speedChangeScreen;
    private bool activeAnim;
    public bool chainAnim;
    public GameObject[] mapElementArr;
    public GameObject[] bookElementArr;
    public GameObject[] questElementArr;
    public GameObject[] helloPageElementArr;
    public GameObject[] contactPageElementArr;
    public ScrollViewAdapterv2 scrollViewAdapter;
    private bool needMapOpen;
    private bool needBookOpen;
    private bool needQuestOpen;
    private bool needHelloPageOpen;
    private bool needContactPageOpen;
    private bool mapOpen;
    private bool bookOpen;
    private bool questOpen;
    private bool helloPageOpen;
    private bool contactPageOpen;
    private bool needTransferIndex;
    private int transferIndex;
    public static BlackScreenController instance;
    public GameObject panelGrid;
    public Text text1;
    public Text text2;
    public Text text3;
    public Image loadingImage;
    //public MapIconController GlampingMapIcon;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


        blackScreenAllImage = blackScreenAll.GetComponent<Image>();
        blackScreenPartImage = blackScreenPart.GetComponent<Image>();
        SetBlack();
        mapOpen = true;
        bookOpen = false;
        questOpen = false;
        helloPageOpen = false;
        contactPageOpen = false;
    }

    //public void UpBlackScreen()
    //{
    //    if (!activeAnim)
    //    {
    //        StartCoroutine(StartUpBlackScreen());
    //    }
    //}

    public void DownBlackScreen()
    {
        if (!activeAnim)
        {
            StartCoroutine(StartDownBlackScreen());
            StartCoroutine(DownAllText());
        }
    }
    //private IEnumerator StartUpBlackScreen()
    //{
    //    blackScreen.SetActive(true);
    //    activeAnim = true;
    //    tempColor = blackScreenImage.color;
    //    tempColor.a = 0f;
    //    blackScreenImage.color = tempColor;

    //    while (blackScreenImage.color.a < 0.99)
    //    {
    //        tempColor = blackScreenImage.color;
    //        tempColor.a = Mathf.Lerp(tempColor.a, 2f, Time.deltaTime * speedAnim);
    //        blackScreenImage.color = tempColor;
    //        yield return null;
    //    }
    //    //if (chainAnim)
    //    //{
    //    //    StartCoroutine(StartDownBlackScreen());
    //    //}
    //    //else
    //    //{
    //       // activeAnim = false;
    //      //  blackScreen.SetActive(false);
    //    //}

    //    if (needMapOpen)
    //    {
    //        NeedMapOpen();
    //        needMapOpen = false;
    //    }

    //    if (needBookOpen)
    //    {
    //        NeedBookOpen();
    //        if (needTransferIndex)
    //        {
    //            ScrollViewAdapterv2.instance.ClickOnInfoElement(transferIndex);
    //            //scrollViewAdapter.ClickOnInfoElement(transferIndex);
    //            needTransferIndex = false;
    //            transferIndex = 0;
    //        }
    //        needBookOpen = false;
    //    }

    //    StartCoroutine(StartDownBlackScreen());

    //}

    private IEnumerator StartDownBlackScreen()
    {
       // blackScreen.SetActive(true);
        activeAnim = true;
        tempColor = blackScreenAllImage.color;
        tempColor.a = 1f;
        blackScreenAllImage.color = tempColor;

        while (blackScreenAllImage.color.a > 0.01)
        {
            tempColor = blackScreenAllImage.color;
            tempColor.a = Mathf.Lerp(tempColor.a, -1f, Time.deltaTime * speedAnim);
            blackScreenAllImage.color = tempColor;
            //text1.color = tempColor;
            //text2.color = tempColor;
            //text3.color = tempColor;
            yield return null;
        }
        activeAnim = false;
        blackScreenAll.SetActive(false);
    }

    public void SetNeedMapOpen()
    {
        if (!needBookOpen && !needQuestOpen && !needHelloPageOpen && !activeAnim && !mapOpen && !needContactPageOpen)
        {
            needMapOpen = true;
            StartCoroutine(ChangeScreen());
            mapOpen = true;
            bookOpen = false;
            questOpen = false;
            helloPageOpen = false;
            contactPageOpen = false;
        }
    }

    public void SetNeedBookOpen()
    {
        if (!needMapOpen && !needQuestOpen && !needHelloPageOpen && !activeAnim && !bookOpen && !needContactPageOpen)
        {
            needBookOpen = true;
            StartCoroutine(ChangeScreen());
            mapOpen = false;
            bookOpen = true;
            questOpen = false;
            helloPageOpen = false;
            contactPageOpen = false;
        }

        else if (bookOpen)
        {
            //PermamentBookOpen();
            ScrollViewAdapterv2.instance.BackButtonClick();
        }
    }

    public void SetNeedQuestOpen()
    {
        if (!needBookOpen && !needMapOpen && !needHelloPageOpen && !activeAnim && !questOpen && !needContactPageOpen)
        {
            needQuestOpen = true;
            StartCoroutine(ChangeScreen());
            mapOpen = false;
            bookOpen = false;
            questOpen = true;
            helloPageOpen = false;
            contactPageOpen = false;
        }
    }

    public void SetNeedHelloPageOpen()
    {
        if (!needBookOpen && !needMapOpen && !needQuestOpen && !activeAnim && !helloPageOpen && !needContactPageOpen)
        {
            needHelloPageOpen = true;
            StartCoroutine(ChangeScreen());
            mapOpen = false;
            bookOpen = false;
            questOpen = false;
            helloPageOpen = true;
            contactPageOpen = false;
        }
    }

    public void SetNeedContactPageOpen()
    {
        if (!needBookOpen && !needMapOpen && !needQuestOpen && !activeAnim && !needHelloPageOpen && !contactPageOpen)
        {
            needContactPageOpen = true;
            StartCoroutine(ChangeScreen());
            mapOpen = false;
            bookOpen = false;
            questOpen = false;
            helloPageOpen = false;
            contactPageOpen = true;
        }
    }


    public void SetBlack()
    {
        blackScreenAll.SetActive(true);
        loadingImage.gameObject.SetActive(true);
        tempColor = blackScreenAllImage.color;
        tempColor.a = 1f;
        blackScreenAllImage.color = tempColor;

        tempColor = text1.color;
        tempColor.a = 0f;
        text1.color = tempColor;
        text2.color = tempColor;
        text3.color = tempColor;

        StartCoroutine(DownLoadingImage());
        StartCoroutine(StartTextAll());
    }

    public void PermamentMapOpen()
    {
        for (int i = 0; i < bookElementArr.Length; i++)
        {
            bookElementArr[i].SetActive(false);
        }
        for (int i = 0; i < questElementArr.Length; i++)
        {
            questElementArr[i].SetActive(false);
        }
        for (int i = 0; i < helloPageElementArr.Length; i++)
        {
            helloPageElementArr[i].SetActive(false);
        }
        for (int i = 0; i < contactPageElementArr.Length; i++)
        {
            contactPageElementArr[i].SetActive(false);
        }
        for (int j = 0; j < mapElementArr.Length; j++)
        {
            mapElementArr[j].SetActive(true);
        }
        
    }

    public void PermamentBookOpen()
    {
        for (int i = 0; i < mapElementArr.Length; i++)
        {
            mapElementArr[i].SetActive(false);
        }
        for (int i = 0; i < questElementArr.Length; i++)
        {
            questElementArr[i].SetActive(false);
        }
        for (int i = 0; i < helloPageElementArr.Length; i++)
        {
            helloPageElementArr[i].SetActive(false);
        }
        for (int i = 0; i < contactPageElementArr.Length; i++)
        {
            contactPageElementArr[i].SetActive(false);
        }
        for (int j = 0; j < bookElementArr.Length; j++)
        {
            bookElementArr[j].SetActive(true);
        }
        
    }

    public void PermamentQuestOpen()
    {
        for (int i = 0; i < mapElementArr.Length; i++)
        {
            mapElementArr[i].SetActive(false);
        }
        for (int i = 0; i < helloPageElementArr.Length; i++)
        {
            helloPageElementArr[i].SetActive(false);
        }
        for (int j = 0; j < bookElementArr.Length; j++)
        {
            bookElementArr[j].SetActive(false);
        }
        for (int i = 0; i < contactPageElementArr.Length; i++)
        {
            contactPageElementArr[i].SetActive(false);
        }
        for (int i = 0; i < questElementArr.Length; i++)
        {
            questElementArr[i].SetActive(true);
        }
    }

    public void PermamentHelloPageOpen()
    {
        for (int i = 0; i < mapElementArr.Length; i++)
        {
            mapElementArr[i].SetActive(false);
        }   
        for (int j = 0; j < bookElementArr.Length; j++)
        {
            bookElementArr[j].SetActive(false);
        }
        for (int i = 0; i < questElementArr.Length; i++)
        {
            questElementArr[i].SetActive(false);
        }
        for (int i = 0; i < contactPageElementArr.Length; i++)
        {
            contactPageElementArr[i].SetActive(false);
        }
        //for (int i = 0; i < helloPageElementArr.Length; i++)
        //{
        //    helloPageElementArr[i].SetActive(true);
        //}
        helloPageElementArr[0].SetActive(true);
    }

    public void PermamentContactPageOpen()
    {
        for (int i = 0; i < mapElementArr.Length; i++)
        {
            mapElementArr[i].SetActive(false);
        }
        for (int i = 0; i < helloPageElementArr.Length; i++)
        {
            helloPageElementArr[i].SetActive(false);
        }
        for (int j = 0; j < bookElementArr.Length; j++)
        {
            bookElementArr[j].SetActive(false);
        }
        
        for (int i = 0; i < questElementArr.Length; i++)
        {
            questElementArr[i].SetActive(false);
        }
        for (int i = 0; i < contactPageElementArr.Length; i++)
        {
            contactPageElementArr[i].SetActive(true);
        }
    }

    public void TransferRecordIndex(int val)
    {
        needTransferIndex = true;
        transferIndex = val;
    }

    //public void PermamentBookOpen()
    //{
    //    if (needBookOpen)
    //    {
    //        NeedBookOpen();
    //        if (needTransferIndex)
    //        {
    //            ScrollViewAdapterv2.instance.ClickOnInfoElement(transferIndex);
    //            //scrollViewAdapter.ClickOnInfoElement(transferIndex);
    //            needTransferIndex = false;
    //            transferIndex = 0;
    //        }
    //        needBookOpen = false;
    //    }
    //}

    private IEnumerator ChangeScreen()
    {
        blackScreenPart.SetActive(true);
        activeAnim = true;
        tempColor = blackScreenPartImage.color;
        tempColor.a = 0f;
        blackScreenPartImage.color = tempColor;

        while (blackScreenPartImage.color.a < 0.99)
        {
            tempColor = blackScreenPartImage.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 2f, Time.deltaTime * speedChangeScreen);
            blackScreenPartImage.color = tempColor;
            yield return null;
        }

        if (needMapOpen)
        {
            PermamentMapOpen();
            needMapOpen = false;
            SoundController.instance.WorldSoundUp();
        }
        else
        {
            SoundController.instance.WorldSoundDown();
        }

        if (needBookOpen)
        {
            PermamentBookOpen();
            if (needTransferIndex)
            {
                ScrollViewAdapterv2.instance.ClickOnInfoElement(transferIndex);
                panelGrid.gameObject.SetActive(false);
                needTransferIndex = false;
                transferIndex = 0;

            }
            needBookOpen = false;
        }

        if (needQuestOpen)
        {
            PermamentQuestOpen();
            needQuestOpen = false;
        }

        if (needHelloPageOpen)
        {
            PermamentHelloPageOpen();
            needHelloPageOpen = false;
        }

        if (needContactPageOpen)
        {
            PermamentContactPageOpen();
            needContactPageOpen = false;
        }


        tempColor = blackScreenPartImage.color;
        tempColor.a = 1f;
        blackScreenPartImage.color = tempColor;

        while (blackScreenPartImage.color.a > 0.01)
        {
            tempColor = blackScreenPartImage.color;
            tempColor.a = Mathf.Lerp(tempColor.a, -1f, Time.deltaTime * speedChangeScreen);
            blackScreenPartImage.color = tempColor;
            yield return null;
        }
        activeAnim = false;
        blackScreenPart.SetActive(false);

    }

    private IEnumerator StartTextAll()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(StartText1());
        yield return new WaitForSeconds(2f);
        StartCoroutine(StartText2());
        yield return new WaitForSeconds(2f);
        StartCoroutine(StartText3());
    }

    private IEnumerator StartText1()
    {
        while (text1.color.a < 0.99)
        {
            tempColorText1 = text1.color;
            tempColorText1.a = Mathf.Lerp(tempColorText1.a, 1.5f, Time.deltaTime);
            text1.color = tempColorText1;
            yield return null;
        }
    }

    private IEnumerator StartText2()
    {
        while (text2.color.a < 0.99)
        {
            tempColorText2 = text2.color;
            tempColorText2.a = Mathf.Lerp(tempColorText2.a, 1.5f, Time.deltaTime);
            text2.color = tempColorText2;
            yield return null;
        }
    }

    private IEnumerator StartText3()
    {
        while (text3.color.a < 0.99)
        {
            tempColorText3 = text3.color;
            tempColorText3.a = Mathf.Lerp(tempColorText3.a, 1.5f, Time.deltaTime);
            text3.color = tempColorText3;
            yield return null;
        }
    }

    private IEnumerator DownAllText()
    {
        tempColorText1 = text1.color;
        tempColorText1.a = 1f;
        text1.color = tempColorText1;
        text2.color = tempColorText1;
        text3.color = tempColorText1;

        while (text1.color.a > 0.01)
        {
            tempColorText1 = text1.color;
            tempColorText1.a = Mathf.Lerp(tempColorText1.a, -1f, Time.deltaTime * speedAnim);
            text1.color = tempColorText1;
            text2.color = tempColorText1;
            text3.color = tempColorText1;
            yield return null;
        }

        CameraController.instance.SetTargetPosition(-2.694f , -1.832f);
        CameraController.instance.SetFreezeCamera(true);

        yield return new WaitForSeconds(2);
        CameraController.instance.SetStartDelayPass2(true);
        //Debug.Log("delay 2");

    }


    private IEnumerator DownLoadingImage()
    {
        tempColorLoadingImage = loadingImage.color;


        tempColorLoadingImage.a = 1f;
 

        while (loadingImage.color.a > 0.01)
        {
            tempColorLoadingImage.a = Mathf.Lerp(tempColorLoadingImage.a, -0.2f, Time.deltaTime);
            loadingImage.color = tempColorLoadingImage;

            yield return null;
        }

        loadingImage.gameObject.SetActive(false);
    }

    public bool GetMapOpen()
    {
        return mapOpen;
    }
}
