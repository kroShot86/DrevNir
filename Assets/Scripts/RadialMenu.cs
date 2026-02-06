using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuContainer;
    private bool isOpen;

    private void Start()
    {
        if (menuContainer != null) menuContainer.SetActive(false);
        GameInput.Instance.OnInventoryAction += (s, e) => ToggleMenu();
    }

    private void ToggleMenu()
    {
        if (menuContainer == null) return;
        
        isOpen = !isOpen;
        menuContainer.SetActive(isOpen);
        
        Cursor.visible = isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}