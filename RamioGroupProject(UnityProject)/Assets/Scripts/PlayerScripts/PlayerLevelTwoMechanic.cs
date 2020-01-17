using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class PlayerLevelTwoMechanic : MonoBehaviour
{
    #region VARIABLES
    public float lightOuterRadius;
    public float speed;
    int prevCoins;
    bool killLoop = true;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start() { lightOuterRadius = GetComponentInChildren<Light2D>().pointLightDistance; }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        GetComponentInChildren<Light2D>().pointLightOuterRadius = lightOuterRadius;
        lightOuterRadius -= Time.deltaTime * speed;
        if (GetComponentInChildren<Light2D>().pointLightOuterRadius <= 0 && killLoop == true)
            StartCoroutine(Death());
        if (GetComponentInChildren<Light2D>().pointLightOuterRadius <= 0)
            GetComponentInChildren<Light2D>().pointLightOuterRadius = 0;
    }
    #endregion
    #region LIGHT INCREASE FUNCTION
    public void LightIncrease()
    {
        print("works");
        lightOuterRadius = lightOuterRadius + .1f;
        GetComponentInChildren<Light2D>().pointLightOuterRadius = lightOuterRadius;
    }
    #endregion
    #region DEATH FUNCTION
    IEnumerator Death()
    {
        killLoop = false;
        yield return new WaitForSeconds(2f);
        GetComponent<PlayerCollision>().TakeDamage(1);
        killLoop = true;
    }
    #endregion
}
