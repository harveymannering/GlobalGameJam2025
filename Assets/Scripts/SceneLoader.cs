using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Define bubble prefabs
    public GameObject bigBubblePrefab;
    public GameObject smallBubblePrefab;

    // Define randomizer objects
    System.Random random = new System.Random();
    float targetTime = 0;

    void Start()
    {
        
    }

    double GenerateRandomNumberInRanges(System.Random random)
    {
        // Randomly choose between the two ranges
        bool chooseLowerRange = random.Next(2) == 0;

        if (chooseLowerRange)
        {
            // Generate random number between -8 and -4.5
            return random.NextDouble() * (-4.5 - (-8)) + (-8);
        }
        else
        {
            // Generate random number between 4.5 and 8
            return random.NextDouble() * (8 - 4.5) + 4.5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update timer
        targetTime -= Time.deltaTime;
        if (targetTime <= 0)
        {
            // Randomize the starting position
            random = new System.Random();
            double x_loc = GenerateRandomNumberInRanges(random);
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

            // set time until next bubble
            targetTime = (float) (random.NextDouble() * 2f);
        } 
    }
    
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
        GameManager.totalScore = 0;
    }
}
