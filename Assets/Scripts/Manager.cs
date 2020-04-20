using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject current;
    public GameObject prefab;

    public RawImage play;
    public RawImage pause;

    public RawImage home;

    public GameObject obstaclePrefab;
    private static Manager instance;

    public Button restart;

    public Button homeButton;

    public Text highScore;
    public static Manager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Manager>();
            }
            return instance;
        }
    }

    private Stack<GameObject> pieces;
    public Stack<GameObject> Pieces {
        get => pieces;
        set => pieces = value;
    }

    private int rightBlocked = 0;
    private int leftBlocked = 0;
    // Start is called before the first frame update
    void Start()
    {
        highScore.enabled = false;
        homeButton.gameObject.SetActive(false);
        home.enabled = false;
        restart.gameObject.SetActive(false);
        pieces = new Stack<GameObject>();
        CreatePieces(150);
        for(int i = 0; i<100; i++)
        {
            SpawnPieces();
        }
        
    }

    public void CreatePieces(int count){
        for(int i =0; i<count; i++){
            Pieces.Push(Instantiate(prefab));
            Pieces.Peek().SetActive(false);
        }
    }

    public void SpawnPieces(){
        if(Pieces.Count==0){
            CreatePieces(10);
        }
        GameObject temp = Pieces.Pop();
        temp.SetActive(true);
        temp.transform.position = current.transform.GetChild(0).position + new Vector3(0, 0, 1.25f);
        if(Random.Range(0, 2)==0){
            if(rightBlocked==0){
                if(Random.Range(0,8)==0){
                    temp.transform.GetChild(3).gameObject.SetActive(true);
                    leftBlocked = 1;
                }
                
            }
            else if(rightBlocked==1){
                rightBlocked = 2;
            }
            else{
                rightBlocked = 0;
            }
        }
        else{
            if(leftBlocked==0){
                if(Random.Range(0,8)==0){
                    temp.transform.GetChild(2).gameObject.SetActive(true);
                    rightBlocked = 1;
                }
                
            }
            else if(leftBlocked==1){
                leftBlocked = 2;
            }
            else{
                leftBlocked = 0;
            }
        }
        if(Random.Range(0, 15)==0){
            if (Random.Range(0, 2) == 0) { temp.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.SetActive(true); }
            else
            {
                temp.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
           
        }
        current = temp;
    }
    public void ResetGame()
    {
        Vibration.Vibrate(2);
        SceneManager.LoadScene("Game");
    }

    public void GoHome(){
        Vibration.Vibrate(2);
        SceneManager.LoadScene("Loading");
    }

    public void DestroyAllGameOver(){
        GameObject[] obj = (GameObject[]) GameObject.FindObjectsOfType(typeof (GameObject));
        foreach(GameObject o in obj){
            if(o.GetComponent<Rigidbody>()!=null){
                o.GetComponent<Rigidbody>().isKinematic = false;
                if(o.tag=="Player"){
                    o.GetComponent<Rigidbody>().drag = -5;
                }
                else{
                    o.GetComponent<Rigidbody>().drag = Random.Range(0, 3);
                }
            }
        }
    }
    public void Pause(){
        if(Time.timeScale==1){
            Vibration.Vibrate(2);
            Time.timeScale = 0;
            pause.enabled = false;
            play.enabled = true;
            
        }
        else{
            Vibration.Vibrate(2);
            Time.timeScale = 1;
            pause.enabled = true;
            play.enabled = false;
        }
    }

    public void OnDeadManager(){
        pause.enabled = false;
        play.enabled = false;
    }
    
    public void OnDeadDestroy(){
        GameObject[] obj = (GameObject[]) GameObject.FindObjectsOfType(typeof (GameObject));
        foreach(GameObject o in obj){
            if(o.GetComponent<Rigidbody>()!=null){
                Destroy(o);
            }
        }
        
    }

    public void Menu(){
        restart.gameObject.SetActive(true);
        home.enabled = true;
        highScore.enabled = true;
        homeButton.gameObject.SetActive(true);
    }
}
