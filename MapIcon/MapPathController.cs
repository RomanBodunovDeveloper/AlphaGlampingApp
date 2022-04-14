using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapPathController : MonoBehaviour
{
    // Start is called before the first frame update
   // public Sprite idleSprite;
    //public Sprite activeSprite;
    //public GameObject banner;
   // public Sprite questBanner;
   // public Sprite exploredBanner;
    public Camera cam;
    private CameraController camController;
   // private SpriteRenderer spriteRender;
    private bool activeIcon;
    private bool clickIcon;
    //private Vector3 oldCamPosition;
    private bool fixCamPosition;
   // public GameObject panel;
   // private bool panelExist;
   //// public bool enableAutoScale;
   // private bool banerExist;
  //  public bool enableFocusActive;
   // public AudioSource soundAudioSource;
  //  private bool soundExist;
   // private bool soundPlay;
    //public float minSoundX;
    //public float maxSoundX;
    //public float minSoundY;
    //public float maxSoundY;
    public float shiftTargetPositionX;
    public float shiftTargetPositionY;
    private bool delayMouseOverStart;
    private bool delayMouseOverPass;
    private float delayMouseOverCurr;
    //public string playerPrefName;
    //public bool animal;
    //public bool place;
    //public bool venchile;
    public float targetZoom;

    //public string playerPrefGPS;
    //private SpriteRenderer bannerImage;

    //public MapQuestController mapQuestPoint;
    //public float mapQuestPointShiftY;
    //private bool mapQuestPointExist;

    //public BoxCollider questCollider;

    void Start()
    {
        camController = cam.GetComponent<CameraController>();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (clickIcon && CameraController.instance.GetCameraOnPosition() && !fixCamPosition)
        {
            fixCamPosition = true;


        }
        if (clickIcon && !CameraController.instance.GetCameraOnPosition() && fixCamPosition)
        {
            clickIcon = false;
            fixCamPosition = false;


            activeIcon = false;

            camController.SetCursorOnActiveIcon(false);
            delayMouseOverPass = false;
            delayMouseOverStart = false;
            delayMouseOverCurr = 0f;
        }

    }


    private void OnMouseOver()
    {
        if (!camController.GetMouseDrag() && !IsPointerOverUIObject())
        {

            activeIcon = true;





            //if (Input.touchCount >= 1)  delayMouseOverStart = true;
            delayMouseOverStart = Input.touchCount >= 1;

            if (delayMouseOverStart)
            {
                delayMouseOverCurr += Time.deltaTime;
                if (delayMouseOverCurr >= 1f)
                {
                    delayMouseOverStart = false;
                    delayMouseOverPass = true;
                }
            }
            else
            {
                delayMouseOverPass = false;
                delayMouseOverStart = false;
                delayMouseOverCurr = 0f;
            }

            if (delayMouseOverPass)
            {

                camController.SetCursorOnActiveIcon(true);
            }
        }
    }


    void OnMouseDown()
    {

        if (!camController.GetMouseDrag() && !IsPointerOverUIObject())  //!EventSystem.current.IsPointerOverGameObject()
        {
            activeIcon = true;
        }
    }


    private void OnMouseExit()
    {
        activeIcon = false;
        camController.SetCursorOnActiveIcon(false);

        //delayMouseOverStart = false;

        if (delayMouseOverPass)
        {
            delayMouseOverPass = false;
            delayMouseOverStart = false;
            delayMouseOverCurr = 0f;
        }
    }

    private void OnMouseUp()
    {
        delayMouseOverPass = false;
        delayMouseOverStart = false;
        delayMouseOverCurr = 0f;

        if (!IsPointerOverUIObject())
        {
            if (activeIcon) //&& !camController.GetCameraOnPosition()
            {
                camController.SetTargetPositionPath(transform.position.x + shiftTargetPositionX, transform.position.y + shiftTargetPositionY, targetZoom);
                camController.SetFreezeCamera(true);

                clickIcon = true;


                camController.SetCameraOnPosition(false);

                camController.SetCursorOnActiveIcon(true);

            }
     
        }
    }

    public void OnVirtualClick()
    {
       // delayMouseOverPass = false;
       // delayMouseOverStart = false;
       // delayMouseOverCurr = 0f;

        //if (!IsPointerOverUIObject())
       // {
         //   if (activeIcon) //&& !camController.GetCameraOnPosition()
         //   {
                camController.SetTargetPositionPath(transform.position.x + shiftTargetPositionX, transform.position.y + shiftTargetPositionY, targetZoom);
                camController.SetFreezeCamera(true);

                clickIcon = true;


                camController.SetCameraOnPosition(false);

                camController.SetCursorOnActiveIcon(true);

        //Debug.Log("Virtual click");

            //}

        //}
    }

    public bool GetActiveIcon()
    {
        return activeIcon;
    }

    private IEnumerator DelayMouseOver()
    {

        yield return new WaitForSeconds(0.5f);
        delayMouseOverPass = true;

    }

    private void OnDisable()
    {
        clickIcon = false;
        fixCamPosition = false;

        activeIcon = false;

        camController.SetCursorOnActiveIcon(false);
        delayMouseOverPass = false;
        delayMouseOverStart = false;
        delayMouseOverCurr = 0f;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
