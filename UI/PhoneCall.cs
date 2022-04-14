using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCall : MonoBehaviour
{
    // Start is called before the first frame update
    public string stringToEdit;
    private TouchScreenKeyboard keyboard;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPhoneNumber()
    {
        Debug.Log("Click");
       // keyboard = TouchScreenKeyboard.Open(stringToEdit, TouchScreenKeyboardType.PhonePad, true, false, false, false, "");
        //keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        // public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default, bool autocorrection = true, bool multiline = false, bool secure = false, bool alert = false, string textPlaceholder = "");
        Application.OpenURL($"tel:{stringToEdit}") ;
    }
}
