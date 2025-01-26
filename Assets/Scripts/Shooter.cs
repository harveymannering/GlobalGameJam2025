using UnityEngine;
using System;
using TMPro;

public class Shooter : MonoBehaviour
{

    // Define objects for drawing line
    private LineRenderer lineRenderer;
    private Camera mainCamera;
    private bool isDrawing = false;
    private Vector3 startPoint;
    private Vector3 endPoint;

    // Define projectile variables
    public GameObject arrowPrefab;

    // Text box objects
    public TMP_Text statusTextBox;

    // Cool down time for shooter
    float totalCoolDownTime = 0.5f;
    float coolDownTime = 0f;

    void Start()
    {
        // Initialize main line drawing objects
        lineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {

        // Update cool down on shooter
        coolDownTime -= Time.deltaTime;

        // Start drawing aiming line
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            startPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 0;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, startPoint);
        }

        // Continue drawing line as cursor moves
        if (isDrawing)
        {
            endPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0;
            lineRenderer.SetPosition(1, endPoint);
        }

        // Stop drawing aiming line
        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);

            if (coolDownTime <= 0)
            {
                Debug.Log(coolDownTime);

                // Spawn arrow
                GameObject arrow = Instantiate(arrowPrefab, startPoint, Quaternion.identity);
                
                // Set inital velocity of the arrow
                float x_vel = endPoint.x - startPoint.x;
                float y_vel = endPoint.y - startPoint.y;
                float vel_length = (float)Math.Sqrt(x_vel*x_vel+y_vel*y_vel);
                if (vel_length > (float)Math.Sqrt(10))
                {
                    x_vel = (x_vel / vel_length) * (float)Math.Sqrt(10);
                    y_vel = (y_vel / vel_length) * (float)Math.Sqrt(10);
                }
                arrow.GetComponent<Arrow>().Initialize(x_vel*10f,y_vel*10f);

                coolDownTime = totalCoolDownTime;
            }
        }

        // Update reloading text
        if (coolDownTime > (2*totalCoolDownTime) / 3)
            statusTextBox.text = "Reloading.";
        else if (coolDownTime > totalCoolDownTime / 3)
            statusTextBox.text = "Reloading..";
        else if (coolDownTime > 0)
            statusTextBox.text = "Reloading...";
        else
            statusTextBox.text = "Ready!";
    }
}
