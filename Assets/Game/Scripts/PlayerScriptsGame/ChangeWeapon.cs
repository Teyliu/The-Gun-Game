using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject pistol;
    public Animator animator;
    public RuntimeAnimatorController TwoHands;
    public RuntimeAnimatorController OneHands;
    public RuntimeAnimatorController ZeroHands;
    public bool hadPistol;
    public bool hadShotgun;
    public bool isShotgunAvailable; // New bool for shotgun availability

    private void Start()
    {
        hadPistol = true;
        pistol.SetActive(true);
        shotgun.SetActive(false);
        animator.runtimeAnimatorController = OneHands;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && isShotgunAvailable)
        {
            Change();
        }
    }

    private void Change()
    {
        if (hadPistol && !hadShotgun)
        {
            pistol.SetActive(false);
            shotgun.SetActive(true);
            hadPistol = false;
            hadShotgun = true;
            animator.runtimeAnimatorController = ZeroHands;
        }
        else if (!hadPistol && hadShotgun)
        {
            pistol.SetActive(true);
            shotgun.SetActive(false);
            hadPistol = true;
            hadShotgun = false;
            animator.runtimeAnimatorController = OneHands;
        }
    }
}
