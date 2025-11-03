using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private bool isGrounded = false;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // Gerak kiri-kanan di 2D tapi fisika 3D
        rb.linearVelocity = new Vector3(moveX * moveSpeed, rb.linearVelocity.y, 0);

        // Lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}