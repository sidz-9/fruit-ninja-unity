using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject bladeTrailPrefab;
    public GameObject blast;
    public float minCuttingVelocity = 5f;
    bool isCutting = false;
    bool gameOver;

    // float velocity;

    Vector2 previousPosition;

    GameObject currentBladeTrail;
    Rigidbody2D rb;
    Camera cam;
    CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !gameOver) {
            StartCutting();
        }
        else if(Input.GetMouseButtonUp(0)) {
            StopCutting();
        }

        if(isCutting && !gameOver) {
            UpdateCut();
        }
    }

    void UpdateCut() {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        // newPosition - previousPosition gives the length of the vector, 
        // then we convert it into magnitude, to get the speed of which the mouse is travelling
        float velocity = (newPosition - previousPosition).magnitude / Time.deltaTime;    

        if(velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
        }
        else 
        {
            circleCollider.enabled = false;
        }

        previousPosition = newPosition;
    }

    void StartCutting() {
        isCutting = true;
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        circleCollider.enabled = false;
    }

    void StopCutting() {
        isCutting = false;
        currentBladeTrail.transform.SetParent(null);
        Destroy(currentBladeTrail, 2f);
        circleCollider.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Bomb") {
            GameObject b = Instantiate(blast, col.transform.position, Quaternion.identity) as GameObject;
            Destroy(b.gameObject, 1f);
            Destroy(col.gameObject);
            // GameController.instance.gameOver = true;
                 // will not collide with existing objects(if any exists)
            gameOver = true;
            GameController.instance.StopGame();
        }
    }
}
