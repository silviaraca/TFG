using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   public float speed;
   public Animator animator;

   private Vector3 direction;


   private void Update()
   {
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    
    direction = new Vector3(horizontal, vertical);

    AnimateMovement(direction);
   }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void AnimateMovement(Vector3 direction)
    {
        if(animator != null)
        {
            if(direction.magnitude > 0)
            {
                animator.SetBool("IsMoving", true);
                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);

            }
            else
            {
                animator.SetBool("IsMoving", false);

            }

        }
    }
}
