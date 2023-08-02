using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool allowMove;
   public float speed;
   public Animator animator;
   private Vector3 direction;
   public VectorPosition startingPosition;
   //public GameObject pauseMenu;

    private void Start(){
        allowMove = true;
        transform.position = startingPosition.initialValue;

    }

   private void Update()
   {
    if(allowMove && !PauseMenu.isPaused){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        direction = new Vector3(horizontal, vertical, vertical/1000);
        AnimateMovement(direction);
    }
    
   }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/1000);
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
