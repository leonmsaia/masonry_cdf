using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 initialVelocity;
    [SerializeField] private float velocityMultiplier;

    private Rigidbody2D ballRb;
    private bool isBallMoving;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBallMoving)
        {
            Launch();
        }
    }

    private void Launch()
    {
        transform.parent = null;
        ballRb.velocity = initialVelocity;
        isBallMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Choco");

        if (collision.gameObject.CompareTag("Block")) 
        {
            Destroy(collision.gameObject);
            ballRb.velocity *= velocityMultiplier;
            GameManager.Instance.BlockDestroyed();
        }
        VelocityFix();
    }

    private void VelocityFix()
    {
        float velocityDelta = 0.5f;
        float minVelocity = 0.2f;

        if (Mathf.Abs(ballRb.velocity.x) < minVelocity)
        {
            velocityDelta = Random.value < 0.5f ? velocityDelta : -velocityDelta;
            ballRb.velocity += new Vector2(0f, velocityDelta);
        }
        if (Mathf.Abs(ballRb.velocity.y) < minVelocity)
        {
            velocityDelta = Random.value < 0.5f ? velocityDelta : -velocityDelta;
            ballRb.velocity += new Vector2(velocityDelta, 0f);
        }
    }
}
