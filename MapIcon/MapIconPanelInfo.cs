using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapIconPanelInfo : MonoBehaviour
{
    public int recordIndex;
    //public BlackScreenController blackScreenController;
    //public GameObject panelGrid;
    // public BookDataTransfer bookDataTransfer;
    private void OnMouseUp()
    {
        // bookDataTransfer.SetRecordIndexTransfer(recordIndex);
        // SceneManager.LoadScene(1);
        BlackScreenController.instance.TransferRecordIndex(recordIndex - 1 + 6);
        BlackScreenController.instance.SetNeedBookOpen();
        
        // BlackScreenController.instance.PermamentBookOpen();
        ScrollViewAdapterv2.instance.ActivateTargetIndex(recordIndex);
        //panelGrid.gameObject.SetActive(false);
        //blackScreenController.UpBlackScreen();
        


    }
}
