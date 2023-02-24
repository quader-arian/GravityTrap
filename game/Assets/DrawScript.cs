using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    private Camera cam;
    public GameObject brush;
    public Transform daddy;
    private Rigidbody2D body;
    private bool start;
    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    void Start(){
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        body = GetComponent<Rigidbody2D>();
        start = false;
    }

    void Update(){
        Draw();
    }
    
    void Draw(){
        if(Input.GetButtonDown("Fire1") && body.velocity == Vector2.zero){
            CreateBrush();
            start = true;
        }
        else if(body.velocity != Vector2.zero && start){
            Vector2 mousePos = this.transform.position;
            if(mousePos != lastPos){
                AddAPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else{
            currentLineRenderer = null;
        }

    }

    void CreateBrush(){
        GameObject brushInstance = Instantiate(brush);
        brushInstance.transform.SetParent(daddy);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = this.transform.position;;

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
    }

    void AddAPoint(Vector2 pointPos){
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount -1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }
}