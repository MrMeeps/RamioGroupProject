using UnityEngine;
[CreateAssetMenu(fileName = "New Armor", menuName = "Armor")]
public class Armor : ScriptableObject
{
    #region VARIABLES
    public enum ArmorOptions { Leather, Iron, Gold }
    public Armor armorType;
    public string armorName;
    public int defensePoints;
    #endregion
}