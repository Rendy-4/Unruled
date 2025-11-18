using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector3(moveX * moveSpeed, rb.linearVelocity.y, 0);
    }
}