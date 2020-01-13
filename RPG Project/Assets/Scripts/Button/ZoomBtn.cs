using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomBtn : MonoBehaviour
{
    enum zoomLevels {zoom1=33, zoom2=40, zoom3=48}

    public Sprite[] sprite;

    [SerializeField] zoomLevels zoomLevel = zoomLevels.zoom1;
    [SerializeField] float zoomSpeed = 1f;

    private int zoomLevelsCounts = System.Enum.GetValues(typeof(zoomLevels)).Length;

    private void Awake() {
        SetZoomLevel();
    }

    private void Update() {
        ZoomCamera();
    }

    public void SetZoomLevel()
    {
        int index = System.Array.IndexOf(System.Enum.GetValues(zoomLevel.GetType()), zoomLevel);
        //Debug.Log("enum: " + (int)zoomLevel + ", count: " + zoomLevelsCounts.ToString() + ", index: " + index);

        if (index >= zoomLevelsCounts-1)
        {
            index=0;
            GetComponent<Image>().sprite = sprite[0];
        }else {
            index++;
            if(index >= zoomLevelsCounts - 1)
            {
                GetComponent<Image>().sprite = sprite[1];
            }
        }

        zoomLevel = (zoomLevels) (System.Enum.GetValues(zoomLevel.GetType())).GetValue(index); 
    }

    void ZoomCamera(){
        float zoomDifference = (float)zoomLevel - Camera.main.fieldOfView;
        Camera.main.fieldOfView += zoomDifference * zoomSpeed * Time.deltaTime;
        if(Mathf.Abs(zoomDifference) <= 0.05f){
            Camera.main.fieldOfView = (float)zoomLevel;
        }
    }
}
