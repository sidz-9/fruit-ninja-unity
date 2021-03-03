using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    int vertexCount = 0;
    bool mouseDown = false;
    LineRenderer line;
    public GameObject blast;

    void Awake() {
        line = GetComponent<LineRenderer>();    // get access of line renderer component
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android) 
        {
            if(Input.touchCount > 0)    // there is at least one touch on screen
            {      
                if(Input.GetTouch(0).phase == TouchPhase.Moved)   // Get first touch and if the first touch is moving
                {
                    // line.SetVertexCount(vertexCount + 1); SetVertexCount is obselete
                    line.positionCount = vertexCount + 1;   // increment vertex count of the line
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // converts screen coordinates to world coordinates
                    line.SetPosition(vertexCount, mousePos);    // setting index of line point(vertexCount) and the next point(mousePos)
                    vertexCount++;  // increment vertex count as mouse is still pressed

                    // attach DoxCollider2D to line
                    BoxCollider2D boxCol = gameObject.AddComponent<BoxCollider2D>();
                    boxCol.transform.position = line.transform.position;    // changing posistion of box collider to position of line
                    boxCol.size = new Vector2(0.1f, 0.1f);
                }

                if(Input.GetTouch(0).phase == TouchPhase.Ended)     // delete line when touch phase is ended(stop touching the screen)
                {
                    mouseDown = false;
                    vertexCount = 0;
                    line.positionCount = 0;

                    // Destroy the BoxColliders
                    BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();     // get all boxColliders and store in colliders array
                    foreach (BoxCollider2D box in colliders)
                    {
                        Destroy(box);
                    }
                }
            }    
        }
        // else if(Application.platform == RuntimePlatform.WindowsPlayer) 
        else {
            if(Input.GetMouseButton(0)) {
                mouseDown = true;
            }

            // whenever mouse is pressed, creating the line and incrementing the line. 
            // Moving position of line according to position of mouse
            if(mouseDown) {
                // line.SetVertexCount(vertexCount + 1); SetVertexCount is obselete
                line.positionCount = vertexCount + 1;   // increment vertex count of the line
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // converts screen coordinates to world coordinates
                line.SetPosition(vertexCount, mousePos);    // setting index of line point(vertexCount) and the next point(mousePos)
                vertexCount++;  // increment vertex count as mouse is still pressed

                // attach DoxCollider2D to line
                BoxCollider2D boxCol = gameObject.AddComponent<BoxCollider2D>();
                boxCol.transform.position = line.transform.position;    // changing posistion of box collider to position of line
                boxCol.size = new Vector2(0.1f, 0.1f);
            }

            // delete line when mouse is unpressed
            if(Input.GetMouseButtonUp(0)) {
                mouseDown = false;
                vertexCount = 0;
                line.positionCount = 0;

                // Destroy the BoxColliders
                BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();     // get all boxColliders and store in colliders array
                foreach (BoxCollider2D box in colliders)
                {
                    Destroy(box);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Bomb") {
            GameObject b = Instantiate(blast, col.transform.position, Quaternion.identity) as GameObject;
            Destroy(b.gameObject, 1f);
            Destroy(col.gameObject);
            // GameController.instance.gameOver = true;
            GameController.instance.StopGame();
        }
    }
}
