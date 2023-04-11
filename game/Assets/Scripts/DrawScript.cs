using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawScript : MonoBehaviour
{
    private bool waitForLine1 = false;
    private bool waitForLine2 = false;
    private bool intersect = false;
    public GameObject brush;
    public GameObject sensor;
    private Vector3[] allPoints;
    private LineRenderer currentLineRenderer;
    private GameObject currentLineObject;
    public Gradient gradient;
    public Color col;
    private bool inNoDrawArea = false;

    private Vector2 lastPos;
    public int tolerance;
    private int currTol;

    private PolygonCollider2D collideArea;
    private MeshFilter collideVisual;
    private bool foundFirst;

    private CameraController cameraController;

    void Start(){
        //CreateBrush();
        waitForLine1 = true;
        waitForLine2 = true;
        intersect = false;
        currTol = tolerance;
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    void Update(){
        if(!waitForLine1 && !waitForLine2){
            Vector2 mousePos = this.transform.position;
            float round = 1000f;
            float mpRoundX = Mathf.Round(mousePos.x * round) / round;
            float mpRoundY = Mathf.Round(mousePos.y * round) / round;
            float lpRoundX = Mathf.Round(lastPos.x * round) / round;
            float lpRoundY = Mathf.Round(lastPos.y * round) / round;
            if(!(mpRoundX == lpRoundX && mpRoundY == lpRoundY)){
                currTol--;
                if(currTol<=0){
                    AddAPoint(new Vector2(mpRoundX, mpRoundY));
                    currTol = tolerance;
                }
                lastPos = new Vector2(mpRoundX, mpRoundY);
            }
        }else if(GetComponentInParent<GravityScript>().onGround){
            waitForLine1 = false;
        }else if(GetComponentInParent<GravityScript>().onGroundExit && !waitForLine1){
            waitForLine2 = false;
            CreateBrush();
        }

        if(intersect){
            Debug.Log("Intersect");
            Destroy(currentLineObject, 0.5f);
            currentLineRenderer.colorGradient = gradient;
            currentLineRenderer = null;
            waitForLine1 = true;
            waitForLine2 = true;
            foreach(Transform child in GameObject.FindWithTag("Trail").transform.GetChild(0).transform){
                child.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = col;
                child.GetComponent<SpriteRenderer>().color = col;
            }
            intersect = false;
            cameraController.StartShake(0.1f, 0.1f);
        }
    }

    void CreateBrush(){
        GameObject brushInstance = Instantiate(brush);
        currentLineObject = brushInstance;
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.transform.parent = GameObject.FindWithTag("Trail").transform;

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
        if (col.gameObject.tag == "NoDrawArea"){
            inNoDrawArea = true;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Sensor"){
            col.gameObject.GetComponent<SensorScript>().prevSensor = true;
        }
        if (col.gameObject.tag == "NoDrawArea"){
            inNoDrawArea = false;
        }
    }

    //when it enters for first time
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "NoDrawArea"){
            inNoDrawArea = true;
        }
        if (col.gameObject.tag == "Sensor" && col.gameObject.GetComponent<SensorScript>().prevSensor && !inNoDrawArea && currentLineRenderer != null){
            Vector2 intersectPoint = col.transform.position;
            AddAPoint(intersectPoint);
            intersect = true;

            if(currentLineObject.GetComponent<PolygonCollider2D>() == null){
                collideArea = currentLineObject.AddComponent<PolygonCollider2D>();
            }
            collideArea.isTrigger = true;

            Vector3[] pos = new Vector3[currentLineRenderer.positionCount];
            int count = 0;
            foundFirst = false;
            currentLineRenderer.GetPositions(pos);
            foreach(Vector3 temp in pos){
                Vector2 p = temp;
                if(p == intersectPoint && !foundFirst){
                    foundFirst = true;
                    count++;
                }else if(foundFirst){
                    count++;
                }
            }
            int maxCount = count - 1;
            Vector2[] newPos = new Vector2[maxCount];
            count = 0;
            foundFirst = false;
            foreach(Vector3 temp in pos){
                Vector2 p = temp;
                if(p == intersectPoint && !foundFirst && count < maxCount){
                    foundFirst = true;
                    newPos[count] = p;
                    count++;
                }else if(foundFirst && count < maxCount){
                    newPos[count] = p;
                    count++;
                }
            }
            collideArea.SetPath(0, newPos);

            if(currentLineObject.GetComponent<MeshFilter>() == null){
                collideVisual = currentLineObject.AddComponent<MeshFilter>();
            }
            if(collideArea != null && collideVisual != null && collideArea.GetTotalPointCount() > 5){
                collideVisual.mesh = collideArea.CreateMesh(true, true);
            }else{
                Destroy(collideVisual.mesh);
            }
        }
    }
}