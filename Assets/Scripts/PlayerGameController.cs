using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameController : MonoBehaviour
{
    public List<GameObject> Cubes = new List<GameObject>();
    public Transform spawnPoint;
    public int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            //Debug.Log("Girdi");
            Cubes.Add(other.gameObject);
            other.gameObject.transform.SetParent(transform);
            other.gameObject.transform.position = spawnPoint.position + new Vector3(0, 1, 0) * Cubes.Count;

        }
    }
}
