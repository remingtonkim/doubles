using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame(){
        Vibration.Vibrate(2);
        SceneManager.LoadScene("Game");
    }
    
    public void OnTutorial(){
        Vibration.Vibrate(2);
        SceneManager.LoadScene("Tutorial");
    }
}
