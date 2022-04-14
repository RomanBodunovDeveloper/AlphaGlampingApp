using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSController : MonoBehaviour
{
    public static GPSController instance { set; get; }

    public float latitude;
    public float longitude;
    private bool firstStart;

    public float radiusLatitude;
    public float radiusLongitude;

    public tempCoord[] pointGPS;

    public GameObject player;
    public float xRadius;
    public float yRadius;

    private float shiftLatitudeX;
    private float shiftLongitudeX;
    private float shiftLatitudeY;
    private float shiftLongitudeY;

    public GameObject imageRadius1;
    public GameObject imageRadius2;
    public GameObject imagePoint1;
    public GameObject imagePoint2;

    private SpriteRenderer imageRadius1SpriteRenderer;
    private SpriteRenderer imageRadius2SpriteRenderer;
    private SpriteRenderer imagePoint1SpriteRenderer;
    private SpriteRenderer imagePoint2SpriteRenderer;

    private Vector3 currPosition;
    private Vector3 imageRadius1TempScale;
    private Vector3 imageRadius2TempScale;
    private Vector3 imagePointTempScale;
    private Vector3 imagePointTempPosition;
    // private Vector3 imagePoint2TempScale;
    private float tempColorA;
    private Color tempColor;
    private bool GPSPointUpdate;

    private bool locationServiceStart;
    private bool locationServiceStartTry;

    private float curDelayStopLocation;
    private bool pointFind;
    public GameObject[] questIconArr;
    public BoxCollider[] questColliderArr;

    public bool debug1;
    public bool debug2;
    public bool debug3;

    [Serializable]
    public struct PlaceRecordGPS
    {
        public string playerPrefPlace;
        public float latitudePlace;
        public float longitudePlace;
    }

    [Serializable]
    public class GPSData
    {
        public List<PlaceRecordGPS> Items = new List<PlaceRecordGPS>();
    }

    public GPSData gpsData;

    //public 
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        imageRadius1.transform.localScale = Vector3.zero;
        imageRadius2.transform.localScale = Vector3.zero;
        imagePoint1.transform.localScale = Vector3.zero;
        imagePoint2.transform.localScale = Vector3.zero;
        imageRadius1.SetActive(false);
        imageRadius2.SetActive(false);
        imagePoint1.SetActive(false);
        imagePoint2.SetActive(false);
        imageRadius1TempScale = Vector3.zero;
        imageRadius2TempScale = Vector3.zero;
        imagePointTempScale = Vector3.zero;

        imageRadius1SpriteRenderer = imageRadius1.GetComponent<SpriteRenderer>();
        imageRadius2SpriteRenderer = imageRadius2.GetComponent<SpriteRenderer>();
        imagePoint1SpriteRenderer = imagePoint1.GetComponent<SpriteRenderer>();
        imagePoint2SpriteRenderer = imagePoint2.GetComponent<SpriteRenderer>();

        //DontDestroyOnLoad(gameObject);
        //StartCoroutine(StartLocationService());

        var jsonTextFile = Resources.Load<TextAsset>("Text/GPSRecords");
        gpsData = new GPSData();
        gpsData = JsonUtility.FromJson<GPSData>(jsonTextFile.text);

        //0 0 = -53.676680 -48.836117
        // -1.94 0 = -53.676680 - 48.842554
        //     0 2.32 = -53.673680 - 48.836117


        //   x  1.94 = 0.006437
        //   x  1 = 0.003318
        //     x = (longitude - 48.836117)* 0.003318

        // y 2.32 = -0.003
        //     y = -0.001293
        //     y = (latitude - 53.676680)*(-0.001293)

        for (int i = 0; i < gpsData.Items.Count; i++)
        {
            if (PlayerPrefs.GetInt($"{gpsData.Items[i].playerPrefPlace}_gps", 0) == 1)
            {
                questIconArr[i].SetActive(false);
                questColliderArr[i].enabled = false;
            }
        }
        

    }

    //private void Update()
    //{
    //    Debug.Log($"{curDelayStopLocation}, {Input.location.status}");
    //}

    public void ClickUpdate()
    {
        //if (BlackScreenController.instance.GetMapOpen())
       // {
            if (!locationServiceStart && !locationServiceStartTry)
            {
                StartCoroutine(StartLocationService());
                curDelayStopLocation = 30f;
                //Debug.Log("1");
            }
            else if (locationServiceStart)
            {
                if (!GPSPointUpdate)
                {
                    StartCoroutine(StartUpdateGEO());
                    curDelayStopLocation = 30f;
                    //Debug.Log("2");

                }
            }
       // }
        
    }

    // Update is called once per frame
    private IEnumerator StartLocationService()
    {

        //if (!firstStart)
        //{

        //    yield return new WaitForSeconds(5);
        //    //Debug.Log("first start delay over");
        //    firstStart = true;
        //}
        //Debug.Log("try start location");
        locationServiceStartTry = true;

        if (!Input.location.isEnabledByUser)
        {
            //Debug.Log("Пользователь не разрешил использовать геолокацию");
            GPSTextController.instance.SetText("Нет разрешения использовать\nгеолокацию");
            GPSTextController.instance.ShowText();

            yield return new WaitForSeconds(4);
            GPSTextController.instance.HideText();
            //locationServiceStartTry = false;
            yield break;
        }

        Input.location.Start();

        //Input.location.Stop();

        //if (firstStart)
        //{

        //    yield return new WaitForSeconds(5);
        //   // Debug.Log("second start delay over");

        //}

        int maxWait = 20;

        GPSTextController.instance.SetText("Определение\nместоположения...");
        GPSTextController.instance.ShowText();
        yield return new WaitForSeconds(2);

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {

            GPSTextController.instance.SetText("Ошибка подключения GPS");
            //Debug.Log("Время ожидания GPS вышло");

            yield return new WaitForSeconds(4);
            GPSTextController.instance.HideText();
            //locationServiceStartTry = false;

            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSTextController.instance.SetText("Не удалось\nопределить геолокацию");

            //Debug.Log("Не удалось определить геолокацию");
            yield return new WaitForSeconds(4);
            GPSTextController.instance.HideText();
            //locationServiceStartTry = false;

            yield break;
        }



        //latitude = Input.location.lastData.latitude;
        //longitude = Input.location.lastData.longitude;

        locationServiceStart = true;
        StartCoroutine(StartDelayStopLocation());

        //latitude = Input.location.lastData.latitude;
        //longitude = Input.location.lastData.longitude;

        if (!GPSPointUpdate)
        {
            StartCoroutine(StartUpdateGEO());
        }
        //locationServiceStartTry = false;
        GPSTextController.instance.HideText();
        yield break;
    }

    private IEnumerator StartShowimageRadius1()
    {
        imageRadius1.transform.position = currPosition;
        imagePoint1.transform.position = currPosition + new Vector3(0, 0.13f, 0);
        imagePoint2.transform.position = currPosition;
        imageRadius1.SetActive(true);
        imagePoint1.SetActive(true);
        imagePoint2.SetActive(true);


        while (imageRadius1.transform.localScale.x < 1f)
        {
            imageRadius1TempScale.x += Time.deltaTime;
            imageRadius1TempScale.y = imageRadius1TempScale.x;
            imageRadius1TempScale.z = 1f;
            imageRadius1.transform.localScale = imageRadius1TempScale;
            imagePoint1.transform.localScale = imageRadius1TempScale;
            imagePoint2.transform.localScale = imageRadius1TempScale;
            yield return null;
        }
        imageRadius1TempScale = Vector3.zero;
        StartCoroutine(StartPointMove());
        StartCoroutine(StartShowimageRadius2());
        yield return new WaitForSeconds(1f);
        StartCoroutine(StartShowimageRadius2());
        yield return new WaitForSeconds(1f);
        StartCoroutine(StartShowimageRadius2());
        yield return new WaitForSeconds(1f);
        StartCoroutine(StartFadeGPSPoint());
    }

    private IEnumerator StartShowimageRadius2()
    {
        imageRadius2.transform.position = currPosition;
        imageRadius2.SetActive(true);
        while (imageRadius2.transform.localScale.x < 1f)
        {
            imageRadius2TempScale.x += Time.deltaTime;
            imageRadius2TempScale.y = imageRadius2TempScale.x;
            imageRadius2TempScale.z = 1f;
            imageRadius2.transform.localScale = imageRadius2TempScale;
            yield return null;
        }
        imageRadius2.transform.localScale = Vector3.zero;
        imageRadius2TempScale = Vector3.zero;

    }

    private IEnumerator StartPointMove()
    {
        imagePointTempPosition = Vector3.zero;
        while (imagePoint1.transform.position.y < currPosition.y + 0.13f + 0.1f)
        {
            imagePointTempPosition.y += Time.deltaTime * 0.5f;
            imagePoint1.transform.position = new Vector3(imagePoint1.transform.position.x, currPosition.y + 0.13f + imagePointTempPosition.y, imagePoint1.transform.position.z);
            yield return null;
        }

        while (imagePoint1.transform.position.y > currPosition.y + 0.13f)
        {
            imagePointTempPosition.y -= Time.deltaTime * 0.5f;
            imagePoint1.transform.position = new Vector3(imagePoint1.transform.position.x, currPosition.y + 0.13f + imagePointTempPosition.y, imagePoint1.transform.position.z);
            yield return null;
        }

        while (imagePoint1.transform.position.y < currPosition.y + 0.13f + 0.1f)
        {
            imagePointTempPosition.y += Time.deltaTime * 0.5f;
            imagePoint1.transform.position = new Vector3(imagePoint1.transform.position.x, currPosition.y + 0.13f + imagePointTempPosition.y, imagePoint1.transform.position.z);
            yield return null;
        }

        while (imagePoint1.transform.position.y > currPosition.y + 0.13f)
        {
            imagePointTempPosition.y -= Time.deltaTime * 0.5f;
            imagePoint1.transform.position = new Vector3(imagePoint1.transform.position.x, currPosition.y + 0.13f + imagePointTempPosition.y, imagePoint1.transform.position.z);
            yield return null;
        }

    }

    private IEnumerator StartFadeGPSPoint()
    {
        tempColor = imageRadius1SpriteRenderer.color; 

        while (tempColor.a > 0)
        {
            tempColor.a -= Time.deltaTime;
            imageRadius1SpriteRenderer.color = tempColor;
            imageRadius2SpriteRenderer.color = tempColor;
            imagePoint1SpriteRenderer.color = tempColor;
            imagePoint2SpriteRenderer.color = tempColor;
            yield return null;
        }

        GPSPointUpdate = false;

        imageRadius1.transform.localScale = Vector3.zero;
        imageRadius2.transform.localScale = Vector3.zero;
        imagePoint1.transform.localScale = Vector3.zero;
        imagePoint2.transform.localScale = Vector3.zero;
        imageRadius1.SetActive(false);
        imageRadius2.SetActive(false);
        imagePoint1.SetActive(false);
        imagePoint2.SetActive(false);
        imageRadius1TempScale = Vector3.zero;
        imageRadius2TempScale = Vector3.zero;
        imagePointTempScale = Vector3.zero;
        imagePointTempPosition = Vector3.zero;

        tempColor.a = 1;
        imageRadius1SpriteRenderer.color = tempColor;
        imageRadius2SpriteRenderer.color = tempColor;
        imagePoint1SpriteRenderer.color = tempColor;
        imagePoint2SpriteRenderer.color = tempColor;

        //GPSTextController.instance.HideText();

    }

    private IEnumerator StartUpdateGEO()
    {
        if (!GPSPointUpdate)//(Input.location.status == LocationServiceStatus.Running)
        {
            //Debug.Log($"{latitude},{longitude}");
             latitude = Input.location.lastData.latitude;
             longitude = Input.location.lastData.longitude;
             pointFind = false;
            for (int i = 0; i < pointGPS.Length; i++)
            {
                if (Math.Abs(latitude - pointGPS[i].latitude) < 0.0025 && Math.Abs(longitude - pointGPS[i].longitude) < 0.0025)
                {
                    //Debug.Log($"{pointGPS[i].latitude}, {pointGPS[i].longitude}");
                    pointFind = true;

                    if (latitude >= pointGPS[i].latitude)
                    {
                        shiftLatitudeX = -Mathf.Clamp((Math.Abs(latitude - pointGPS[i].latitude) / 0.005f) * (xRadius), 0, xRadius);
                        shiftLatitudeY = -Mathf.Clamp((Math.Abs(latitude - pointGPS[i].latitude) / 0.005f) * (yRadius), 0, yRadius);
                    }
                    else
                    {
                        shiftLatitudeX = Mathf.Clamp((Math.Abs(latitude - pointGPS[i].latitude) / 0.005f) * (xRadius), 0, xRadius);
                        shiftLatitudeY = Mathf.Clamp((Math.Abs(latitude - pointGPS[i].latitude) / 0.005f) * (yRadius), 0, yRadius);
                    }

                    if (longitude >= pointGPS[i].longitude)
                    {
                        shiftLongitudeX = -Mathf.Clamp((Math.Abs(longitude - pointGPS[i].longitude) / 0.005f) * (xRadius / 2), 0, xRadius / 2);
                        shiftLongitudeY = Mathf.Clamp((Math.Abs(longitude - pointGPS[i].longitude) / 0.005f) * (yRadius), 0, yRadius);
                    }
                    else
                    {
                        shiftLongitudeX = Mathf.Clamp((Math.Abs(longitude - pointGPS[i].longitude) / 0.005f) * (xRadius / 2), 0, xRadius / 2);
                        shiftLongitudeY = -Mathf.Clamp((Math.Abs(longitude - pointGPS[i].longitude) / 0.005f) * (yRadius), 0, yRadius);
                    }


                    //shiftLatitudeX = Mathf.Clamp(((latitude - pointGPS[i].latitude) / 0.5f) * (xRadius - (-xRadius)) - xRadius, -xRadius, xRadius);
                    //shiftLongitudeX = Mathf.Clamp(((longitude - pointGPS[i].longitude) / 0.5f) * (xRadius - (-xRadius)) - xRadius, -xRadius, xRadius);

                    //Debug.Log($"{shiftLatitudeX}, {shiftLongitudeX}");

                    //shiftLatitudeY = Mathf.Clamp(((latitude - pointGPS[i].latitude) / 0.5f) * (yRadius - (-yRadius)) - yRadius, -yRadius, yRadius);
                    //shiftLongitudeY = Mathf.Clamp(((longitude - pointGPS[i].longitude) / 0.5f) * (yRadius - (-yRadius)) - yRadius, -yRadius, yRadius);

                    //Debug.Log($"{shiftLatitudeY}, {shiftLongitudeY}");

                    currPosition = new Vector3(pointGPS[i].transform.position.x + shiftLatitudeX + shiftLongitudeX, pointGPS[i].transform.position.y + shiftLatitudeY + shiftLongitudeY, 1);
                }
                
            }

            if (pointFind)
            {
                CameraController.instance.SetTargetPosition(currPosition.x, currPosition.y);
                CameraController.instance.SetFreezeCamera(true);
                CameraController.instance.SetCameraOnPosition(false);




                for (int i = 0; i < gpsData.Items.Count; i++)
                {
                    // Debug.Log(i);
                    // Debug.Log($"{gpsData.Items[i].latitudePlace}, {gpsData.Items[i].longitudePlace}, {gpsData.Items[i].playerPrefPlace}, {PlayerPrefs.GetInt($"{gpsData.Items[i].playerPrefPlace}_gps", 0)}");
                    if ((Math.Abs(latitude - gpsData.Items[i].latitudePlace) < radiusLatitude && Math.Abs(longitude - gpsData.Items[i].longitudePlace) < radiusLongitude) && PlayerPrefs.GetInt($"{gpsData.Items[i].playerPrefPlace}_gps", 0) == 0)
                    {
                        PlayerPrefs.SetInt($"{gpsData.Items[i].playerPrefPlace}_gps", 1);
                        PlayerPrefs.Save();
                        QuestTextController.instance.SetPlaceGPSText(gpsData.Items[i].playerPrefPlace);
                        questIconArr[i].SetActive(false);
                        questColliderArr[i].enabled = false;

                    }
                }
                //player.transform.position = new Vector3((longitude  - 48.836117f) / 0.00001f *  -0.002318f, (latitude - 53.676680f) / 0.00001f * (0.001293f), 2f);

                if (!CameraController.instance.GetCameraOnPosition())
                {
                    StartCoroutine(StartShowimageRadius1());
                    GPSPointUpdate = true;

                    // GPSTextController.instance.ShowText();
                }

            }
            else
            {
                GPSTextController.instance.SetText("Вы находитесь\nза пределами карты");
                GPSTextController.instance.HideText();
            }

        }

        yield return null;
    }

    public void SetLocationServiceStartTry(bool val)
    {
        locationServiceStartTry = val;
    }

    private IEnumerator StartDelayStopLocation()
    {

        //Debug.Log("StartDelayStopLocation");
        while (curDelayStopLocation > 0)
        {
            yield return new WaitForSeconds(1);
            curDelayStopLocation--;
        }
        Input.location.Stop();
        locationServiceStart = false;

    }

    public bool GetLocationServiceStart()
    {
        return locationServiceStart;
    }


    private void Update()
    {
        if (debug1)
        {
            PlayerPrefs.SetInt($"{gpsData.Items[0].playerPrefPlace}_gps", 1);
            PlayerPrefs.Save();
            QuestTextController.instance.SetPlaceGPSText(gpsData.Items[0].playerPrefPlace);
            questIconArr[0].SetActive(false);
            questColliderArr[0].enabled = false;

        }

        if (debug2)
        {
            PlayerPrefs.SetInt($"{gpsData.Items[1].playerPrefPlace}_gps", 1);
            PlayerPrefs.Save();
            QuestTextController.instance.SetPlaceGPSText(gpsData.Items[1].playerPrefPlace);
            questIconArr[1].SetActive(false);
            questColliderArr[1].enabled = false;

        }

        if (debug3)
        {
            PlayerPrefs.SetInt($"{gpsData.Items[2].playerPrefPlace}_gps", 1);
            PlayerPrefs.Save();
            QuestTextController.instance.SetPlaceGPSText(gpsData.Items[2].playerPrefPlace);
            questIconArr[2].SetActive(false);
            questColliderArr[2].enabled = false;

        }
    }
}
