using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapIconController : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite idleSprite;
    public Sprite activeSprite;
    public GameObject banner;
    public Sprite questBanner;
    public Sprite exploredBanner;
    public Camera cam;
    private CameraController camController;
    private SpriteRenderer spriteRender;
    private bool activeIcon;
    private bool clickIcon;
    private Vector3 oldCamPosition;
    private bool fixCamPosition;
    public GameObject panel;
    private bool panelExist;
    public bool enableAutoScale;
    private bool banerExist;
    public bool enableFocusActive;
    public AudioSource soundAudioSource;
    private bool soundExist;
    private bool soundPlay;
    public float minSoundX;
    public float maxSoundX;
    public float minSoundY;
    public float maxSoundY;
    public float shiftTargetPositionX;
    public float shiftTargetPositionY;
    private bool delayMouseOverStart;
    private bool delayMouseOverPass;
    private float delayMouseOverCurr;
    public string playerPrefName;
    public bool animal;
    public bool place;
    public bool venchile;


    public string playerPrefGPS;
    private SpriteRenderer bannerImage;

    public MapQuestController mapQuestPoint;
    public float mapQuestPointShiftY;
    private bool mapQuestPointExist;

    public BoxCollider questCollider;
    public GameObject questObject;

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        camController = cam.GetComponent<CameraController>();
        if (banner != null)
        {
            banner.gameObject.SetActive(false);
            SetBannerPosition(true);
            banerExist = true;
            bannerImage = banner.GetComponent<SpriteRenderer>();

        }

        if (soundAudioSource != null)
        {
            soundExist = true;
        }

        if (panel != null)
        {
            panelExist = true;
            panel.gameObject.SetActive(false);
        }

        if (mapQuestPoint != null)
        {
            mapQuestPointExist = true;

            if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 0)
            {
                questCollider.enabled = true;
                questObject.SetActive(true);
                //Debug.Log("quest activate!");
            }
            else if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 1)
            {
                questCollider.enabled = false;
                questObject.SetActive(false);
            }
        }

        soundPlay = false;

    }

    private void Update()
    {



        if (enableAutoScale)
        {
            transform.localScale = new Vector3(Mathf.Clamp(cam.orthographicSize * 0.2f, 1f, 3f), Mathf.Clamp(cam.orthographicSize * 0.2f, 1f, 3f), transform.localScale.z);
        }
        else
        {
            if (panelExist)
            {
                panel.transform.localScale = new Vector3(Mathf.Clamp(cam.orthographicSize * 0.2f, 1f, 3f), Mathf.Clamp(cam.orthographicSize * 0.2f, 1f, 3f), panel.transform.localScale.z);
            }
            
        }

        if (banerExist)
        {
            if (banner.gameObject.activeSelf)
            {
                if (enableAutoScale)
                {
                    banner.transform.localScale = transform.localScale * 1.2f;
                }
                else
                {
                    banner.transform.localScale = new Vector3(Mathf.Clamp(cam.orthographicSize * 0.2f, 1f, 3f) * 1.2f, Mathf.Clamp(cam.orthographicSize * 0.2f, 1f, 3f) * 1.2f, banner.transform.localScale.z);

                }

                SetBannerPosition(true);

            }
        }


        if (clickIcon && CameraController.instance.GetCameraOnPosition() && !fixCamPosition)
        {
            fixCamPosition = true;
            if (soundExist)
            {
                if (!soundAudioSource.isPlaying)
                {
                    soundAudioSource.volume = 1;
                    soundAudioSource.Play();
                    soundPlay = true;
                    oldCamPosition = CameraController.instance.transform.position;
                }

            }

            if (playerPrefName != null && PlayerPrefs.GetInt(playerPrefName, 0) == 0 && animal)
            {
                PlayerPrefs.SetInt(playerPrefName, 1);
                PlayerPrefs.Save();
               // Debug.Log($"{playerPrefName} clicked");
                ClickTextController.instance.SetText(playerPrefName);
                QuestTextController.instance.SetAnimalText();
            }

            if (playerPrefName != null && PlayerPrefs.GetInt(playerPrefName, 0) == 0 && place)
            {
                PlayerPrefs.SetInt(playerPrefName, 1);
                PlayerPrefs.Save();
                // Debug.Log($"{playerPrefName} clicked");
                ClickTextController.instance.SetText(playerPrefName);
                QuestTextController.instance.SetPlaceText();
            }

            if (playerPrefName != null && PlayerPrefs.GetInt(playerPrefName, 0) == 0 && venchile)
            {
                PlayerPrefs.SetInt(playerPrefName, 1);
                PlayerPrefs.Save();
                // Debug.Log($"{playerPrefName} clicked");
                ClickTextController.instance.SetText(playerPrefName);
                QuestTextController.instance.SetVenchileText();
            }

        }
        if (clickIcon && !CameraController.instance.GetCameraOnPosition() && fixCamPosition)
        {
            clickIcon = false;
            fixCamPosition = false;
            if (banerExist) banner.gameObject.SetActive(false);
            if (panelExist)
            {
                panel.gameObject.SetActive(false);
            }

            spriteRender.sprite = idleSprite;
            activeIcon = false;
            if (banerExist)
            {
                if (!clickIcon)
                {
                    banner.gameObject.SetActive(false);
                }
            }


            //if (mapQuestPointExist)
            //{
            //    mapQuestPoint.shiftY = 0;
            //    mapQuestPoint.UpdateCurPosition(false);
            //}

            camController.SetCursorOnActiveIcon(false);
            delayMouseOverPass = false;
            delayMouseOverStart = false;
            delayMouseOverCurr = 0f;
        }

        if (soundExist)
        {
            if (!soundAudioSource.isPlaying)
            {
                soundPlay = false;
            }

            if (Mathf.Abs(CameraController.instance.transform.position.x - oldCamPosition.x) > Mathf.Abs(CameraController.instance.transform.position.y - oldCamPosition.y))
            {
                if (CameraController.instance.transform.position.x < transform.position.x)
                {
                    soundAudioSource.volume = 1 - Mathf.Clamp(((CameraController.instance.transform.position.x - transform.position.x + minSoundX) / (maxSoundX - minSoundX)) * (-1f), 0f, 1f);
                }
                else
                {
                    soundAudioSource.volume = 1 - Mathf.Clamp(((CameraController.instance.transform.position.x - transform.position.x - minSoundX) / (maxSoundX - minSoundX)) * (1f), 0f, 1f);
                }
            }
            else if (Mathf.Abs(CameraController.instance.transform.position.y - oldCamPosition.y) > Mathf.Abs(CameraController.instance.transform.position.x - oldCamPosition.x))
            {
                if (CameraController.instance.transform.position.y < transform.position.y)
                {
                    soundAudioSource.volume = 1 - Mathf.Clamp(((CameraController.instance.transform.position.y - transform.position.y + minSoundY) / (maxSoundY - minSoundY)) * (-1f), 0f, 1f);
                }
                else
                {
                    soundAudioSource.volume = 1 - Mathf.Clamp(((CameraController.instance.transform.position.y - transform.position.y - minSoundY) / (maxSoundY - minSoundY)) * (1f), 0f, 1f);
                }
            }

            soundAudioSource.volume = soundAudioSource.volume - soundAudioSource.volume * SoundController.instance.GethKoef();

        }
    }


    private void OnMouseOver()
    {
        if (!camController.GetMouseDrag() && (enableFocusActive || cam.orthographicSize <= 9f) && !IsPointerOverUIObject())
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
                spriteRender.sprite = activeSprite;

                if (banerExist)
                {
                    if (!banner.gameObject.activeSelf)
                    {
                        if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 0)
                        {
                            bannerImage.sprite = questBanner;
                        }
                        else if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 1)
                        {
                            bannerImage.sprite = exploredBanner;
                        }

                        banner.gameObject.SetActive(true);
                    }
                }

                //if (mapQuestPointExist)
                //{
                //    mapQuestPoint.shiftY = mapQuestPointShiftY;
                //    mapQuestPoint.UpdateCurPosition(true);
                //}

                camController.SetCursorOnActiveIcon(true);
            }
        }        
    }


    void OnMouseDown()
    {

        if (!camController.GetMouseDrag() && (enableFocusActive || cam.orthographicSize <= 9f) && !IsPointerOverUIObject())  //!EventSystem.current.IsPointerOverGameObject()
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
            spriteRender.sprite = idleSprite;
            if (banerExist)
            {
                if (!clickIcon)
                {
                    banner.gameObject.SetActive(false);
                }
            }

            //if (mapQuestPointExist)
            //{
            //    mapQuestPoint.shiftY = 0;
            //    mapQuestPoint.UpdateCurPosition(false);
            //}
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
                camController.SetTargetPosition(transform.position.x + shiftTargetPositionX, transform.position.y + shiftTargetPositionY);
                camController.SetFreezeCamera(true);

                clickIcon = true;
                if (panelExist)
                {
                    panel.gameObject.SetActive(true);
                }

                camController.SetCameraOnPosition(false);

                spriteRender.sprite = activeSprite;

                if (banerExist)
                {
                    if (!banner.gameObject.activeSelf)
                    {

                        if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 0)
                        {
                            bannerImage.sprite = questBanner;
                        }
                        else if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 1)
                        {
                            bannerImage.sprite = exploredBanner;
                        }

                        banner.gameObject.SetActive(true);
                    }
                }

                //if (mapQuestPointExist)
                //{
                //    mapQuestPoint.shiftY = mapQuestPointShiftY;
                //    mapQuestPoint.UpdateCurPosition(true);
                //}

                camController.SetCursorOnActiveIcon(true);




            }
            //delayMouseOverPass = false;
            //delayMouseOverStart = false;
            //delayMouseOverCurr = 0f;
        }
    }

    public void OnVirtualClick()
    {
        if (banerExist)
        {
            if (!banner.gameObject.activeSelf)
            {
                if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 0)
                {
                    bannerImage.sprite = questBanner;
                }
                else if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 1)
                {
                    bannerImage.sprite = exploredBanner;
                }

                banner.gameObject.SetActive(true);
            }
        }

        //if (mapQuestPointExist)
        //{
        //    mapQuestPoint.shiftY = mapQuestPointShiftY;
        //    mapQuestPoint.UpdateCurPosition(true);
        //}

        camController.SetTargetPosition(transform.position.x, transform.position.y);
        camController.SetFreezeCamera(true);

        clickIcon = true;
        if (panelExist)
        {
            panel.gameObject.SetActive(true);
        }
            
        camController.SetCameraOnPosition(false);

        delayMouseOverPass = false;
        delayMouseOverStart = false;
        delayMouseOverCurr = 0f;
    }


    public void SetBannerPosition(bool basePosition)
    {
        if (basePosition)
        {
            banner.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + (spriteRender.sprite.bounds.size.y / 2f + 0.2f)*transform.localScale.y), banner.transform.position.z);
        }
        
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
        if (banerExist)
        {
            if (banner.gameObject != null)
            banner.gameObject.SetActive(false);
        }
        if (panelExist)
        {
            panel.gameObject.SetActive(false);
        }

        spriteRender.sprite = idleSprite;
        activeIcon = false;
        if (banerExist)
        {
            if (!clickIcon)
            {
                if (banner.gameObject != null)
                    banner.gameObject.SetActive(false);
            }
        }

        //if (mapQuestPointExist)
        //{
        //    mapQuestPoint.shiftY = 0;
        //    mapQuestPoint.UpdateCurPosition(false);
        //}

        camController.SetCursorOnActiveIcon(false);
        delayMouseOverPass = false;
        delayMouseOverStart = false;
        delayMouseOverCurr = 0f;

        if (mapQuestPointExist == true)
        {
            questCollider.enabled = false;
            if (questObject != null)
            {
                questObject.SetActive(false);
            }
            
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void OnEnable()
    {
        if (mapQuestPointExist == true)
        {
            //mapQuestPointExist = true;

            if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 0)
            {
                questCollider.enabled = true;
                if (questObject != null)
                {
                    questObject.SetActive(true);
                }
                
                //Debug.Log("quest activate!");
            }
            else if (PlayerPrefs.GetInt($"{playerPrefGPS}_gps", 0) == 1)
            {
                questCollider.enabled = false;
                if (questObject != null)
                {
                    questObject.SetActive(false);
                }
                
            }
        }
    }

}
