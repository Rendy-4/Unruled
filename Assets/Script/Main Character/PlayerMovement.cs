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
    public MovementMode currentMode = MovementMode.HorizontalX;

    void FixedUpdate()
    {
        if (isFrozen)
        {
            rb.linearVelocity = Vector3.zero;
            anim.SetFloat("horizontal", 0f);
            return;
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        
        // Logika Flip visual
        if (horizontal > 0 && visualsTransform.localScale.x < 0 || horizontal < 0 && visualsTransform.localScale.x > 0)
        {
            Flip();
        }
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));

        if (currentMode == MovementMode.HorizontalX)
        {
            // Mode X (Normal)
            rb.linearVelocity = new Vector3(horizontal * moveSpeed, rb.linearVelocity.y, 0f);
        }
        else if (currentMode == MovementMode.HorizontalZ)
        {
            
            // Mathf.Sign akan mengembalikan 1 jika positif, dan -1 jika negatif.
            float directionMultiplier = Mathf.Sign(transform.right.z);
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, horizontal * moveSpeed * directionMultiplier);
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