using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 startPosition;
    private Quaternion _targetRotation;
    private float turningRate;
    // Start is called before the first frame update
    void Start()
    {
        _targetRotation = transform.rotation;
        turningRate = 35f;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + startPosition;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);
    }
 

    // Call this when you want to turn the object smoothly.
    public void OnDead(Vector3 angles)
    {
        _targetRotation = Quaternion.Euler(angles);
    }
}
