using UnityEngine;
using System;

public class ArrowHead : MonoBehaviour
{

    // Define audio objects
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    // Total bubbles popped with one arrow
    int BubbleCount = 0;

    // Text popup prefab
    public PointsPopup textPopupPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BubblePop(Vector2 location)
    {
        // Increase bubble counter
        BubbleCount++;

        // Increase score
        GameManager.totalScore += (BubbleCount * BubbleCount);
        

        // Play audio
        var random = new System.Random();
        AudioClip audioClip = audioClips[random.Next(0, audioClips.Length)];
        audioSource.PlayOneShot(audioClip); 

        // Create points text popup
        Canvas canvas = FindObjectOfType<Canvas>();
        PointsPopup popupText = Instantiate(textPopupPrefab, canvas.transform);
        popupText.Initialize(BubbleCount);

        // Position the text above the object
        RectTransform rectTransform = popupText.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector3 worldPosition = transform.position;
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            rectTransform.position = screenPosition; // Set screen position
        }
    }
}
