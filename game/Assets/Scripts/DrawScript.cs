using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    private Camera cam;
    private bool waitForLine = false;
    private bool intersect = false;
    public GameObject brush;
    public GameObject sensor;
    public GameObject allLines;
    private Rigidbody2D body;
    private Vector3[] allPoints;
    private LineRenderer currentLineRenderer;
    private GameObject currentLineObject;
    public Gradient gradient;

    private Vector2 lastPos;
    public int tolerance;
    private int currTol;
    //private Collider2D lastCol;
    //private Collider2D curCol;

    void Start(){
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        body = GetComponent<Rigidbody2D>();
        CreateBrush();
        currTol = tolerance;
    }

    void Update(){
        if(!waitForLine){
            Vector2 mousePos = this.transform.position;
            float round = 1000f;
            float mpRoundX = Mathf.Round(mousePos.x * round) / round;
            float mpRoundY = Mathf.Round(mousePos.y * round) / round;
            float lpRoundX = Mathf.Round(lastPos.x * round) / round;
            float lpRoundY = Mathf.Round(lastPos.y * round) / round;
            if(mpRoundX != lpRoundX && mpRoundY != lpRoundY){
                currTol--;
                if(currTol<=0){
                    AddAPoint(mousePos);
                    currTol = tolerance;
                }
                lastPos = mousePos;
            }
        }
        else if(body.velocity == Vector2.zero){
            waitForLine = false;
            CreateBrush();
        }

        if(intersect){
            Debug.Log("Intersect");
            Destroy(currentLineObject, 0.5f);
            currentLineRenderer.colorGradient = gradient;
            currentLineRenderer = null;
            waitForLine = true;
            intersect = false;
        }
    }

    void CreateBrush(){
        GameObject brushInstance = Instantiate(brush);
        currentLineObject = brushInstance;
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.transform.parent = allLines.transform;

        Vector2 mousePos = this.transform.position;

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
        GameObject sensorInstance = Instantiate(sensor, mousePos, Quaternion.identity);
        sensorInstance.transform.parent = currentLineRenderer.transform;
    }

    void AddAPoint(Vector2 pointPos){
        
            currentLineRenderer.positionCount++;
            int positionIndex = currentLineRenderer.positionCount -1;
            currentLineRenderer.SetPosition(positionIndex, pointPos);
            GameObject sensorInstance = Instantiate(sensor, pointPos, Quaternion.identity);
            sensorInstance.transform.parent = currentLineRenderer.transform;
    }

    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject.tag == "Sensor"){
            col.gameObject.GetComponent<SensorScript>().prevSensor = false;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Sensor"){
            col.gameObject.GetComponent<SensorScript>().prevSensor = true;
        }
    }

    //when it enters for first time
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Sensor"){
            Vector2 intersectPoint = col.transform.position;
            AddAPoint(intersectPoint);
            if(col.gameObject.GetComponent<SensorScript>().prevSensor){
                intersect = true;
            }
        }
    }
}