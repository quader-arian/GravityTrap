using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript1 : MonoBehaviour
{
    private Camera cam;
    private bool waitForLine1 = false;
    private bool waitForLine2 = false;
    private bool intersect = false;
    public GameObject brush;
    public GameObject sensor;
    private Rigidbody2D body;
    private Vector3[] allPoints;
    private LineRenderer currentLineRenderer;
    private GameObject currentLineObject;
    public Gradient gradient;

    private Vector2 lastPos;
    private int firstPointArea;

    private PolygonCollider2D collideArea;
    private MeshFilter collideVisual;

    private CameraShake shake;

    void Start(){
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        CreateBrush();
    }

    void Update(){
        if(!waitForLine1 && !waitForLine2){
            Vector2 mousePos = this.transform.position;
            float round = 10f;
            float mpRoundX = Mathf.Round(mousePos.x * round) / round;
            float mpRoundY = Mathf.Round(mousePos.y * round) / round;
            float lpRoundX = Mathf.Round(lastPos.x * round) / round;
            float lpRoundY = Mathf.Round(lastPos.y * round) / round;
            if(!(mpRoundX == lpRoundX && mpRoundY == lpRoundY)){
                if(!GameObject.FindWithTag("Player").GetComponent<GravityScript>().onGround){
                    Vector3[] pos = new Vector3[currentLineRenderer.positionCount];
                    currentLineRenderer.GetPositions(pos);
                    firstPointArea = 0;
                    foreach(Vector3 p in pos){
                        if(mpRoundX == p.x && mpRoundY == p.y){
                            intersect = true;
                            break;
                        }else{
                            firstPointArea++;
                        }
                    }
                }

                if(!intersect){
                    AddAPoint(new Vector2(mpRoundX, mpRoundY));
                    lastPos = new Vector2(mpRoundX, mpRoundY);
                }
            }
        }else if(GetComponentInParent<GravityScript>().onGround){
            waitForLine1 = false;
        }else if(GetComponentInParent<GravityScript>().onGroundExit && !waitForLine1){
            waitForLine2 = false;
            CreateBrush();
        }

        if(intersect){
            if(currentLineObject.GetComponent<PolygonCollider2D>() == null){
                collideArea = currentLineObject.AddComponent<PolygonCollider2D>();
            }
            collideArea.isTrigger = true;

            int totalPoints = currentLineRenderer.positionCount;
            Vector3[] pos = new Vector3[totalPoints];
            Vector2[] newPos = new Vector2[totalPoints - firstPointArea];
            currentLineRenderer.GetPositions(pos);
            int i = 0;
            int j = 0;
            foreach(Vector3 temp in pos){
                Vector2 p = temp;
                if(i >= firstPointArea){
                    newPos[j] = p;
                    j++;
                }
                i++;
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

            Destroy(currentLineObject, 0.5f);
            currentLineRenderer.colorGradient = gradient;
            currentLineRenderer = null;
            waitForLine1 = true;
            waitForLine2 = true;
            intersect = false;
            StartCoroutine(shake.Shake(.1f, .1f));
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
}