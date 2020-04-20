using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float speed;


    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        float amountToMove = speed * Time.deltaTime;
        transform.Translate(dir * amountToMove);
    }
}
