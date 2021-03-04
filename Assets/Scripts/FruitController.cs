using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public GameObject cut_left;
    public GameObject cut_right;
    public GameObject splash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // if(collision.gameObject.tag == "Line")
        if(collision.gameObject.tag == "Blade")
        {
            // spawn splash effect
            GameObject s = Instantiate(splash, transform.position, Quaternion.identity) as GameObject;
            Destroy(s.gameObject, 2f);

            // GameObject c1 = Instantiate(cut_right, transform.position, Quaternion.identity) as GameObject;
            GameObject c1 = Instantiate(cut_right, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity) as GameObject;
            GameObject c2 = Instantiate(cut_left, new Vector3(transform.position.x - 1, transform.position.y, 0), cut_left.transform.rotation) as GameObject;

            c1.GetComponent<Rigidbody2D>().AddForce(new Vector2(2f, 2f), ForceMode2D.Impulse);      // add force
            c1.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-2f, 2f), ForceMode2D.Impulse);   // add random rotation

            c2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2f, 2f), ForceMode2D.Impulse);      // add force
            c2.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-2f, 2f), ForceMode2D.Impulse);   // add random rotation

            Destroy(gameObject);
            Destroy(c1, 2f);
            Destroy(c2, 2f);   
            ScoreController.instance.IncrementScore();
        }
    }
}
