using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSTextController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text GPSText;
    public Image contentImage;
    public RectTransform contentRect;
    public RectTransform GPSTextRect;
    public static GPSTextController instance;
    private float tempSizeX;

    private Color tempColorText;
    private Color tempColorImage;
    //private Color textColor;
    //private Color imageColor;
    private bool startShowTextCor;
    private bool startHideTextCor;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        tempColorText = GPSText.color;
        tempColorImage = contentImage.color;


        tempSizeX = 0f;
        GPSText.gameObject.SetActive(false);
        contentImage.gameObject.SetActive(false);
        contentRect.sizeDelta = new Vector2(0, 100);
        GPSTextRect.sizeDelta = new Vector2(0, 100);

    }
    public void SetText(string value)
    {
        GPSText.text = value;
    }

    public void ShowText()
    {
        if (!startShowTextCor && !startHideTextCor)
        {
            StartCoroutine(StartShowText());
            //Debug.Log("Show text");
        }
    }

    public void HideText()
    {
        if (!startHideTextCor)
        {
            StartCoroutine(StartHideText());
            //Debug.Log("Hide text");

        }
    }

    private IEnumerator StartShowText()
    {
        startShowTextCor = true;

        GPSText.gameObject.SetActive(true);
        contentImage.gameObject.SetActive(true);
        while (contentRect.sizeDelta.x < 620)
        {
            tempSizeX += Time.deltaTime * 1000;
            contentRect.sizeDelta = new Vector2(tempSizeX, 100);
            GPSTextRect.sizeDelta = new Vector2(tempSizeX, 100);
            yield return null;

        }

        startShowTextCor = false;
    }

    private IEnumerator StartHideText()
    {
        startHideTextCor = true;

        while (tempColorText.a > 0)
        {
            tempColorText.a -= Time.deltaTime;
            tempColorImage.a -= Time.deltaTime;
            GPSText.color = tempColorText;
            contentImage.color = tempColorImage;
            yield return null;
        }

        tempSizeX = 0f;
        GPSText.gameObject.SetActive(false);
        contentImage.gameObject.SetActive(false);
        contentRect.sizeDelta = new Vector2(0, 100);
        GPSTextRect.sizeDelta = new Vector2(0, 100);
        tempColorText.a = 1;
        tempColorImage.a = 1;

        GPSText.color = tempColorText;
        contentImage.color = tempColorImage;

        Canvas.ForceUpdateCanvases();

        startHideTextCor = false;
        GPSController.instance.SetLocationServiceStartTry(false);
    }
}
