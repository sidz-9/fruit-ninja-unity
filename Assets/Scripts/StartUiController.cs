using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartUiController : MonoBehaviour
{
    public GameObject fruitNinja_Image;

    public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {   //
        fruitNinja_Image.GetComponent<Animator>().Play("FruitNinja_SlideUp");
        startButton.GetComponent<Animator>().Play("StartButton_Fade");
        SceneManager.LoadScene("Level1");
    }
}
