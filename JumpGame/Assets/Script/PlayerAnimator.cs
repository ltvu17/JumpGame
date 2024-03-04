using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string Is_Walking = "IsWalking";
    private const string Is_Hodling = "IsHolding";
    private Animator animator;
    [SerializeField] private Player player;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        animator.SetBool(Is_Walking, player.IsWalking());
        animator.SetBool(Is_Hodling, player.IsHoding());
    }
}
