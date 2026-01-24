using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    [Header("Sprint Settings")]
    public float normalSpeed = 5f;
    public float sprintSpeed = 8f;
    public bool isSprinting = false;

    private string sprintBool = "IsRunning";
    private PlayerMovement3D playerMovement3D;
    
    void Start()
    {
        playerMovement3D = GetComponent<PlayerMovement3D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = playerMovement3D.rb.linearVelocity.magnitude > 0.1f;
        bool isHoolShift = Input.GetKey(KeyCode.LeftShift);
        if (isHoolShift && isMoving)
        {
            isSprinting = true;
            playerMovement3D.moveSpeed = sprintSpeed; // Tambah kecepatan lari
            if (playerMovement3D.anim != null)
            {
                playerMovement3D.anim.SetBool(sprintBool, true);
            }
        }
        else 
        {
            isSprinting = false;
            playerMovement3D.moveSpeed = normalSpeed;

            if (playerMovement3D.anim != null)
            {
                playerMovement3D.anim.SetBool(sprintBool, false);
            }
        }
    }
}
