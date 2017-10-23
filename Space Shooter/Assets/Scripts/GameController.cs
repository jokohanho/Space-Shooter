using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameObject hazard1;
    public GameObject hazard2;
    public GameObject hazard3;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        score = 0;
        UpdateScore();
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        StartCoroutine (SpawnWaves());
    }

    void Update()
    {
        if(restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                Application.LoadLevel (Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        { 
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                System.Random rnd = new System.Random();
                int spawnType = rnd.Next(1, 4);
                if (spawnType == 1)
                {
                    Instantiate(hazard1, spawnPosition, spawnRotation);
                }
                else if (spawnType == 2)
                {
                    Instantiate(hazard2, spawnPosition, spawnRotation);
                }
                else if (spawnType == 3)
                {
                    Instantiate(hazard3, spawnPosition, spawnRotation);
                }

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
        
    }
	
    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;

    }
    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
