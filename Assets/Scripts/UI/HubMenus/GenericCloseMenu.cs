using System;
using UnityEngine;

public class GenericCloseMenu : MonoBehaviour
{
    public static Action CloseCurrentMenu;
    [SerializeField]private float cooldown = 1;
    private void Update()
    {
        if (cooldown >= 0) { cooldown -= Time.deltaTime; }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }
    public void CloseMenu()
    {
        /* Subscription: UIManager + HubUIManager script. */
        if (cooldown <= 0)
        {
            CloseCurrentMenu?.Invoke();
            cooldown = 1;
        }
    }
}
