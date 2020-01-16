using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerCollision : MonoBehaviour
{
    #region VARIABLES
    [Header("Hitpoint Settings")]
    public int maxHealth = 5;
    public int currentHealth;
    public int lives;
    public Slider healthSlider;
    public Text livesText;
    [Header("Coin Settings")]
    public int coins;
    public Text coinText;
    [Header("Level Settings")]
    public Animator animator;
    public Animator playerAC;
    public bool nextLevel;
    public int level;
    public bool loadTown;
    [Header("Camera Settings")]
    [HideInInspector]public bool camZoom;
    [Header("Testing Only")]
    public bool LifeTesting;
    [HideInInspector] public bool InShop;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        currentHealth = maxHealth;
        lives = PlayerPrefs.GetInt("lives");
        level = PlayerPrefs.GetInt("level");
        coins = PlayerPrefs.GetInt("coins");
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        livesText.text = "Lives: " + lives;
        coinText.text = "" + coins;
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
            playerAC.SetFloat("x", velocity.x);
            playerAC.SetFloat("y", velocity.y);
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
        //Enemy Collision
        if (collision.gameObject.CompareTag("Enemy Arrow"))
            TakeDamage(collision.gameObject.GetComponent<ArrowScript>().attackDamage);
        //Shop Collision
        if (collision.gameObject.CompareTag("Shop"))
            InShop = true;
        //Level Load
        if (collision.gameObject.CompareTag("Town"))
        {
            loadTown = true;
            animator.SetTrigger("FadeOut");
        }
        else if (collision.gameObject.CompareTag("NextLevel"))
            StartCoroutine(LoadLevel(collision));
        //Camera Zoom
        if (collision.gameObject.CompareTag("CameraZoom"))
            camZoom = true;
        //Death
        if (collision.gameObject.CompareTag("Death"))
            StartCoroutine(CameraDeath());
        //Coin Collecting
        if(collision.gameObject.CompareTag("Coin"))
        {
            coins++;
            coinText.text = "" + coins;
            PlayerPrefs.SetInt("coins", coins);
            Destroy(collision.gameObject);
        }
    }
    #endregion
    #region ON TRIGGER EXIT 2D FUNCTION
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
            InShop = false;
    }
    #endregion
    //PLAYER HEALTH FUNCTIONS
    #region TAKE DAMAGE FUNCTION
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
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
    #region CAMERA DEATH FUNCTION
    IEnumerator CameraDeath()
    {
        GetComponentInChildren<Camera>().transform.parent = null;
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("lives", lives - 1);
        lives = PlayerPrefs.GetInt("lives");
        if (lives < 0)
            SceneManager.LoadScene("Game Over");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}
