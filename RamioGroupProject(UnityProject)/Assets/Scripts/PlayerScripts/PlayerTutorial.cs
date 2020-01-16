using UnityEngine;
using UnityEngine.UI;
public class PlayerTutorial : MonoBehaviour
{
    #region VARIABLES
    [Header("Text Settings")]
    public Text[] tips = new Text[4];
    int arrayNum = 5;
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
                break;
            Case 1:
                tips[arrayNum].SetActive(true);
                break;
            Case 2:
                tips[arrayNum].SetActive(true);
                break;
            Case 3:
                tips[arrayNum].SetActive(true);
                break;
            Case 4:
                tips[arrayNum].SetActive(true);
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
        switch
        if(collision.gameObject.name == "MovingTipTrigger");
            arrayNum = 0;
        else if(collision.gameObject.name == "JumpingTipTrigger");
            arrayNum = 1;
        else if(collision.gameObject.name == "AttackTipTrigger");
            arrayNum = 2;
        else if(collision.gameObject.name == "SwitchingWeaponsTipTrigger");
            arrayNum = 3;
        else if(collision.gameObject.name == "SlowMoTipTrigger");
            arrayNum = 4;
    }
    #endregion
}
