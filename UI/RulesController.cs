using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesController : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform[] rulesArr;
    public float speed;
    private float distanceConst = 800f;
    //private Vector3[] curPosition;
    public List<Vector3> curPosition = new List<Vector3>();
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 tempvec;
    private bool activeAnim;


    void Start()
    {

        for (int i = 0; i < rulesArr.Length; i++)
        {
            startPosition = rulesArr[i].localPosition;
            startPosition.x = i * distanceConst;
            rulesArr[i].localPosition = startPosition;
            curPosition.Add(rulesArr[i].localPosition);
        }

        endPosition.x = distanceConst * rulesArr.Length * (-1f);
        tempvec = rulesArr[0].localPosition;


    }

    public void MoveRight()
    {
        if (!activeAnim && rulesArr[0].localPosition.x > endPosition.x + distanceConst)
        {
            StartCoroutine(StartMoveRight());
        }
    }

    public void MoveLeft()
    {
        if (!activeAnim && rulesArr[rulesArr.Length - 1].localPosition.x < endPosition.x *(-1f) - distanceConst)
        {
            StartCoroutine(StartMoveLeft());
        }
    }

    private IEnumerator StartMoveRight()
    {
        activeAnim = true;

        tempvec = Vector3.zero;

        for (int i = 0; i < rulesArr.Length; i++)
        {
            curPosition[i] = rulesArr[i].localPosition;
        }


        while (tempvec.x > -800)
        {

            tempvec.x = Mathf.Lerp(tempvec.x, -802, speed);

            for (int i = 0; i < rulesArr.Length; i++)
            {
                rulesArr[i].localPosition = curPosition[i] + tempvec;
            }
            //Debug.Log(tempvec.x);
            yield return null;
        }

        activeAnim = false;
        //clickText.gameObject.SetActive(false);
        //yield return new WaitForSeconds(1f);
        //if (tempCoroutine == null)
        //{
        //    tempCoroutine = StartCoroutine(ContentFade());

        //}
    }

    private IEnumerator StartMoveLeft()
    {
        activeAnim = true;

        tempvec = Vector3.zero;

        for (int i = 0; i < rulesArr.Length; i++)
        {
            curPosition[i] = rulesArr[i].localPosition;
        }

        while (tempvec.x < 800)
        {

            tempvec.x = Mathf.Lerp(tempvec.x, 802, speed);

            for (int i = 0; i < rulesArr.Length; i++)
            {
                rulesArr[i].localPosition = curPosition[i] + tempvec;
            }
            yield return null;
        }

        activeAnim = false;
        //clickText.gameObject.SetActive(false);
        //yield return new WaitForSeconds(1f);
        //if (tempCoroutine == null)
        //{
        //    tempCoroutine = StartCoroutine(ContentFade());

        //}
    }
}
