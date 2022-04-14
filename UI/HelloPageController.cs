using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloPageController : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform page1;
    public RectTransform page2;
    public RectTransform page3;
    private Vector3 tempvec;
    public float speed;
    private bool activeAnim;

    private bool page1open;
    private bool page2open;
    private bool page3open;

    private Vector3 startPage1position;
    private Vector3 startPage2position;
    private Vector3 startPage3position;

    void Start()
    {
        page1open = true;
        page2open = false;
        page3open = false;

        startPage1position = page1.localPosition;
        startPage2position = page2.localPosition;
        startPage3position = page3.localPosition;

        page1.gameObject.SetActive(false);
        page2.gameObject.SetActive(false);
        page3.gameObject.SetActive(false);


    }


    public void ClickPage1()
    {
        if (!activeAnim)
        {
            if (page2open)
            {
                StartCoroutine(ContentMove(page2, false));
            }
            else if (page3open)
            {
                StartCoroutine(ContentMove(page3, false));
            }

            page1open = true;
            page2open = false;
            page3open = false;
        }
    }

    public void ClickPage2()
    {
        if (!activeAnim)
        {
            page1open = false;
            page2open = true;
            page3open = false;
            page2.gameObject.SetActive(true);
            StartCoroutine(ContentMove(page2, true));
        }
    }

    public void ClickPage3()
    {
        if (!activeAnim)
        {
            page1open = false;
            page2open = false;
            page3open = true;
            page3.gameObject.SetActive(true);
            StartCoroutine(ContentMove(page3, true));
        }
    }

    private IEnumerator ContentMove(RectTransform contentRect, bool open)
    {
        activeAnim = true;
        tempvec = contentRect.localPosition;
        
        if (open)
        {
            while (contentRect.localPosition.x > 0)
            {

                tempvec.x = Mathf.Lerp(tempvec.x, -1, speed * Time.deltaTime);

                //tempVec.y -= Time.deltaTime * speed;
                contentRect.localPosition = tempvec;
                yield return null;
            }
        }
        else
        {
            while (contentRect.localPosition.x < startPage2position.x)
            {

                tempvec.x = Mathf.Lerp(tempvec.x, startPage2position.x + 1, speed * Time.deltaTime);

                //tempVec.y -= Time.deltaTime * speed;
                contentRect.localPosition = tempvec;
                yield return null;
            }

            contentRect.gameObject.SetActive(false);
        }
        

       
        //clickText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        activeAnim = false;
    }


}
