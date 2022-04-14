using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBannerController : MonoBehaviour
{
    private bool growed; //признак - объект отмасшатбирован
    private float tempFloat; // промежетучная величина мастабирования
    public float growSpeed; // скорость масштабирования
    private Camera cam;

    private void OnEnable() 
    {
        //при активации объекта сбрасывает все настройки масштабирования на 0
        transform.localScale = Vector3.zero;
        growed = false;
        tempFloat = 0f;
    }

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        //при активации увеличиваем масштабирование объекта по Х с но 0 до 1
        if (!growed)
        {
            tempFloat += Time.deltaTime * growSpeed * Mathf.Clamp(cam.orthographicSize * 0.2f, 1f, 3f) * 1.2f;
            transform.localScale = new Vector3(tempFloat, Mathf.Clamp(cam.orthographicSize * 0.2f, 1f, 3f) * 1.2f, 1);
            if (tempFloat >= Mathf.Clamp(cam.orthographicSize * 0.2f, 1f, 3f) * 1.2f)
            {
                tempFloat = 0f;
                growed = true;
            }
        }

    }
}

//transform.localScale = new Vector3(Mathf.Clamp(cam.orthographicSize* 0.2f, 1f, 3f), Mathf.Clamp(cam.orthographicSize* 0.2f, 1f, 3f), transform.localScale.z);


   // banner.transform.position = new Vector3(this.transform.position.x, (this.transform.position.y + (spriteRender.sprite.bounds.size.y / 2f + 0.2f)* transform.localScale.y), banner.transform.position.z);
