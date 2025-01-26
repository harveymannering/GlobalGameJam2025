using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour
{
    // Speed at which the bubble floats upwards
    public float floatSpeed = 2f;

    // Access the objects rigidbody
    private Rigidbody2D rb;
    // Access this objects animator
    private Animator animator;

    void Start()
    {
        var random = new System.Random();
        floatSpeed += (float) (random.NextDouble() - 0.5f);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(SelfDestruct());
    }

    void Update()
        {
            // Destroy object if pop animation has finished playing
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && 
                !animator.GetCurrentAnimatorStateInfo(0).loop)
            {
                Destroy(gameObject);
            }
        }

    void FixedUpdate()
    {
        // Bubble floats up
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, floatSpeed);
        //rb.linearVelocity += new Vector2(rb.linearVelocity.y, Mathf.PingPong(Time.time * floatSpeed, floatSpeed));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has an Arrow script
        //if (collision.gameObject.GetComponent<Arrow>() != null)
        if (collision.gameObject.tag == "ArrowHead")
        {
            // Play pop animation
            animator.SetBool("Pop", true);
            ArrowHead arrowHead = collision.gameObject.GetComponent<ArrowHead>();
            Vector2 collisionPosition = collision.transform.position;
            arrowHead.BubblePop(collisionPosition);
        }
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
