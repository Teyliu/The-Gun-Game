using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private bool hadPistol;
    [SerializeField] private bool hadShotgun;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject pistol;
    public Animator animator;
    public RuntimeAnimatorController TwoHands;
    public RuntimeAnimatorController OneHands;
    public RuntimeAnimatorController ZeroHands;
    public bool condicionCumplida;

    private void Start()
    {
        hadPistol = true;
        pistol.SetActive(true);
        shotgun.SetActive(false);
        animator.runtimeAnimatorController = OneHands;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
