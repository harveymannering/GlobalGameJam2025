using UnityEngine;
using System;

public class Arrow : MonoBehaviour
{
    // Access the objects rigidbody
    private Rigidbody2D rb;
    void Start()
    {

    }

    public void Initialize(float x_vel, float y_vel)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(x_vel,y_vel);
    }

    // Update is called once per frame
    void Update()
    {
        // Get object velocity
        Vector2 vel = rb.linearVelocity;
        // Get the angle of the direction of the velocity
        float rot_z = Mathf.Atan2(vel.y, vel.x);
        rot_z = rot_z * (180f / Mathf.PI);
        // Set the rotation of the object
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rot_z);

        // Destroy object if it is too far from the center
        float distance = 100f;
        float x = transform.position.x;
        float y = transform.position.y;
        if (distance < Math.Sqrt(x * x + y * y))
        {
            Destroy(gameObject);
        }
    }
}
