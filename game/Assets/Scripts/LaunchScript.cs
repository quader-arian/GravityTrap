using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaunchScript : MonoBehaviour
{
    private Rigidbody2D body;
    private Camera cam;
    private Vector2 mousePos;
    private AudioSource source;
    private bool canLaunch = true;
    public GameObject dialogueBox;
    public int dialogueCount;
    public TMP_Text t;

    public AudioClip [] sounds;

    public float force = 20f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        body = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetButtonDown("Fire1")){
            if(canLaunch){
                Lauch();
            }else{
                if(dialogueCount == 0){
                    t.text = "THEY BUSTED ME UP PRETTY BAD.";
                }else if(dialogueCount == 1){
                    t.text = "HOW AM I SUPPOSED TO PROTECT EVERYONE NOW?";
                }else if(dialogueCount == 2){
                    t.text = "I SEE YOUR GRAVITY CHIP STILL WORKS.";
                }else if(dialogueCount == 3){
                    t.text = "WATCH OUT FOR THE ENEMY TO THE RIGHT.";
                }else if(dialogueCount == 4){
                    t.text = "TO TAKE EM OUT, MOVE AROUND AND CLOSE YOUR TRACE BY TOUCHING THE LIGHTNING FLUX POINTS ON YOUR TRAIL";
                }else{
                    t.text = "TRAPPING THEM WITHIN YOUR GRAVITY AURA SHOULD DO THE TRICK";
                    canLaunch = true;
                }
                dialogueCount++;
            }
            int num = Random.Range(0, sounds.Length);
            source.clip = sounds[num];
            source.Play();
       }
    }

    void Lauch(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = transform.position;
        Vector2 lookDir = (mousePos - pos);

        //float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rotZ);
        body.AddForce(lookDir.normalized * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Dialogue" && dialogueCount <= 0){
            canLaunch = false;
            dialogueBox.SetActive(true);
        }
    }
}
