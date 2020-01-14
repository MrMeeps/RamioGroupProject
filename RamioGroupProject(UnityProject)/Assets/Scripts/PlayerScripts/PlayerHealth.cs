using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    #region VARIABLES
    [Header("Hitpoint Settings")]
    public int maxHealth = 5;
    public int currentHealth;
    public int lives;
    [Header("Level Settings")]
    public Animator animator;
    public bool nextLevel;
    public int level;
    public bool loadTown;
    [Header("Testing Only")]
    public bool LifeTesting;

    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        currentHealth = maxHealth;
        lives = PlayerPrefs.GetInt("lives");
        level = PlayerPrefs.GetInt("level");
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        //Lives testing
        if (LifeTesting == true && Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerPrefs.SetInt("lives", 3);
            lives = PlayerPrefs.GetInt("lives");
        }
        //Movement transition to next level
        if (nextLevel == true)
        {
            GetComponent<PlayerMovement>().movementOn = false;
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            velocity.x = GetComponent<PlayerMovement>().moveSpeed * 1;
            GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
    #endregion
    #region ON COLLISION ENTER 2D FUNCTION
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            TakeDamage(1);
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Arrow"))
            TakeDamage(collision.gameObject.GetComponent<ArrowScript>().attackDamage);
        if (collision.gameObject.CompareTag("Town"))
        {
            loadTown = true;
            animator.SetTrigger("FadeOut");
        }
        else if (collision.gameObject.CompareTag("NextLevel"))
            StartCoroutine(LoadLevel(collision));
    }
    #endregion
    //PLAYER HEALTH FUNCTIONS
    #region TAKE DAMAGE FUNCTION
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 1)
        {
            PlayerPrefs.SetInt("lives", lives - 1);
            lives = PlayerPrefs.GetInt("lives");
            if (lives < 0)
                SceneManager.LoadScene("Game Over");
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    #endregion
    #region LOAD LEVEL
    IEnumerator LoadLevel(Collider2D trigger)
    {
        GameObject disableTrigger = trigger.gameObject;
        disableTrigger.SetActive(false);
        GetComponentInChildren<Camera>().transform.parent = null;
        nextLevel = true;
        level++;
        PlayerPrefs.SetInt("level", level);
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("FadeOut");
    }
    #endregion
}
