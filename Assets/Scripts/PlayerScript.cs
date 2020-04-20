using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private int speed;
    private Vector3 dir;

    public int score;

    public Material material;

    public Text tap;
    private float prizeCount;
    private float floorCount;

    private bool started;

    private Vector3 touchPosition;
    private Rigidbody rb;

    private float distance = 0;
    void Start()
    {
        speed = 0;
        started = false;
        floorCount = 0;
        prizeCount = 0;
        score = 0;
        dir = Vector3.forward;
        
    }

    void Update()
    {
        if(!started){
            if(Input.GetMouseButton(0)){
                Vibration.Vibrate(2);
                speed = 65;
                started = true;
                tap.enabled = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            transform.GetChild(0).gameObject.transform.position = Vector3.MoveTowards(transform.GetChild(0).gameObject.transform.position , new Vector3(transform.GetChild(0).gameObject.transform.position.x * -1, 2, transform.position.z), 4);
        }

        float amountToMove = speed * Time.deltaTime;
        transform.Translate(dir * amountToMove);

        if (score> PlayerPrefs.GetInt("HighScore", 0)){
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag == "Obstacle"){
            other.GetComponent<MeshRenderer>().material = material;
            Vibration.Vibrate(5);
            speed = 0;
            transform.GetChild(1).transform.parent = null;
            Manager.Instance.DestroyAllGameOver();
            StartCoroutine(WaitOnDead());
        }
        else if(other.tag == "LeftPrize"){
            other.gameObject.SetActive(false);
            if(prizeCount==5){
                score++;
                prizeCount = 0;
            }
            else{
                prizeCount++;
            }
            Vibration.Vibrate(2);
        }
        else if(other.tag == "RightPrize"){
            other.gameObject.SetActive(false);
            if(prizeCount==5){
                score++;
                prizeCount = 0;
            }
            else{
                prizeCount++;
            }
            Vibration.Vibrate(2);
        }
        if(other.tag=="Floor"){
            if(floorCount==50){
                score++;
                floorCount = 0;
            }
            else{
                floorCount++;
            }
            
        }
        GameObject.Find("/Canvas/Score").gameObject.GetComponent<Text>().text = "Score: " + score;
        GameObject.Find("/Canvas/HighScore").gameObject.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }
   IEnumerator WaitOnDead(){
       yield return new WaitForSeconds(2);
       GameObject.Find("Camera").gameObject.GetComponent<CameraScript>().OnDead(new Vector3(0, 45, 0));
       Manager.Instance.OnDeadManager();
       yield return new WaitForSeconds(1);
       Manager.Instance.Menu();
       Manager.Instance.OnDeadDestroy();
   }
}
