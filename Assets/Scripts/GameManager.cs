using UnityEngine;
using TMPro;

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    // Track total time
    private float targetTime = 60.0f;
    private List<int> spawnTimes;

    // Track total score
    public static int totalScore;

    // Text box objects
    public TMP_Text timerTextBox;
    public TMP_Text scoreTextBox;
    public TMP_Text finalScoreTextBox;


    // Define bubble prefabs
    public GameObject bigBubblePrefab;
    public GameObject smallBubblePrefab;

    void Start()
    {
        // Generate random numbers and sort them
        var random = new System.Random();
        spawnTimes = Enumerable.Range(0, 100).Select(_ => random.Next(6, 61)).OrderBy(n => n).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        // Update timer
        if (targetTime <= 0.0f)
        {
            // Game ends
            finalScoreTextBox.text = "Final Score: " + totalScore.ToString();
            finalScoreTextBox.gameObject.SetActive(true);
        }
        else
        {
            targetTime -= Time.deltaTime;
            timerTextBox.text = ((int)targetTime).ToString();
        }

        // Update score counter
        scoreTextBox.text = "Score: " + ((int)totalScore).ToString();

        // Spawn bubbles
        if (spawnTimes.Any() && spawnTimes[^1] > targetTime)
        {

            // Randomize the starting position
            var random = new System.Random();
            double x_loc = (random.NextDouble() * 16.0) - 8.0;
            Vector3 startPoint = new Vector3((float)x_loc,-7.5f, 0f);

            // Randomize whether we are generating a big or small bubble
            if (random.Next(0, 2) == 1)
            {
                Instantiate(bigBubblePrefab, startPoint, Quaternion.identity);
            }
            else
            {
                Instantiate(smallBubblePrefab, startPoint, Quaternion.identity);
            }

            // Remove bubble from spawn list
            spawnTimes.RemoveAt(spawnTimes.Count - 1); // Remove the last element
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        GameManager.totalScore = 0;
    }
}
