using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour, IDataPresistence
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    private int facingDirection = 1;
    public Animator anim;
    public Transform visualsTransform;
    private bool isFrozen;

    public enum MovementMode {HorizontalX, HorizontalZ}
    public MovementMode movementMode = MovementMode.HorizontalX;



    void FixedUpdate()
    {

        if (isFrozen)
        {
            rb.linearVelocity = Vector3.zero;
            anim.SetFloat("horizontal", 0f);
            return;
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));

        if (movementMode == MovementMode.HorizontalX)
        {
            rb.linearVelocity = new Vector3(horizontal, 0, 0 ) * moveSpeed;
        }
        else if (movementMode == MovementMode.HorizontalZ)
        {
            rb.linearVelocity = new Vector3(0, 0, horizontal) * moveSpeed;
        }

    }

    void Flip()
    {
        facingDirection *= -1;
        visualsTransform.localScale = new Vector3(visualsTransform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void Freeze(bool freeze)
    {
    isFrozen = freeze;

    if (freeze)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
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