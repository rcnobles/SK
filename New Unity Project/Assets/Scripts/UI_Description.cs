using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Description : MonoBehaviour
{
    // original position of window
    private Vector3 origin;

    // reference for inventoryActive
    private Player_InteractionControl interactionControl;

    float out_y;

    void Start() {
        origin = transform.localPosition;
        out_y = -GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.yMax - GetComponent<RectTransform>().rect.height / 2;
        transform.localPosition = new Vector3(transform.localPosition.x, out_y, transform.localPosition.z);
        interactionControl = GetComponentInParent<Player_InteractionControl>();
    }

    void Update() {
        // slides inventory window in and out of view
        if (interactionControl.inventoryActive && transform.localPosition.y < origin.y) {
            transform.localPosition += new Vector3(0, Math.Max(1, (origin.y - transform.localPosition.y) / 10), 0);
        }
        else if (!interactionControl.inventoryActive && transform.localPosition.y > out_y) {
            transform.localPosition -= new Vector3(0, Math.Max(1, (transform.localPosition.y - out_y) / 10), 0);
        }
    }

    public void ResetPosition() {
        transform.localPosition = new Vector3(transform.localPosition.x, out_y, transform.localPosition.z);
    }
}
