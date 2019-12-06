using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FollowerState{
    idle,
    walk,
}
public class bear : MonoBehaviour{
    public FollowerState currentState;
    private Rigidbody2D myRigidbody;
    // Target variable
    public Transform target;
    // Inside chase radius the bear will follow the player
    public float outerChaseRadius;
    public float innerChaseRadius;
    // If Player goes out of chase radius the bear moves back to homeposition
    public Transform homePosition;
    public string followerName;
    public float moveSpeed;
    public Animator anim;

    // Start is called before the first frame update
    void Start(){
        currentState = FollowerState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Where the bear needs to moving towards
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate(){
        CheckDistance();
    }

    void CheckDistance(){
        // Only move towards player if inside of radius.
        if (Vector3.Distance(target.position,
                            transform.position) <= outerChaseRadius
            && Vector3.Distance(target.position,transform.position) > innerChaseRadius){
            currentState = FollowerState.walk;
            if (currentState == FollowerState.idle || currentState == FollowerState.walk ){
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, Math.Min(Vector3.Distance(target.position, transform.position) * Time.deltaTime * 2, moveSpeed * Time.deltaTime * 2));
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(FollowerState.walk);
            }
        }
        else {
            anim.SetBool("moving", false);
            anim.SetFloat("moveX", 0);
            anim.SetFloat("moveY", 0);
        }
    }

    private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
        Debug.Log("x " + setVector.x);
        Debug.Log("y " + setVector.y);
    }
    private void changeAnim(Vector2 direction){
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
            anim.SetBool("moving", true);
            if (direction.x > 0){
                // Right
                SetAnimFloat(Vector2.right);
            }else if (direction.x < 0){
                // Left
                SetAnimFloat(Vector2.left);
            }
        }else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)){
            anim.SetBool("moving", true);
            if (direction.y > 0){
                // Up
                SetAnimFloat(Vector2.up);
            }else if (direction.y < 0){
                // Down
                SetAnimFloat(Vector2.down);
            }
        }
    }
    private void ChangeState(FollowerState newState){
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
