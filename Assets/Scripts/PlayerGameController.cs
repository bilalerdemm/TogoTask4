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
        if (other.gameObject.CompareTag("LowObstacle"))
        {
            Debug.Log("LowObstacle");
            Destroy(Cubes[Cubes.Count - 1].gameObject);
            Cubes.RemoveAt(Cubes.Count - 1);
        }
        if (other.gameObject.CompareTag("HiObstacle"))
        {
            Debug.Log("HiObstacle");
            //Destroy(Cubes[Cubes.Count].gameObject);
            for (int i = 0; i < Cubes.Count; i++)
            {
                Destroy(Cubes[Cubes.Count - 1].gameObject);
            }
            Cubes.Clear();
        }

    }
}
