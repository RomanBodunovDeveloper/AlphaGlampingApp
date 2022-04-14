using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSDebug : MonoBehaviour
{
    public Text latitudeText;
    public Text longitudeText;

    // Update is called once per frame
    void Update()
    {
        latitudeText.text = "latitude: " + GPSController.instance.latitude.ToString();
        longitudeText.text = "longitude: " + GPSController.instance.longitude.ToString();
    }
}
