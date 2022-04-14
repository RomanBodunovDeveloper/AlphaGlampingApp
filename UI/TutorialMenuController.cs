using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform pageMain;
    public RectTransform page1;
    public RectTransform page2;
    public RectTransform page3;
    private Vector3 tempvec;
    public float speed;
    private bool activeAnim;

    private bool pageMainOpen;
    private bool page1Open;
    private bool page2Open;
    private bool page3Open;

    private Vector3 startPageMainPosition;
    private Vector3 startPage1position;
    private Vector3 startPage2position;
    private Vector3 startPage3position;

    public GameObject pageMainMask;
    public GameObject page1Mask;
    public GameObject page2Mask;
    public GameObject page3Mask;

    private bool tutorialOpen;
    private bool firstStart;

    public RectTransform firstStartWindow;
    public RectTransform firstStartArrow;

    private Vector3 arrowTempPosition;
    private Vector3 arrowStartPosition;

    public Scrollbar scrollPage1;
    public Scrollbar scrollPage2;
    public Scrollbar scrollPage3;

    void Start()
    {
        pageMainOpen = false;
        page1Open = false;
        page2Open = false;
        page3Open = false;

        startPageMainPosition = pageMain.localPosition;
        startPage1position = page1.localPosition;
        startPage2position = page2.localPosition;
        startPage3position = page3.localPosition;

        pageMain.gameObject.SetActive(false);
        page1.gameObject.SetActive(false);
        page2.gameObject.SetActive(false);
        page3.gameObject.SetActive(false);

        pageMainMask.SetActive(false);
        page1Mask.SetActive(false);
        page2Mask.SetActive(false);
        page3Mask.SetActive(false);
        firstStart = true;


        firstStartWindow.gameObject.SetActive(false);
        firstStartArrow.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("FirstStartTutorial", 0) == 0)
        {
            firstStartWindow.gameObject.SetActive(true);
            firstStartArrow.gameObject.SetActive(true);
            //Debug.Log("1");
            arrowStartPosition = firstStartArrow.localPosition;
            StartCoroutine(StartArrowMove());
        }
        //else
        //{
        //    firstStartWindow.gameObject.SetActive(false);
        //   firstStartArrow.gameObject.SetActive(false);
        //}

        scrollPage1.value = 1;
        scrollPage2.value = 1;
        scrollPage3.value = 1;
    }


    public void ClickPage1()
    {
        if (!activeAnim)
        {
            //if (page2Open)
            //{
            //    StartCoroutine(ContentMove(page2, false));
            //}
            //else if (page3Open)
            //{
            //    StartCoroutine(ContentMove(page3, false));
            //}
            page1Mask.SetActive(true);
            page1.gameObject.SetActive(true);
            StartCoroutine(ContentMove(page1, page1Mask, true));

            pageMainOpen = false;
            page1Open = true;
            page2Open = false;
            page3Open = false;

            scrollPage1.value = 1;

            //Debug.Log("scrol =1");
        }
    }

    public void ClickPage2()
    {
        if (!activeAnim)
        {
            page2Mask.SetActive(true);
            page2.gameObject.SetActive(true);
            StartCoroutine(ContentMove(page2, page2Mask, true));

            pageMainOpen = false;
            page1Open = false;
            page2Open = true;
            page3Open = false;

            scrollPage2.value = 1;
        }
    }

    public void ClickPage3()
    {
        if (!activeAnim)
        {
            page3Mask.SetActive(true);
            page3.gameObject.SetActive(true);
            StartCoroutine(ContentMove(page3, page3Mask, true));

            pageMainOpen = false;
            page1Open = false;
            page2Open = false;
            page3Open = true;

            scrollPage3.value = 1;
        }
    }

    public void ClickPageMain()
    {
        if (!activeAnim)
        {
            if (page1Open)
            {
                StartCoroutine(ContentMove(page1, page1Mask, false));
            }
            else if (page2Open)
            {
                StartCoroutine(ContentMove(page2, page2Mask,  false));
            }
            else if (page3Open)
            {
                StartCoroutine(ContentMove(page3, page3Mask, false));
            }

            pageMainOpen = true;
            page1Open = false;
            page2Open = false;
            page3Open = false;

            
        }
    }

    private IEnumerator ContentMove(RectTransform contentRect, GameObject contentMask, bool open)
    {
        activeAnim = true;
        tempvec = contentRect.localPosition;

        if (open)
        {
            while (contentRect.localPosition.x > 0)
            {

                tempvec.x = Mathf.Lerp(tempvec.x, -2, speed * Time.deltaTime);

                //tempVec.y -= Time.deltaTime * speed;
                contentRect.localPosition = tempvec;
                yield return null;
            }
        }
        else
        {
            while (contentRect.localPosition.x < startPage2position.x)
            {

                tempvec.x = Mathf.Lerp(tempvec.x, startPage2position.x + 2, speed * Time.deltaTime);

                //tempVec.y -= Time.deltaTime * speed;
                contentRect.localPosition = tempvec;
                yield return null;
            }

            contentRect.gameObject.SetActive(false);
            contentMask.SetActive(false);
        }



        //clickText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        activeAnim = false;
    }

    public void OpenTutorial()
    {
        if (!tutorialOpen)
        {
            pageMainOpen = true;
            pageMainMask.SetActive(true);
            pageMain.gameObject.SetActive(true);

            tutorialOpen = true;
        }
        else
        {
            CloseTutorial();
        }
        

        
    }

    public void CloseTutorial()
    {
        if (!activeAnim)
        {
            if (pageMain != null) pageMain.gameObject.SetActive(false);
            if (page1 != null) page1.gameObject.SetActive(false);
            if (page2 != null) page2.gameObject.SetActive(false);
            if (page3 != null) page3.gameObject.SetActive(false);

            if (pageMainMask != null) pageMainMask.SetActive(false);
            if (page1Mask != null) page1Mask.SetActive(false);
            if (page2Mask != null) page2Mask.SetActive(false);
            if (page3Mask != null) page3Mask.SetActive(false);

            pageMainOpen = false;
            page1Open = false;
            page2Open = false;
            page3Open = false;
            tutorialOpen = false;

            if (page1 != null) page1.localPosition = startPage1position;
            if (page2 != null) page2.localPosition = startPage2position;
            if (page3 != null) page3.localPosition = startPage3position;
        }
    }

    private void OnEnable()
    {
        if (firstStart)
        {
            CloseTutorial();

            if (PlayerPrefs.GetInt("FirstStartTutorial", 0) == 0)
            {
                firstStartWindow.gameObject.SetActive(true);
                firstStartArrow.gameObject.SetActive(true);
                //arrowStartPosition = firstStartArrow.localPosition;
                StartCoroutine(StartArrowMove());
                //Debug.Log("2");
            }
            else
            {
                firstStartWindow.gameObject.SetActive(false);
                firstStartArrow.gameObject.SetActive(false);
            }
        }
        
    }

    private void OnDisable()
    {
        CloseTutorial();
        if (firstStartWindow != null) firstStartWindow.gameObject.SetActive(false);
        if (firstStartArrow != null) firstStartArrow.gameObject.SetActive(false);
    }

    public void firstStartWindowClose()
    {
        firstStartWindow.gameObject.SetActive(false);
        firstStartArrow.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("FirstStartTutorial", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstStartTutorial", 1);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator StartArrowMove()
    {
        arrowTempPosition = arrowStartPosition;
        while (PlayerPrefs.GetInt("FirstStartTutorial", 0) == 0)
        {
            while (firstStartArrow.localPosition.y > arrowStartPosition.y - 60f)
            {
                arrowTempPosition.y -= Time.deltaTime * 80f;
                arrowTempPosition.x -= Time.deltaTime * 80f;
                firstStartArrow.localPosition = new Vector3(arrowTempPosition.x, arrowTempPosition.y, firstStartArrow.localPosition.z);
                yield return null;
            }

            while (firstStartArrow.localPosition.y < arrowStartPosition.y)
            {
                arrowTempPosition.y += Time.deltaTime * 200f;
                arrowTempPosition.x += Time.deltaTime * 200f;
                firstStartArrow.localPosition = new Vector3(arrowTempPosition.x, arrowTempPosition.y, firstStartArrow.localPosition.z);
                yield return null;
            }

            yield return null;
        }

        
    }
}
