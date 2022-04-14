using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDataTransfer : MonoBehaviour
{
    // Start is called before the first frame update
    public float recordIndexTransfer;
    public static BookDataTransfer instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void SetRecordIndexTransfer(float val)
    {
        recordIndexTransfer = val;
    }

    public float GetRecordIndexTransfer()
    {
        return recordIndexTransfer;
    }
}
