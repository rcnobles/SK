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

    // Start is called before the first frame update
    void Start(){
        // Complete the reference to the players rigid body
        my_rigid_body = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update(){
        // Reset change to 0, every frame reset how much the player changed
        change = Vector3.zero;
        /* If the player is pushing any buttons: up, down, left, right, WASD
            Add to that change and move the character depending on the change.
            GetAxisRaw: doesnt interpolate between values; snappy movement */
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        // If there is a change, move character.
        if(change != Vector3.zero){
            MoveCharacter();
        }
    }
    // Move character from other places beside keyboard.
    void MoveCharacter(){
        // Call player rigid body and set it to move to new position 
        my_rigid_body.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

}
