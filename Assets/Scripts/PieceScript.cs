using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public float speed;
    private float delay = 2.5f;
    private Vector3 left;
    private Vector3 right;
    void OnTriggerExit(Collider other)
    {
        if (other.tag=="Middle")
        {
            Manager.Instance.SpawnPieces();
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(delay);
        Manager.Instance.Pieces.Push(gameObject);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(4).gameObject.SetActive(false);
    }
}
