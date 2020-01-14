using UnityEngine;
[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable")]
public class Consumable : ScriptableObject
{
    #region VARIABLES
    public enum Consumables { Apple, Bread, Cake, HealthPotion, PowerPotion }
    public Consumables consumableType;
    public string consumableName;
    public int healPoints;
    public int powerPoints;
    #endregion
}