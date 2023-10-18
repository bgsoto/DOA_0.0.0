using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIList;

    public static Action<bool> DisablePlayerControls;

    private Stack<int> UIStack = new Stack<int>();

    [SerializeField] private AudioSource audioSource;
    private void OnEnable()
    {
        /* Subscribes to event(s). */
        MenuDisplay.OnMenuEnter += DisplayMenu;
        KeypadDisplayManager.OnCorrectCoor += HideMenu;
        GenericCloseMenu.CloseCurrentMenu += HideMenu;
    }

    private void OnDisable()
    {
        /* Unsubscribes from event(s). */
        MenuDisplay.OnMenuEnter -= DisplayMenu;
        KeypadDisplayManager.OnCorrectCoor -= HideMenu;
        GenericCloseMenu.CloseCurrentMenu -= HideMenu;
    }

    private void Start()
    {
        /* Locks cursor in the middle of the screen and hides the cursor. */
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /* 
     * Disables player controls when a menu is active. 
     * Uses a Stack data structure to manage the number of active menus.
     * Will check settings for animation and sound info, settings script should be on any UI object.
     */
    private void DisplayMenu(int menuIndex)
    {
        UIStack.Push(menuIndex);
        if (UIList[UIStack.Peek()].GetComponent<UISettings>().doSound == true) { OpenMenuSound(); }
        if (UIList[UIStack.Peek()].GetComponent<UISettings>().doAnim == true) { OpenMenuAnim(menuIndex); }
        else { UIList[menuIndex].SetActive(true); }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        /* Subscription: PlayerCamera, PlayerMovement. */
        DisablePlayerControls?.Invoke(true);
    }

    /* Enables player controls if there are no active menus. */
    private void HideMenu()
    {
        if (UIList[UIStack.Peek()].GetComponent<UISettings>().doAnim == true) { StartCoroutine(CloseMenuAnim()); }
        else { UIList[UIStack.Pop()].SetActive(false); }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (UIStack.Count <= 0)
        {
            /* Subscription: PlayerCamera, PlayerMovement. */
            DisablePlayerControls?.Invoke(false);
        }
    }

    public void OpenMenuAnim(int menuIndex)//animation for opening menu if doAnim is ticked
    {
        UIList[menuIndex].SetActive(true);
        UIList[menuIndex].GetComponent<CanvasGroup>().DOFade(1, UIList[UIStack.Peek()].GetComponent<UISettings>().duration);
    }
    public void OpenMenuSound()//animation for opening menu if doAnim is ticked
    {
        var audioClip  = UIList[UIStack.Peek()].GetComponent<UISettings>().clip;
        if (audioClip != null) { audioSource.PlayOneShot(audioClip); }
    }

    IEnumerator CloseMenuAnim() //animation for closing menu (waits until completed before closing fully)
    {
        UIList[UIStack.Peek()].GetComponent<CanvasGroup>().DOFade(0, UIList[UIStack.Peek()].GetComponent<UISettings>().duration / 2);
        yield return new WaitForSeconds(UIList[UIStack.Peek()].GetComponent<UISettings>().duration / 2);
        UIList[UIStack.Pop()].SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (UIStack.Count <= 0)
        {
            /* Subscription: PlayerCamera, PlayerMovement. */
            DisablePlayerControls?.Invoke(false);
        }
        yield return null;
    }
}
