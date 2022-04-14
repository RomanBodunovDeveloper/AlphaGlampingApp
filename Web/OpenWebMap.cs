using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebMap : MonoBehaviour
{
    private string urlInput;
    private string targetCoor;
    public string playerPref;
    public string casualPlaseCoord;

    private void Start()
    {
        for (int i = 0; i < GPSController.instance.gpsData.Items.Count; i++)
        {
            if (playerPref == GPSController.instance.gpsData.Items[i].playerPrefPlace)
            {
                targetCoor = $"{GPSController.instance.gpsData.Items[i].latitudePlace},{GPSController.instance.gpsData.Items[i].longitudePlace}";
                //Debug.Log(targetCoor);
                break;
            }
            else
            {
                targetCoor = casualPlaseCoord;
            }
        }
    }
    public void UrlOpen(string url)
    {
        //UNITY_WEBGL
        Application.OpenURL(url);
    }

    //private void Update()
    //{
    //    if (targetCoor == "null")
    //    {
    //        Debug.Log("null");
    //        if (CameraController.instance.GetStartDelayPass())
    //        {
    //            for (int i = 0; i < GPSController.instance.gpsData.Items.Count; i++)
    //            {
    //                Debug.Log(playerPref);
    //                if (playerPref == GPSController.instance.gpsData.Items[i].playerPrefPlace)
    //                {
    //                    targetCoor = $"{GPSController.instance.gpsData.Items[i].latitudePlace},{GPSController.instance.gpsData.Items[i].longitudePlace}";
    //                    Debug.Log(targetCoor);
    //                    break;
    //                }
    //                else
    //                {
    //                    targetCoor = casualPlaseCoord;
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("return");
    //        return;
    //    }


    //}

    public void OnMouseUp()
    {
        //urlInput = "http://yandex.ru/maps/?rtext=" + GPSController.instance.latitude.ToString() + "," + GPSController.instance.longitude.ToString() + "~" + targetCoor + "&rtt=auto";
        // Debug.Log(urlInput);
        //UrlOpen(urlInput);
        StartCoroutine(CheckLocationService());
    }

    // http://yandex.ru/maps/?rtext=59.967870,30.242658~59.898495,30.299559&rtt=mt

    private IEnumerator StartDelayGPS()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log(playerPref);
        for (int i = 0; i < GPSController.instance.gpsData.Items.Count; i++)
        {
            if (playerPref == GPSController.instance.gpsData.Items[i].playerPrefPlace)
            {
                targetCoor = $"{GPSController.instance.gpsData.Items[i].latitudePlace},{GPSController.instance.gpsData.Items[i].longitudePlace}";
                Debug.Log(targetCoor);
                break;
            }
            else
            {
                targetCoor = casualPlaseCoord;
            }
        }
    }

    private IEnumerator CheckLocationService()
    {
        GPSController.instance.ClickUpdate();

        int maxWait = 20;

        while (!GPSController.instance.GetLocationServiceStart() && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (GPSController.instance.GetLocationServiceStart())
        {
            urlInput = "http://yandex.ru/maps/?rtext=" + GPSController.instance.latitude.ToString() + "," + GPSController.instance.longitude.ToString() + "~" + targetCoor + "&rtt=auto";
            UrlOpen(urlInput);
        }
    }
}
