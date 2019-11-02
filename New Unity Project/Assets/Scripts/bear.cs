using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bear : MonoBehaviour{
    // Target variable
    public Transform target;
    // Inside chase radius the bear will follow the player
    public float outerChaseRadius;
    public float innerChaseRadius;
    // If Player goes out of chase radius the bear moves back to homeposition
    public Transform homePosition;
    public string followerName;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start(){
        // Where the bear needs to moving towards
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update(){
        CheckDistance();
    }

    void CheckDistance(){
        // Only move towards player if inside of radius.
        if(Vector3.Distance(target.position,
                            transform.position) <= outerChaseRadius
                            && Vector3.Distance(target.position,transform.position) > innerChaseRadius){
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
}
