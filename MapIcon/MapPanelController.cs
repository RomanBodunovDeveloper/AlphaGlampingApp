using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanelController : MonoBehaviour
{
    public bool restart;
    public GameObject[] iconObjArr;
    public float speedPosition = 4f;
    public bool start;
    public float yPosition;
    public float zPosition;


    private List<GameObject> panelObjects;
    private List<Vector3> panelObjectsPosition;

    private void Awake()
    {
        panelObjects = new List<GameObject>();
        panelObjectsPosition = new List<Vector3>();
        PanelInit();
        PanelObjPlace();
    }

    void OnEnable()
    {
        for (int i = 0; i < iconObjArr.Length; i++)
        {
            panelObjects[i].transform.localScale = Vector3.zero;
            panelObjects[i].transform.localPosition = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            PanelInit();
            PanelObjPlace();
            restart = false;
        }

        //if (start)
        // {
        for (int i = 0; i < panelObjects.Count; i++)
        {
            panelObjects[i].transform.localPosition = new Vector3(Mathf.Lerp(panelObjects[i].transform.localPosition.x, panelObjectsPosition[i].x, speedPosition * Time.deltaTime), Mathf.Lerp(panelObjects[i].transform.localPosition.y, panelObjectsPosition[i].y, speedPosition * Time.deltaTime), zPosition);
            panelObjects[i].transform.localScale = new Vector3(Mathf.Lerp(panelObjects[i].transform.localScale.x, 1, speedPosition * Time.deltaTime), Mathf.Lerp(panelObjects[i].transform.localScale.y, 1, speedPosition * Time.deltaTime), zPosition);

        }

        //}


    }

    private void PanelInit()
    {
        panelObjects.Clear();
        panelObjectsPosition.Clear();
        for (int i = 0; i < iconObjArr.Length; i++)
        {
            panelObjects.Add(iconObjArr[i]);
            panelObjectsPosition.Add(Vector3.zero);
            panelObjects[i].transform.localScale = Vector3.zero;
        }
    }

    private void PanelObjPlace()
    {
        if (panelObjects.Count == 1)
        {
            panelObjectsPosition[0] = new Vector3(0, yPosition, zPosition);
        }
        if (panelObjects.Count > 1)
        {
            float startPosX = panelObjects.Count * (-0.22f) + 0.22f;
            for (int i = 0; i < panelObjects.Count; i++)
            {
                panelObjectsPosition[i] = new Vector3(startPosX + 0.44f * i, yPosition, zPosition);
            }
        }
    }

    public void PanelObjActivate(bool val)
    {
        for (int i = 0; i > panelObjects.Count; i++)
        {
            panelObjects[i].SetActive(val);
        }
    }
}