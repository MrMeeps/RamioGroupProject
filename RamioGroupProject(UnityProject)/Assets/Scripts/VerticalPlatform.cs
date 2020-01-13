using UnityEngine;
public class VerticalPlatform : MonoBehaviour
{
    #region VARIABLES
    PlatformEffector2D effector;
    public float waitTime;
    #endregion
    #region START FUNCTION
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        waitTime = 0.3f;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            waitTime = 0.3f;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.3f;
            }
            else
                waitTime -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
            effector.rotationalOffset = 0;
    }
    #endregion
}
