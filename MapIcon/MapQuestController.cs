using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapQuestController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 imageQuestTempIconPosition;
    private Vector3 currPosition;
    private Vector3 startPosition;
    private int delayCount;
    private Coroutine tempCoroutine;
    private bool firstStart;
    public float shiftY;
    
    void Start()
    {
        currPosition = this.transform.localPosition;
        startPosition = this.transform.localPosition;
        tempCoroutine = StartCoroutine(StartQuestIconMove());
        firstStart = true;
        //questCollider.enabled = true;
    }

    // Update is called once per frame


    private IEnumerator StartQuestIconMove()
    {
        //Debug.Log("quest!");

        imageQuestTempIconPosition = Vector3.zero;

        while (true)
        {
            while (this.transform.localPosition.y < currPosition.y + 0.13f + 0.1f)
            {
                imageQuestTempIconPosition.y += Time.deltaTime * 0.8f;
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, currPosition.y + 0.13f + imageQuestTempIconPosition.y, this.transform.localPosition.z);
                yield return null;
            }

            while (this.transform.localPosition.y > currPosition.y)
            {
                imageQuestTempIconPosition.y -= Time.deltaTime * 1.2f;
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, currPosition.y + 0.13f + imageQuestTempIconPosition.y, this.transform.localPosition.z);
                yield return null;
            }

            while (this.transform.localPosition.y < currPosition.y + 0.13f + 0.1f)
            {
                imageQuestTempIconPosition.y += Time.deltaTime * 0.8f;
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, currPosition.y + 0.13f + imageQuestTempIconPosition.y, this.transform.localPosition.z);
                yield return null;
            }

            while (this.transform.localPosition.y > currPosition.y)
            {
                imageQuestTempIconPosition.y -= Time.deltaTime * 1.2f;
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, currPosition.y + 0.13f + imageQuestTempIconPosition.y, this.transform.localPosition.z);
                yield return null;
            }

            delayCount = 5;
            while (delayCount > 0)
            {
                yield return new WaitForSeconds(1);
                delayCount -= 1;
            }

            yield return null;
        }

        

    }

    private void OnEnable()
    {
        if (firstStart)
        {
            tempCoroutine = StartCoroutine(StartQuestIconMove());
        }
        
    }

    private void OnDisable()
    {
        tempCoroutine = null;
    }

    //public void UpdateCurPosition(bool up)
    //{
    //    currPosition = startPosition + new Vector3(0,shiftY,0);
    //    if (up)
    //    {
    //        this.transform.localPosition = this.transform.localPosition + new Vector3(0, shiftY, 0);
    //    }
    //    else
    //    {
    //        this.transform.localPosition = this.transform.localPosition - new Vector3(0, shiftY, 0);
    //    }
    //}
}
