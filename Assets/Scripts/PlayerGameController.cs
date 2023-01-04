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
    public static PlayerGameController instance;
    public bool isGameWin = false;

    private void Awake() => instance = this;
    private void Start() => myRb = this.GetComponent<Rigidbody>();

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
            smallExplasionParticle.SetActive(true);
            if (score < 0)
            {
                losePanel.SetActive(true);
            }
            Debug.Log(Cubes.Count);
            Debug.Log("LowObstacle");
            if (Cubes.Count > 0)
            {
                //Destroy(Cubes[Cubes.Count - 1].gameObject);
                Destroy(Cubes.GetLastMember());
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

        if (other.gameObject.CompareTag("Finish") && score > 0)
        {
            Debug.Log("Finish");
            foreach (var item in Cubes)
            {
                Destroy(item.gameObject);
            }
            Cubes.Clear();
            finishParticle.SetActive(true);
            winPanel.SetActive(true);
            isGameWin = true;
        }
    }
    IEnumerator Jump()
    {
        myRb.velocity += new Vector3(0, 8, 0);
        yield return null;
    }
}

public static class PlayerExtensions
{
    public static GameObject GetLastMember(this List<GameObject> Cubes )
    {
    return Cubes[Cubes.Count - 1];
    }
}
