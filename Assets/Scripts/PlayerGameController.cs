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
    public GameObject bigExplasionParticle, smallExplasionParticle, finishParticle,scoreObject,restartButton;
    public static PlayerGameController instance;
    public bool isGameWin = false, isGameLose = false;

    private void Awake() => instance = this;
    private void Start() => myRb = this.GetComponent<Rigidbody>();
    private void Update()
    {
        scoreText.text = score.ToString();
        if (score == -1)
        {
            losePanel.SetActive(true);
            PlayerMove.instance.speed = 0;
        }
    }
    public void LoseAction()
    {
        losePanel.SetActive(true);
        scoreObject.SetActive(true);
        PlayerMove.instance.speed = 0;
        isGameLose = true;
        restartButton.SetActive(true);
    }
    public void WinAction()
    {
        Debug.Log("Finish");
        foreach (var item in Cubes)
        {
            Destroy(item.gameObject);
        }
        Cubes.Clear();
        finishParticle.SetActive(true);
        winPanel.SetActive(true);
        scoreObject.SetActive(true);
        isGameWin = true;
        restartButton.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            score++;
            Cubes.Add(other.gameObject);
            other.gameObject.transform.SetParent(transform);
            other.gameObject.transform.position = spawnPoint.position + new Vector3(0, 1, 0) * Cubes.Count;

        }
        if (other.gameObject.CompareTag("LowObstacle") && Cubes.Count >= 0)
        {
            score -= 1;
            smallExplasionParticle.SetActive(true);
            if (score < 0)
            {
                LoseAction();
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
        if (other.gameObject.CompareTag("HiObstacle") && Cubes.Count >= 0)
        {
            score--;
            if (score > 0)
            {
                bigExplasionParticle.SetActive(true);
                foreach (var item in Cubes)
                {
                    Destroy(item.gameObject);
                }
                Cubes.Clear();
                score = 0;
            }
            else
            {
                LoseAction();
            }

        }

        if (other.gameObject.CompareTag("Finish") && score >= 0)
        {
            if (score > 0)
            {
                WinAction();
            }
            else
            {
                LoseAction();
            }

        }
    }
}

public static class PlayerExtensions
{
    public static GameObject GetLastMember(this List<GameObject> Cubes)
    {
        return Cubes[Cubes.Count - 1];
    }
}
