using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    private Camera cam;
    public GameObject brush;
    public GameObject sensor;
    public GameObject allLines;
    private Rigidbody2D body;
    private Vector3[] allPoints;
    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    void Start(){
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        body = GetComponent<Rigidbody2D>();
        CreateBrush();
    }

    void Update(){
        if(body.velocity != Vector2.zero){
            Vector2 mousePos = this.transform.position;
            if(mousePos != lastPos){
                AddAPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else{
            if(checkIntersect(currentLineRenderer)){
                Debug.Log("Intersect");
                currentLineRenderer = null;
                CreateBrush();
            }
        }
    }

    void CreateBrush(){
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        brushInstance.transform.parent = allLines.transform;

        Vector2 mousePos = this.transform.position;;

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
        //GameObject sensorInstance = Instantiate(sensor, mousePos, Quaternion.identity);
        //sensorInstance.transform.parent = allLines.transform;
    }

    void AddAPoint(Vector2 pointPos){
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount -1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
        //GameObject sensorInstance = Instantiate(sensor, pointPos, Quaternion.identity);
        //sensorInstance.transform.parent = allLines.transform;
    }

    bool checkIntersect(LineRenderer line){
        for (int i = 1; i < line.positionCount; i++) {
            for (int j = i + 1 ; j < line.positionCount; j++) {
                if ((Mathf.Round(line.GetPosition(i).x * 1000f) / 1000f == Mathf.Round(line.GetPosition(j).x * 1000f) / 1000f) && (Mathf.Round(line.GetPosition(i).y * 1000f) / 1000f == Mathf.Round(line.GetPosition(j).y * 1000f) / 1000f)) {
                    Debug.Log(Mathf.Round(line.GetPosition(i).x * 1000f)/ 1000f);
                    Debug.Log(Mathf.Round(line.GetPosition(j).x * 1000f)/ 1000f);
                    return true;
                }
            }
        }
        return false;
    }
}