using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnner : MonoBehaviour
{
    public GameObject fruit;
    public GameObject bomb;
    public float maxX;
    public float force;
    public float torque;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartSpawning", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning() {
        InvokeRepeating("SpawnFruitGroups", 1f, 6f);
    }

    public void StopSpawning() {
        CancelInvoke("SpawnFruitGroups");
        StopCoroutine("SpawnFruit");
    }

    public void SpawnFruitGroups() {
        // starts the coroutine
        StartCoroutine("SpawnFruit");

        // if(Random.Range(0, 5) > 2) {
            SpawnBomb();
        // }
    }

    IEnumerator SpawnFruit() {

        for (int i = 0; i < 5; i++)
        {
            // Get random posistion of fruit
            float randomPos = Random.Range(-maxX, maxX);
            Vector3 pos = new Vector3(randomPos, transform.position.y, 0);
            GameObject newFruit = Instantiate(fruit, pos, Quaternion.identity) as GameObject;
            
            newFruit.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);    // add upward force
            newFruit.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-torque, torque));    // add rotation

            yield return new WaitForSeconds(0.5f);      // will wait for 0.5s to spawn the fruit again(going out of the function and calling it one more time)
        }
    }

    void SpawnBomb() {
        float randomPos = Random.Range(-maxX, maxX);
        Vector3 pos = new Vector3(randomPos, transform.position.y, 0);
        GameObject newBomb = Instantiate(bomb, pos, Quaternion.identity) as GameObject;
        
        newBomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);    // add upward force
        newBomb.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));    // add rotation  
    }
}
