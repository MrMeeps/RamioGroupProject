using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class DarkEnemyAI : MonoBehaviour
{
    #region VARIABLES
    [Header("Chase Settings")]
    public Transform player;
    public float speed = .1f;
    public float moveX;
    public bool facingLeft = true;
    [Header("Lighting Settings")]
    public float lightOuterRadius;
    bool lightStart = true;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        transform.parent = null;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        float moveXLocal = GetComponent<Transform>().position.x;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        float moveXAfter = GetComponent<Transform>().position.x;
        moveX = moveXLocal - moveXAfter;
        if (moveX > 0 && !facingLeft)
            Flip();
        else if (moveX < 0 && facingLeft)
            Flip();
        if(lightStart == true)
        {
            GetComponentInChildren<Light2D>().pointLightOuterRadius = lightOuterRadius;
            lightOuterRadius += .01f;
            if (GetComponentInChildren<Light2D>().pointLightOuterRadius >= 1)
                lightStart = false;
        }
    }
    #endregion
    //DARK ENEMY AI FUNCTIONS
    #region FLIP FUNCTION
    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    #endregion
}
