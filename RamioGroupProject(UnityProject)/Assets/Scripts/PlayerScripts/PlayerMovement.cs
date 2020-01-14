using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [Header("Movement Settings")]
    public bool movementOn = true;
    public float moveSpeed = 10;
    public float jumpSpeed = 1;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.5f;
    [HideInInspector] public bool facingRight = true;
    bool grounded = false;
    [Header("Testing Only")]
    public bool cameraFollowTest;
    new Transform camera;
    #endregion
    //UNTIY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        //Set camera variables
        if (cameraFollowTest == true)
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            camera.transform.parent = null;
        }
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        //Jumping
        if (Input.GetButtonDown("Jump") && grounded == true)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100 * jumpSpeed));
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (GetComponent<Rigidbody2D>().velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //Movement
        float moveX = Input.GetAxis("Horizontal");
        if (movementOn == true)
        {
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            velocity.x = moveSpeed * moveX;
            GetComponent<Rigidbody2D>().velocity = velocity;
        }
        //Flipping character
        if (moveX > 0 && !facingRight)
            Flip();
        else if (moveX < 0 && facingRight)
            Flip();
        //Camera movement
        if (cameraFollowTest == true)
            camera.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0)
            grounded = true;
    }
    #endregion
    #region ON TRIGGER STAY 2D FUNCTION
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0)
            grounded = true;
    }
    #endregion
    #region ON TRIGGER EXIT 2D FUNCTION
    void OnTriggerExit2D(Collider2D collision) { grounded &= collision.gameObject.layer != 0; }
    #endregion
    //MOVEMENT FUNCTIONS
    #region FLIP FUNCTION
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    #endregion
}