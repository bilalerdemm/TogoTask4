using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGameController : MonoBehaviour
{
    public List<GameObject> Cubes = new List<GameObject>();
    public Transform spawnPoint;
    public int score = 0;
    public GameObject losePanel, winPanel;
    public Text scoreText;
    public Rigidbody myRb;
    public GameObject bigExplasionParticle, smallExplasionParticle, finishParticle;

    private void Start()
    {
        myRb = this.GetComponent<Rigidbody>();
    }
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
            score = score - 1;
            smallExplasionParticle.SetActive(true);
            if (score < 0)
            {
                losePanel.SetActive(true);
            }
            Debug.Log(Cubes.Count);
            Debug.Log("LowObstacle");
            if (Cubes.Count > 0)
            {
                Destroy(Cubes[Cubes.Count - 1].gameObject);
                Cubes.RemoveAt(Cubes.Count - 1);
            }
        }
        else if (Cubes.Count <= 0)
        {
            Debug.Log("Fail");
            losePanel.SetActive(true);
        }
        if (other.gameObject.CompareTag("HiObstacle") && Cubes.Count > 0)
        {
            score = 0;
            bigExplasionParticle.SetActive(true);
            //Destroy(Cubes[Cubes.Count].gameObject);
            foreach (var item in Cubes)
            {
                Destroy(item.gameObject);
            }
            Cubes.Clear();
        }
        if (other.gameObject.CompareTag("JumpArea"))
        {
            StartCoroutine(Jump());
        }
        /*
        else
        {
            Debug.Log("Fail");
            //losePanel.SetActive(true);
        }
        */
        if (other.gameObject.CompareTag("Finish") && score > 0)
        {
            Debug.Log("Finish");
            finishParticle.SetActive(true);
            winPanel.SetActive(true);
        }
    }
    IEnumerator Jump()
    {
        myRb.velocity += new Vector3(0, 8, 0);
        yield return null;
    }
}
