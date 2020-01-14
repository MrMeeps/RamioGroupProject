using UnityEngine;
public enum MenuType { Food, Potion, Weapon, Armor }
public class ShoppingMenu : MonoBehaviour
{
    #region VARIABLES
    public MenuType Menu;
    public Consumable[] foodOrPotions = new Consumable[5];
    public Weapon[] weapons = new Weapon[5];
    public Armor[] armor = new Armor[5];
    #endregion
    //UNITY FUNCTIONS
    #region UPDATE FUNCTION
    void Update()
    {
        switch (Menu)
        {
            case MenuType.Food:
                break;
            case MenuType.Potion:
                break;
            case MenuType.Weapon:
                break;
            case MenuType.Armor;
                break;
        }

    }
    #endregion
}
