﻿#region NAMESPACES
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#endregion
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
    [Header("Camera Settings")]
    [HideInInspector]public bool camZoom;
    [Header("Level Two Mechanic")]
    public GameObject darkEnemy;
    public bool turnAway;
    [Header("Testing Only")]
    public bool LifeTesting;
    [HideInInspector] public bool coinChange;
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
        livesText.text = "x" + lives;
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
        switch (collision.gameObject.tag)
        {
            case "Enemy Arrow":
                TakeDamage(collision.gameObject.GetComponent<ArrowScript>().attackDamage);
                break;
            case "NextLevel":
                StartCoroutine(LoadLevel(collision));
                break;
            case "CameraZoom":
                camZoom = true;
                GetComponentInChildren<CameraZoom>().slowMoZoom = 2;
                break;
            case "Death":
                StartCoroutine(CameraDeath());
                break;
            case "Coin":
                if (SceneManager.GetActiveScene().name == "Level 2")
                    GetComponent<PlayerLight>().LightIncrease();
                coins++;
                coinText.text = "" + coins;
                PlayerPrefs.SetInt("coins", coins);
                Destroy(collision.gameObject);
                break;
            case "SpawnEnemy":
                StartCoroutine(SpawnEnemy(collision));
                break;
            case "TurnAway":
                turnAway = true;
                break;
            case "Win":
                SceneManager.LoadScene("Win");
                break;
            case "Untagged":
                break;
            default:
                Debug.LogWarning("Tag may not exist in current context");
                break;
        }
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
    #region LOAD LEVEL FUNCTION
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
    #region SPAWN ENEMY FUNCTION
    IEnumerator SpawnEnemy(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        Transform local = GetComponent<Transform>();
        yield return new WaitForSeconds(0.1f);
        GameObject enemy = Instantiate(darkEnemy, local);
        enemy.GetComponent<Transform>().position = new Vector3(local.position.x - 2, local.position.y, -1);
    }
    #endregion
}
