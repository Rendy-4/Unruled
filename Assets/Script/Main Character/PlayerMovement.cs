using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour, IDataPresistence
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    private int facingDirection = 1;
    public Animator anim;


    void FixedUpdate()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        rb.linearVelocity = new Vector3(horizontal * moveSpeed, rb.linearVelocity.y, 0f);

    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerPosistion;
    }
    public void SaveData(ref GameData data)
    {
        data.playerPosistion = transform.position;
    }
}