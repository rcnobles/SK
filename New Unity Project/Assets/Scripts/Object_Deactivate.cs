using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Deactivate : MonoBehaviour
{
    void Triggered() {
        gameObject.SetActive(false);
    }
}
