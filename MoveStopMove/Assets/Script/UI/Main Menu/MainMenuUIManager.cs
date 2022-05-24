using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponShop;
    [SerializeField]
    private GameObject skinShop;
    [SerializeField]
    private TMP_Text gold;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject _joystick;
    [SerializeField]
    private GameObject inGameUI;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _joystick.SetActive(false);
        OpenMainMenu();
    }

    private void Update()
    {
        gold.SetText(GameFlowManager.Instance.totalPlayerGold.ToString());
    }

    public void OpenMainMenu()
    {
        anim.SetBool("isShow", true);
        anim.SetBool("isHide", false);
    }

    public void ShowWeaponShop()
    {
        weaponShop.SetActive(true);
        CloseMainMenu();
    }

    public void ShowSkinShop()
    {
        skinShop.SetActive(true);
        CloseMainMenu();
    }

    public void PressPlayButton()
    {
        _joystick.SetActive(true);
        GameFlowManager.Instance.gameState = GameFlowManager.GameState.gameStart;
        StartCoroutine(OpenInGameUI());
        CloseMainMenu();
    }
    IEnumerator OpenInGameUI()
    {
        yield return new WaitForSeconds(1);
        inGameUI.SetActive(true);
    }

    private void CloseMainMenu()
    {
        anim.SetBool("isShow", false);
        anim.SetBool("isHide", true);
        if (GameFlowManager.Instance.gameState == GameFlowManager.GameState.gameStart)
        {
            StartCoroutine(HideMainMenu());
        }
    }

    IEnumerator HideMainMenu()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
