using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [Header("Movement Settings")]
    public float moveSpeed = 10;
    public float jumpSpeed = 1;
    bool grounded = false;
    #endregion
    //UNTIY FUNCTIONS
    #region UPDATE FUNCTION
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100 * jumpSpeed));
        float moveX = Input.GetAxis("Horizontal");
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveSpeed * moveX;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
    #endregion
}
