using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//vcam = bla;

public class CameraController : MonoBehaviour
{
    //public MapController mapController
    public static CameraController instance;

    public float speedPan;
    public float speedZoom;


    private Vector2 startPos;
    public Camera cam;
    private float targetPosX;
    private float targetPosY;
    private float screenKoef;

    public GameObject debugWindow;

    public Text speedPanText;
    public Text speedZoomText;


    private float xLeftBorder;
    private float xRightBorder;
    private float yDownBorder;
    private float yUpBorder;

    private bool needZoom;
    private bool freezeCamera;
    private bool cameraOnPosition;

    private bool cursorOnActiveIcon;
    private bool mouseDrag;
    private bool startDelayPass;
    private bool startDelayPass2;

    private float xLeftBorderMin;
    private float xRightBorderMin;
    private float yDownBorderMin;
    private float yUpBorderMin;

    private bool startZoom;


    private Vector2 preZoomTargetPos;
    private float preZoomOrthoSize;

    private bool needZoomPath;
    private bool needZoomPathUp;
    private bool needZoomPathDown;
    private float targetZoomPath;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        

        if (instance == null)
        {
            instance = this;
        }


        cam = GetComponent<Camera>();
        targetPosX = transform.position.x;
        targetPosY = transform.position.y;
        screenKoef = (float)Screen.width / (float)Screen.height;

        xLeftBorderMin = -11.5f + 2.5f * screenKoef;
        xRightBorderMin = 11.5f - 2.5f * screenKoef;
        yDownBorderMin = -20.4f + 2.5f;
        yUpBorderMin = 20.4f - 2.5f;
    }

    void Update()
    {
        xLeftBorder = -11.5f + cam.orthographicSize * screenKoef;
        xRightBorder = 11.5f - cam.orthographicSize * screenKoef;
        yDownBorder = -20.4f + cam.orthographicSize;
        yUpBorder = 20.4f - cam.orthographicSize;






        if (startDelayPass)
        {
            if (BlackScreenController.instance.GetMapOpen())
            {
                if (Input.touchCount == 0) startZoom = false;

                if (Input.GetMouseButtonDown(0) && !cursorOnActiveIcon && !startZoom && !IsPointerOverUIObject()) startPos = cam.ScreenToWorldPoint(Input.mousePosition);

                else if (Input.GetMouseButton(0) && !freezeCamera && !cursorOnActiveIcon && !startZoom && !IsPointerOverUIObject())
                {
                    float posX = cam.ScreenToWorldPoint(Input.mousePosition).x - startPos.x;
                    float posY = cam.ScreenToWorldPoint(Input.mousePosition).y - startPos.y;
                    targetPosX = Mathf.Clamp(transform.position.x - posX, xLeftBorder, xRightBorder);
                    targetPosY = Mathf.Clamp(transform.position.y - posY, yDownBorder, yUpBorder);
                    cameraOnPosition = false;

                }
                //else if (startZoom)
                //{
                //    targetPosX = Mathf.Clamp((cam.ScreenToWorldPoint(Input.GetTouch(0).position).x + cam.ScreenToWorldPoint(Input.GetTouch(1).position).x) / 2f, xLeftBorder, xRightBorder);
                //    targetPosY = Mathf.Clamp((cam.ScreenToWorldPoint(Input.GetTouch(0).position).y + cam.ScreenToWorldPoint(Input.GetTouch(1).position).y) / 2f, yDownBorder, yUpBorder);

                //}

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosX, speedPan * Time.deltaTime), Mathf.Lerp(transform.position.y, targetPosY, speedPan * Time.deltaTime), transform.position.z);
                // transform.position = new Vector3(Mathf.Lerp(transform.position.x, Mathf.Clamp(targetPosX, xLeftBorder, xRightBorder), speedPan * Time.deltaTime), Mathf.Lerp(transform.position.y, Mathf.Clamp(targetPosY, yDownBorder, yUpBorder), speedPan * Time.deltaTime), transform.position.z);
                //Debug.Log($"x = {targetPosX}, y = {targetPosY}");

                if (needZoom)
                {
                    zoom((cam.orthographicSize - 2.5f) * (0.1f));
                    if (cam.orthographicSize < 2.6f && ((Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(targetPosX)) < 0.1f) && (Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(targetPosY)) < 0.1f)))
                    {
                        needZoom = false;
                        freezeCamera = false;
                        cameraOnPosition = true;
                    }
                }
                else if (needZoomPath)
                {
                    if (cam.orthographicSize < targetZoomPath  && !needZoomPathDown && !needZoomPathUp)
                    {
                        needZoomPathUp = true;
                        
                    }
                    else if (cam.orthographicSize > targetZoomPath && !needZoomPathUp && !needZoomPathDown)
                    {
                        needZoomPathDown = true;
                       
                    }

                    if (needZoomPathUp)
                    {
                        zoom((cam.orthographicSize - targetZoomPath) * (0.1f));
                        if (cam.orthographicSize > (targetZoomPath -0.1f) && ((Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(targetPosX)) < 0.1f) && (Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(targetPosY)) < 0.1f)))
                        {
                            needZoomPath = false;
                            freezeCamera = false;
                            cameraOnPosition = true;
                        }
                    }
                    else if (needZoomPathDown)
                    {
                        zoom((cam.orthographicSize - targetZoomPath) * (0.1f));
                        if (cam.orthographicSize < (targetZoomPath + 0.1f) && ((Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(targetPosX)) < 0.1f) && (Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(targetPosY)) < 0.1f)))
                        {
                            needZoomPath = false;
                            freezeCamera = false;
                            cameraOnPosition = true;
                        }
                    }


                    if (!needZoomPath)
                    {
                        needZoomPathUp = false;
                        needZoomPathDown = false;
                    }

                    //Debug.Log(needZoomPath);
                }
                else
                {
                    if (Input.touchCount == 2 && !freezeCamera && !cursorOnActiveIcon)
                    {

                        Touch touchZero = Input.GetTouch(0);
                        Touch touchOne = Input.GetTouch(1);

                        Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
                        Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;


                        if (!startZoom)
                        {
                            //targetPosX = Mathf.Clamp(((cam.ScreenToWorldPoint(touchZeroLastPos).x + cam.ScreenToWorldPoint(touchOneLastPos).x) / 2), xLeftBorder, xRightBorder);
                            //targetPosY = Mathf.Clamp(((cam.ScreenToWorldPoint(touchZeroLastPos).y + cam.ScreenToWorldPoint(touchOneLastPos).y) / 2), yDownBorder, yUpBorder);
                            preZoomTargetPos.x = Mathf.Clamp(((cam.ScreenToWorldPoint(touchZeroLastPos).x + cam.ScreenToWorldPoint(touchOneLastPos).x) / 2), xLeftBorder, xRightBorder);
                            preZoomTargetPos.y = Mathf.Clamp(((cam.ScreenToWorldPoint(touchZeroLastPos).y + cam.ScreenToWorldPoint(touchOneLastPos).y) / 2), yDownBorder, yUpBorder);
                            //((cam.ScreenToWorldPoint(touchZeroLastPos).x + cam.ScreenToWorldPoint(touchOneLastPos).x) / 2)
                            preZoomOrthoSize = cam.orthographicSize;
                            //((cam.orthographicSize - 2.5f)/(15f-2.5f))* (((cam.ScreenToWorldPoint(touchZeroLastPos).x + cam.ScreenToWorldPoint(touchOneLastPos).x) / 2) - transform.position.x)+ transform.position.x
                            //((cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (((cam.ScreenToWorldPoint(touchZeroLastPos).y + cam.ScreenToWorldPoint(touchOneLastPos).y) / 2) - transform.position.y) + transform.position.y


                            //targetPosX = Mathf.Clamp(((cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (((cam.ScreenToWorldPoint(touchZeroLastPos).x + cam.ScreenToWorldPoint(touchOneLastPos).x) / 2) - transform.position.x) + transform.position.x, xLeftBorder, xRightBorder);
                            //targetPosY = Mathf.Clamp(((cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (((cam.ScreenToWorldPoint(touchZeroLastPos).y + cam.ScreenToWorldPoint(touchOneLastPos).y) / 2) - transform.position.y) + transform.position.y, yDownBorder, yUpBorder);
                        }



                        float distTouch = (touchZeroLastPos - touchOneLastPos).sqrMagnitude;
                        float currentDistTouch = (touchZero.position - touchOne.position).sqrMagnitude;

                        float difference = currentDistTouch - distTouch;

                        if (difference > 50 || difference < -50)
                        {
                            startZoom = true;
                        }

                        zoom(difference * speedZoom);



                        //offsetCamera = (cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position) - ((cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position) / (cam.orthographicSize / 1));




                    }

                    zoom(Input.GetAxis("Mouse ScrollWheel"));
                }

                if (Input.touchCount >= 1)
                {
                    mouseDrag = (Input.GetTouch(0).phase == TouchPhase.Moved) || Input.touchCount >= 2;
                    //.Log("DRAG");
                }
                else
                {
                    mouseDrag = false;
                }
            }
            else
            {
                return;
            }

        }
        else
            return;
    }

    void zoom(float increment)
    {
       // targetPosX = Mathf.Clamp((cam.ScreenToWorldPoint(Input.GetTouch(0).position).x + cam.ScreenToWorldPoint(Input.GetTouch(1).position).x) / 2f, xLeftBorder, xRightBorder);
       // targetPosY = Mathf.Clamp((cam.ScreenToWorldPoint(Input.GetTouch(0).position).y + cam.ScreenToWorldPoint(Input.GetTouch(1).position).y) / 2f, yDownBorder, yUpBorder);
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, 2.5f, 15f);
        if (startZoom)
        {
            targetPosX = Mathf.Clamp(((cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (transform.position.x - preZoomTargetPos.x) + preZoomTargetPos.x, xLeftBorder, xRightBorder);
            targetPosY = Mathf.Clamp(((cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (transform.position.y - preZoomTargetPos.y) + preZoomTargetPos.y, yDownBorder, yUpBorder);
        }
        if (transform.position.x < xLeftBorder) transform.position = new Vector3(xLeftBorder, transform.position.y, transform.position.z);
        if (transform.position.x > xRightBorder) transform.position = new Vector3(xRightBorder, transform.position.y, transform.position.z);
        if (transform.position.y < yDownBorder) transform.position = new Vector3(transform.position.x, yDownBorder, transform.position.z);
        if (transform.position.y > yUpBorder) transform.position = new Vector3(transform.position.x, yUpBorder, transform.position.z);
    }

    public void PressDebugBut()
    {
        debugWindow.SetActive(!debugWindow.activeSelf);
    }

    public void PressOkBut()
    {
        float.TryParse(speedPanText.text, out speedPan);
        float.TryParse(speedZoomText.text, out speedZoom);
    }

    public void SetTargetPosition(float targetX, float targetY)
    {
        targetPosX = Mathf.Clamp(targetX, xLeftBorderMin, xRightBorderMin); 
        targetPosY = Mathf.Clamp(targetY, yDownBorderMin, yUpBorderMin); 
        needZoom = true;
        cameraOnPosition = false;
    }

    public void SetFreezeCamera(bool val)
    {
        freezeCamera = val;
    }

    public bool GetFreezeCamera()
    {
        return freezeCamera;
    }

    public bool GetCameraOnPosition()
    {
        return cameraOnPosition;
    }

    public void SetCameraOnPosition(bool val)
    {
        cameraOnPosition = val;
    }

    public void SetCursorOnActiveIcon(bool val)
    {
        cursorOnActiveIcon = val;
    }

    public bool GetMouseDrag()
    {
        return mouseDrag;
    }

    public void SetStartDelayPass(bool val)
    {
        startDelayPass = val;
    }

    public bool GetStartDelayPass()
    {
        return startDelayPass;
    }

    public void SetStartDelayPass2(bool val)
    {
        startDelayPass2 = val;
    }

    public bool GetStartDelayPass2()
    {
        return startDelayPass2;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void SetTargetPositionPath(float targetX, float targetY, float targetZoom)
    {
        targetPosX = Mathf.Clamp(targetX, xLeftBorderMin, xRightBorderMin);
        targetPosY = Mathf.Clamp(targetY, yDownBorderMin, yUpBorderMin);
        targetZoomPath = targetZoom;
        needZoomPath = true;
        cameraOnPosition = false;
    }

}
