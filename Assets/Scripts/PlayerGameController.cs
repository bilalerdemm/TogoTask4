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
            score++;
            Cubes.Add(other.gameObject);
            other.gameObject.transform.SetParent(transform);
            other.gameObject.transform.position = spawnPoint.position + new Vector3(0, 1, 0) * Cubes.Count;

        }
        if (other.gameObject.CompareTag("LowObstacle") && Cubes.Count > 0)
        {
            score--;
            Debug.Log(Cubes.Count);
            Debug.Log("LowObstacle");
            Destroy(Cubes[Cubes.Count - 1].gameObject);
            Cubes.RemoveAt(Cubes.Count - 1);
        }
        else
        {
            Debug.Log("Fail");
        }

        if (other.gameObject.CompareTag("HiObstacle") && Cubes.Count > 0)
        {
            score = 0;
            Debug.Log("HiObstacle");
            //Destroy(Cubes[Cubes.Count].gameObject);
            foreach (var item in Cubes)
            {
                Destroy(item.gameObject);
            }
            Cubes.Clear();
        }
        else
        {
            Debug.Log("Fail");
        }

    }
}
