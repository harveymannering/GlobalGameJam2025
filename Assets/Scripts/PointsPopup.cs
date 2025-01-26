using UnityEngine;
using TMPro;
using System.Collections;

public class PointsPopup : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    public void Initialize(int number)
    {
        // Change text 
        TMP_Text timerTextBox = GetComponent<TMP_Text>();
        timerTextBox.text = (number * number).ToString();

        // Set different colours for different numbers
        if (number == 1)
            timerTextBox.color = new Color(0.6254163f, 0.4227405f, 0.2132468f, 1.0f); 
        else if (number == 2)
            timerTextBox.color = new Color(0.6745911f, 0.6745911f, 0.6745911f, 1.0f); 
        else if (number == 3)
            timerTextBox.color = new Color(0.9611523f, 0.7365999f, 0.2064156f, 1.0f); 
        else if (number == 4)
            timerTextBox.color = new Color(0.8268796f, 0.914066f, 0.914066f, 1.0f); 
        else if (number >= 5)
            timerTextBox.color = new Color(0.8836394f, 0.6015822f, 0.9118451f, 1.0f); 
    }


    // Destroy object after 3 seconds 
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
