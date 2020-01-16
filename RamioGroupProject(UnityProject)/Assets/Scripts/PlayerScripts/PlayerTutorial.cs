using UnityEngine;
using UnityEngine.UI;
public class PlayerTutorial : MonoBehaviour
{
    #region VARIABLES
    [Header("Text Settings")]
    public Text[] tips = new Text[4];
    int arrayNum = 5;
    float timer;
    bool timeStart;
    #endregion
    //UNITY FUNCTION
    #region START FUNCTION
    void Start()
    {
        for(int i = 0; int < 4; i++)
            tips[i].SetActive(false)
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        switch(arrayNum)
        {
            Case 0:
                tips[arrayNum].SetActive(true);
                if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    timer += Time.deltaTime;
                    if(timer > 1f)
                    {   
                        arrayNum = 5;
                        timer = 0;
                    }
                }
                break;
            Case 1:
                tips[arrayNum].SetActive(true);
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    timer += Time.deltaTime;
                    if(timer > 1f)
                    {
                        arrayNum = 5;
                        timer = 0;
                    }
                }
                break;
            Case 2:
                tips[arrayNum].SetActive(true);
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    timer += Time.deltaTime
                    if(timer > 1f)
                    {
                        arrayNum = 5;
                        timer = 0;
                    }
                }
                break;
            Case 3:
                tips[arrayNum].SetActive(true);
                if(Input.GetKeyDown(KeyCode.Keypad2))
                {
                    timer += Time.deltaTime
                    if(timer > 1f)
                    {
                        arrayNum = 5;
                        timer = 0;
                    }
                }
                break;
            Case 4:
                tips[arrayNum].SetActive(true);
                if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    timer += Time.deltaTime
                    if(timer > 1f)
                    {
                        arrayNum = 5;
                        timer = 0;
                    }
                }
                break;
            Case 5:
                tips[0,1,2,3,4].SetActive(false):
                break;            
            Case Default:
                tips[0,1,2,3,4].SetActive(false):
                break;
        }
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D()
    {
        switch(colision.gameObject.name)
        {
            Case "MovingTipTrigger":
                arrayNum = 0;
                break;
            Case "JumpingTipTrigger":
                arrayNum = 1;
                break;
            Case "AttackTipTrigger":
                arrayNum = 2;
                break;
            Case "SwitchingWeaponsTipTrigger":
                arrayNum = 3;
                break;
            Case "SlowMoTipTrigger":
                arrayNum = 4;
                break;
        }
    }
    #endregion
}
