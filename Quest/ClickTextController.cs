using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTextController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text clickText;
    private RectTransform textRect;
    //private Color textColor;
    private Vector3 tempvec;
    private Color tempColor;
    public static ClickTextController instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        textRect = clickText.gameObject.GetComponent<RectTransform>();
        clickText.gameObject.SetActive(false);
        tempColor = clickText.color;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void SetText(string textValue)
    {
        StopCoroutine(FirstClick());
        StopCoroutine(TextMove());
        StopCoroutine(TextFade());
        textRect.localPosition = Vector3.zero;

        clickText.gameObject.SetActive(true);
        clickText.text = textValue + " обнаружено!";
        tempColor = clickText.color;
        tempColor.a = 1;
        clickText.color = tempColor;
        StartCoroutine(FirstClick());
    }

    private IEnumerator FirstClick()
    {
        StartCoroutine(TextMove());
        yield return new WaitForSeconds(1f);
        StartCoroutine(TextFade());
    }
    private IEnumerator TextMove()
    {
        tempvec = textRect.localPosition;
        while (tempvec.y < 100)
        {
            tempvec.y += Time.deltaTime * 40;
            textRect.localPosition = tempvec;
            yield return null;
        }
        tempvec = Vector3.zero;
        textRect.localPosition = tempvec;
        clickText.gameObject.SetActive(false);
    }

    private IEnumerator TextFade()
    {
        while (tempColor.a > 0)
        {
            tempColor.a -= Time.deltaTime;
            clickText.color = tempColor;
            yield return null;
        }
    }


}
