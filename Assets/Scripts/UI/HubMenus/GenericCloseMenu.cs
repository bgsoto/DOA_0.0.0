using System;
using UnityEngine;
using UnityEngine.Events;

public class GenericCloseMenu : MonoBehaviour
{
    public static Action CloseCurrentMenu;
    [SerializeField]private float cooldown = 1;
    public UnityEvent CloseCurrentAction;
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
            CloseCurrentAction?.Invoke();
            cooldown = 1;
        }
    }
}
