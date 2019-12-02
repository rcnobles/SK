using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // "speed" How fast the player is moving
    public float speed;
    // "my_rigid_body" Reference to my player rigid body
    private Rigidbody2D my_rigid_body;
    // "change" How much the player's position is changed
    private Vector3 change;
    // reference to the animator component
    private Animator animator;
    // marks know what scene we are in
    public static PlayerMovement instance;
    // marks when player leaves for another scene
    public string scenePassword;
    // controls whether player can move or not
    private Player_InteractionControl interactionControl;

    // Start is called before the first frame update
    void Start(){
        // "GetComponent" Completes the reference to the components
        animator = GetComponent<Animator>();
        my_rigid_body = GetComponent<Rigidbody2D>();
        interactionControl = GetComponent<Player_InteractionControl>();
        
    }

    // Update is called once per frame
    void Update(){
        // Reset change to 0, every frame reset how much the player changed
        change = Vector3.zero;
        /* If the player is pushing any buttons: up, down, left, right, WASD
            Add to that change and move the character depending on the change.
            GetAxisRaw: doesnt interpolate between values; snappy movement */
        if (interactionControl.movementEnabled) {
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
        }
        // Animation Integration
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {
        // If there is a change, move character.
        if(change != Vector3.zero){
            MoveCharacter();
            if ((change.x != 0 && change.y == 0) || (change.x == 0 && change.y != 0)) {
                animator.SetFloat("moveX", change.x);
                animator.SetFloat("moveY", change.y);
                animator.SetBool("moving", true);
            }
        }else{
            animator.SetBool("moving", false);
        }
    }

    // Move character from other places beside keyboard.
    void MoveCharacter(){
        // Reduce the speed of diagonal walking (vector addition)
        change.Normalize();
        // Call player rigid body and set it to move to new position 
        my_rigid_body.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

}
