using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudController : MonoBehaviour
{
    // Start is called before the first frame update
    private Image cloud;
    public Camera cam;
    public float speedMove = 60f;
    private bool startMove;
    private float addCameraZoomShift;
    private float addCameraMoveShiftY;
    private float addCameraMoveShiftX;
    private float addMoveShift;
    private float startPositionX;
    private float startPositionY;
    private float randomShift;
    private float randomSpeed;
    public float cameraShiftSpeed = 1f;
    public bool directionRight;
   // private Color imageColor;
    private Color tempColor;
    void Start()
    {
        cloud = GetComponent<Image>();
        startPositionX = transform.position.x + Random.Range(-200.0f, 200.0f);
        startPositionY = transform.position.y;
        randomShift = Random.Range(0f, 10f);
        randomSpeed = Random.Range(1f, 3f);
        tempColor = cloud.color;
        tempColor.a = Random.Range(0.45f, 1f);
        //Debug.Log(tempColor.a);
        cloud.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (directionRight)
        {
            addCameraZoomShift = (float)Screen.width / 2 * ((cam.orthographicSize  - 2.5f) * (12.5f / 100f) ) + startPositionX;
            if (transform.position.x + cloud.rectTransform.sizeDelta.x > 0f) startMove = true;
            else startMove = false;

            if (startMove)
            {
                // Vector3.right* Mathf.sin(Time.time);
                addMoveShift += speedMove* randomSpeed * Mathf.Sin(Time.time + randomShift); ;
            }
        }
        else
        {
            addCameraZoomShift = -(float)Screen.width / 2 * (cam.orthographicSize - 2.5f) * (12.5f / 100f) + startPositionX;
            if (transform.position.x < Screen.width) startMove = true;
            else startMove = false;

            if (startMove)
            {
                // Vector3.right* Mathf.sin(Time.time);
                addMoveShift += -speedMove* randomSpeed * Mathf.Sin(Time.time + randomShift); ;
            }
        }

        addCameraMoveShiftY = (float)Screen.height / 2 * ((cam.transform.position.y) * (1f / 100f)) + startPositionY;
        addCameraMoveShiftX = ((float)Screen.width / 2) * ((cam.transform.position.x) * (1f / 100f));

        transform.position = new Vector3(addCameraZoomShift + addMoveShift + addCameraMoveShiftX, addCameraMoveShiftY, transform.position.z);

       // Debug.Log(startMove);
    }
}
