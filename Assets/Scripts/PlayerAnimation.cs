using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.ResetTrigger("Walk");
        }
    }
}
