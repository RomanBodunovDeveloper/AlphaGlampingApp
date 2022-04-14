using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystem;

public class GridElementController : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite iconImageIdle;
    public Sprite iconImageActive;
    public string titleText;
    public Image iconImageView;
    public Text titleTextView;

    void Start()
    {
        //iconImageView.sprite = iconImageIdle;
        titleTextView.text = titleText;
    }



}



