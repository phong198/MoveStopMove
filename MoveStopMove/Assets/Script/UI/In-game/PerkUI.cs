using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject[] perkButtonList;

    private void OnEnable()
    {
        RandomizePerk();
    }

    private void RandomizePerk()
    {
        for (int i = 0; i < 3;)
        {
            GameObject randomperk = perkButtonList[UnityEngine.Random.Range(0, perkButtonList.Length)];
            if (!randomperk.activeInHierarchy)
            {
                randomperk.SetActive(true);
                ++i;
            }
            else continue;
        }
    }

    public void ActivateDamagePerk() { player.GetPerk(1); ClosePerkUI(); }
    public void ActivateHealthPerk() { player.GetPerk(2); ClosePerkUI(); }
    public void ActivateSpeedPerk() { player.GetPerk(3); ClosePerkUI(); }
    public void ActivateBullet2Perk() { player.GetPerk(4); ClosePerkUI(); }
    public void ActivateBullet3Perk() { player.GetPerk(5); ClosePerkUI(); }
    public void ActivateBullet4Perk() { player.GetPerk(6); ClosePerkUI(); }

    private void ClosePerkUI()
    {
        foreach (GameObject perk in perkButtonList)
        {
            perk.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
