using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2[] controlPointArr;
    private List<Vector2> controlPointList;
    private float xKoef;
    private float yKoef;
    private float hKoef;

    public AudioSource waterAudioSource;
    public AudioSource forestAudioSource;
    public AudioSource windAudioSource;
    public AudioSource musicAudioSource;

    public AudioMixer masterSound;
   // public AudioMixer worldSound;
    public float startMasterSoundLevel;
    private float curMasterSoundLevel;
    private float curWorldSoundLevel;

    public static SoundController instance;

    private bool needWorldSoundUp;
    private bool needWorldSoundDown;
    private bool changeWorldSoundActive;

    public Slider musicSoundSlider;
    public Slider otherSoundSlider;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        controlPointList = new List<Vector2>();

        for (int i = 0; i < controlPointArr.Length; i++)
        {
            controlPointList.Add(controlPointArr[i]);
        }

        curMasterSoundLevel = startMasterSoundLevel;
        SetSound(masterSound, "masterVolume", startMasterSoundLevel);
        StartCoroutine(StartVolume());

        curWorldSoundLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (worldSoundOn)
        //{
            for (int i = 0; i < controlPointList.Count - 1; i++)
            {
                if (CameraController.instance.transform.position.x > controlPointList[i].x && CameraController.instance.transform.position.x < controlPointList[i + 1].x)
                {
                    xKoef = controlPointList[i].x < 0 ? 1f - (1f - (CameraController.instance.transform.position.x - Mathf.Floor(CameraController.instance.transform.position.x))) : (CameraController.instance.transform.position.x - Mathf.Floor(CameraController.instance.transform.position.x));
                    yKoef = controlPointList[i + 1].y * xKoef + controlPointList[i].y * (1 - xKoef);

                    waterAudioSource.volume = 0.5f + Mathf.Clamp(((CameraController.instance.transform.position.y - yKoef) / (3.65f)) * 3.65f, -0.5f, 0.5f);

                    //(((CameraController.instance.cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (10f - 1.65f) + 1.65f)
                    //waterAudioSource.volume = 0.5f + Mathf.Clamp(((CameraController.instance.transform.position.y - yKoef) / ((((CameraController.instance.cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (10f - 2.65f) + 2.65f))) * (((CameraController.instance.cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (10f - 2.65f) + 2.65f), -0.5f, 0.5f);
                    //Debug.Log(Mathf.Clamp(((CameraController.instance.transform.position.y - yKoef) / ((((CameraController.instance.cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (10f - 2.65f) + 2.65f))) * (((CameraController.instance.cam.orthographicSize - 2.5f) / (15f - 2.5f)) * (10f - 2.65f) + 2.65f), -0.5f, 0.5f));
                    forestAudioSource.volume = 1 - waterAudioSource.volume;
                    //Debug.Log($"volume1 = {volume1}; volume2 ] {volume2}");
                }
            }

            hKoef = Mathf.Clamp(((CameraController.instance.cam.orthographicSize - 2.5f) / (15f - 2.5f)) * 1f, 0f, 1f);
            //Debug.Log($"hKoef = {hKoef}");
            waterAudioSource.volume = (waterAudioSource.volume - waterAudioSource.volume * hKoef)* otherSoundSlider.value;
            forestAudioSource.volume = (forestAudioSource.volume - forestAudioSource.volume * (hKoef )) * otherSoundSlider.value; //- System.Convert.ToSingle(!CameraController.instance.GetStartDelayPass2())
        windAudioSource.volume = (hKoef) * otherSoundSlider.value * Mathf.Clamp(((curMasterSoundLevel + 40) / (0 + 40)) * 1f, 0f, 1f);//(1f / (-curMasterSoundLevel + 1f)); //System.Convert.ToSingle(CameraController.instance.GetStartDelayPass());// + System.Convert.ToSingle(!CameraController.instance.GetStartDelayPass2())*0.5f;


        //}
        musicAudioSource.volume = musicSoundSlider.value;

        if (needWorldSoundDown && !changeWorldSoundActive) StartCoroutine(StartWorldSoundDown());
        if (needWorldSoundUp && !changeWorldSoundActive) StartCoroutine(StartWorldSoundUp());
    }

    public void SetSound(AudioMixer mixer, string tag, float soundLevel)
    {
        mixer.SetFloat(tag, soundLevel);
    }

    private IEnumerator StartVolume()
    {
       

        while (curMasterSoundLevel < 0.0f)
        {
            curMasterSoundLevel += Time.deltaTime * 5;
            SetSound(masterSound, "masterVolume", curMasterSoundLevel);
            yield return null;
        }
    }

    public float GethKoef()
    {
        return hKoef;
    }

    private IEnumerator StartWorldSoundUp()
    {
        changeWorldSoundActive = true;

        while (curWorldSoundLevel < 0.0f)
        {
            curWorldSoundLevel += Time.deltaTime * 30;
            SetSound(masterSound, "worldVolume", curWorldSoundLevel);
            yield return null;
        }

        needWorldSoundUp = false;
        changeWorldSoundActive = false;
    }

    private IEnumerator StartWorldSoundDown()
    {
        changeWorldSoundActive = true;

        while (curWorldSoundLevel > -40.0f)
        {
            curWorldSoundLevel -= Time.deltaTime * 30;
            SetSound(masterSound, "worldVolume", curWorldSoundLevel);
            yield return null;
        }
        needWorldSoundDown = false;
        changeWorldSoundActive = false;
    }

    public void WorldSoundDown()
    {
        needWorldSoundDown = true;
        //StopCoroutine(StartWorldSoundUp());
        //needWorldSoundUp = false;
        //changeWorldSoundActive = false;

    }

    public void WorldSoundUp()
    {
        needWorldSoundUp = true;
        //StopCoroutine(StartWorldSoundDown());
        //needWorldSoundDown = false;
        //changeWorldSoundActive = false;
    }
}
