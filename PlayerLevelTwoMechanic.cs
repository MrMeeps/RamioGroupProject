using System.Collections;
using UnityEngine;
public class PlayerLevelTwoMechanic
{
  #region VARIABLES
  public int coins;
  public float lightOuterRadius;
  public float maxTime;
  public float currentTime;
  public float speed;
  #endregion
  //UNITY FUNCTIONS
  #region UPDATE FUNCTION
  void Update()
  {
      coins = GetComponent<PlayerCollision>().coins;
      lightOuterRadius = MathF.Lerp(GetComponentInChildren<Light2D>().outerRadius,
  }
  #endregion
}
