using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [Header("Элементы Интерфейса")]
    public GameObject interactText;
    public GameObject shopMenu;

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (shopMenu.activeSelf)
            {
                shopMenu.SetActive(false);
            }
            else
            {
                shopMenu.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactText.SetActive(false);
            shopMenu.SetActive(false);
        }
    }
}