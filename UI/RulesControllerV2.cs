using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesControllerV2 : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform[] rulesArr;
    public float speed;

    private Vector3 centerPosition;
    private Vector3 leftPosition;
    private Vector3 rightPosition;
    private Vector3 tempvec;

    private bool activeAnim;
    private int currIndex;
    private int currIndexRight;
    private int currIndexLeft;


    void Start()
    {

        for (int i = 0; i < rulesArr.Length; i++)
        {
            rulesArr[i].gameObject.SetActive(false);
        }

        tempvec = rulesArr[0].localPosition;
        centerPosition = tempvec;

        tempvec = new Vector3(-800, tempvec.y, tempvec.z);
        leftPosition = tempvec;

        tempvec = new Vector3(800, tempvec.y, tempvec.z);
        rightPosition = tempvec;

        currIndex = 0;
        currIndexRight = 1;
        currIndexLeft = rulesArr.Length - 1;

        rulesArr[currIndex].gameObject.SetActive(true);
        rulesArr[currIndex].localPosition = centerPosition;

        rulesArr[currIndexRight].gameObject.SetActive(true);
        rulesArr[currIndexRight].localPosition = rightPosition;

        rulesArr[currIndexLeft].gameObject.SetActive(true);
        rulesArr[currIndexLeft].localPosition = leftPosition;
    }

    public void MoveRight()
    {
        if (!activeAnim)
        {
            StartCoroutine(StartMoveRight());
        }
    }

    public void MoveLeft()
    {
        if (!activeAnim)
        {
            StartCoroutine(StartMoveLeft());
        }
    }

    private IEnumerator StartMoveRight()
    {
        activeAnim = true;
        tempvec = Vector3.zero;

        while (tempvec.x > -800)
        {
            tempvec.x = Mathf.Lerp(tempvec.x, -802, speed);
            rulesArr[currIndex].localPosition = centerPosition + tempvec;
            rulesArr[currIndexRight].localPosition = rightPosition + tempvec;
            yield return null;
        }
        UpCurIndex();
        activeAnim = false;
    }

    private IEnumerator StartMoveLeft()
    {
        activeAnim = true;
        tempvec = Vector3.zero;

        while (tempvec.x < 800)
        {
            tempvec.x = Mathf.Lerp(tempvec.x, 802, speed);
            rulesArr[currIndex].localPosition = centerPosition + tempvec;
            rulesArr[currIndexLeft].localPosition = leftPosition + tempvec;
            yield return null;
        }
        DownCurIndex();
        activeAnim = false;
    }

    public void UpCurIndex()
    {

        rulesArr[currIndexLeft].gameObject.SetActive(false);

        if (currIndex + 1 == rulesArr.Length) currIndex = 0;
        else currIndex += 1;

        if (currIndex + 1 == rulesArr.Length) currIndexRight = 0;
        else currIndexRight = currIndex + 1;

        if (currIndex - 1 < 0) currIndexLeft = rulesArr.Length - 1;
        else currIndexLeft = currIndex - 1;

        rulesArr[currIndexRight].gameObject.SetActive(true);
        rulesArr[currIndexRight].localPosition = rightPosition;
    }

    public void DownCurIndex()
    {
        rulesArr[currIndexRight].gameObject.SetActive(false);

        if (currIndex - 1 < 0) currIndex = rulesArr.Length - 1;
        else currIndex -= 1;

        if (currIndex + 1 == rulesArr.Length) currIndexRight = 0;
        else currIndexRight = currIndex + 1;

        if (currIndex - 1 < 0) currIndexLeft = rulesArr.Length - 1;
        else currIndexLeft = currIndex - 1;

        rulesArr[currIndexLeft].gameObject.SetActive(true);
        rulesArr[currIndexLeft].localPosition = leftPosition;
    }
}
