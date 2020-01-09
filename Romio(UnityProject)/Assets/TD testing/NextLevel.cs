using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TLTD")
        {
            SceneManager.LoadScene("TD testing 2");
        }
        if(collision.gameObject.tag == "NL2")
        {
            SceneManager.LoadScene("Level 2");
        }
        if(collision.gameObject.tag == "NL3")
        {
            SceneManager.LoadScene("Level 3");
        }
        if(collision.gameObject.tag == "SA")
        {
            SceneManager.LoadScene("Shoping area");
        }
    }
}
