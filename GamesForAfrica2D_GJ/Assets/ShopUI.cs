using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel; 

    public void ShowShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void HideShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}