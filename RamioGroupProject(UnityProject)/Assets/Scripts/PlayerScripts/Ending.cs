#region NAMESPACES
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion
public class Ending : MonoBehaviour
{
    #region VARIABLES
    [Header("Ending Settings")]
    public float endingLength;
    public float moveSpeed = 3;
    public Animator animator;
    bool end = true;
    #endregion
    //UNITY FUNCTION
    #region UPDATE FUNCTION
    void Update()
    {
        if(end == true)
        {
            StartCoroutine(ENDING());
            end = false;
        }
        float moveX = Input.GetAxis("Horizontal");
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveSpeed * moveX;
        if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            GetComponent<Rigidbody2D>().velocity = velocity;
        animator.SetFloat("x", velocity.x);
        animator.SetFloat("y", velocity.y);
    }
    #endregion
    //ENDING FUNCTION
    #region ENDING FUNCTION
    IEnumerator ENDING()
    {
        yield return new WaitForSeconds(endingLength);
        SceneManager.LoadScene("Main Menu");
    }
    #endregion
}
