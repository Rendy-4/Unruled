using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour, IDataPresistence
{
    public Rigidbody rb;
    public float moveSpeed = 5f;

    void Update()
    {
        
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector3(moveX * moveSpeed, rb.linearVelocity.y, 0f);
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