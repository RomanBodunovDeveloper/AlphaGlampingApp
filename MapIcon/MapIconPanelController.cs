using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIconPanelController : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite activeSprite;
    private SpriteRenderer spriteRender;
    public Camera cam;
    private CameraController camController;
    public GameObject titleObj;

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        camController = cam.GetComponent<CameraController>();
        titleObj.SetActive(false);
    }

    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnMouseOver()
    {
        spriteRender.sprite = activeSprite;
        camController.SetCursorOnActiveIcon(true);
        titleObj.SetActive(true);
    }

    private void OnMouseExit()
    {
        spriteRender.sprite = idleSprite;
        camController.SetCursorOnActiveIcon(false);
        titleObj.SetActive(false);
    }
}
