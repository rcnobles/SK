using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_InteractionControl : MonoBehaviour {
    // current interactable object;
	public GameObject currentObject = null;

    // player inventory
	public GameObject[] inventory = new GameObject[5];
	private int itemCount = 0;

    private GameObject inventoryUI;
    private GameObject descriptionUI;
    private GameObject useUI;

    // controls whether player can pick up items
    public bool movementEnabled;

    // indicates whether inventory is active
    public bool inventoryActive;

    // indicates whether inventory item is selected
    public bool useActive;

    // indicates whether dialogue is active
    public bool dialogueActive;

    // indicates selected item
    private int currentItem;

    // indicates selected option in use window
    private bool useChoice;

    void Start(){
        movementEnabled = true;
        inventoryActive = false;
        useActive = false;
        dialogueActive = false;
        useChoice = true;
        Image[] inventoryImages = transform.GetChild(0).GetChild(1).GetComponentsInChildren<Image>();
        for (int i = 1; i <= 6; i++) {
            inventoryImages[i].enabled = false;
        }   
        inventoryUI = transform.GetChild(0).GetChild(1).gameObject;
        descriptionUI = transform.GetChild(0).GetChild(0).gameObject;
        useUI = transform.GetChild(0).GetChild(2).gameObject;
        currentItem = 0;
    }

	void Update(){
        if (Input.GetButtonDown("Interact")) {
            if (currentObject && movementEnabled) {
                currentObject.SendMessage("Interact", this);
                Debug.Log("h");
            }
            else if (inventoryActive && itemCount != 0 && !useActive) {
                useActive = true;
                useUI.gameObject.SetActive(true);
            }
            else if (dialogueActive && !useActive) {
                currentObject.SendMessage("Interact", this);
            }
            else if (useActive) {
                if (dialogueActive) {
                    inventory[currentItem].GetComponent<Object_PickupItem>().Use(this, currentObject);
                }
                else if (useChoice) {
                    inventory[currentItem].GetComponent<Object_PickupItem>().Use(this, currentObject);
                    inventoryActive = false;
                    useUI.gameObject.SetActive(false);
                    inventoryUI.GetComponent<UI_Inventory>().ResetPosition();
                    descriptionUI.GetComponent<UI_Description>().ResetPosition();
                }
                else {
                    useChoice = true;
                    useActive = false;
                    useUI.gameObject.SetActive(false);
                    useUI.transform.GetChild(2).GetComponent<RectTransform>().offsetMax = useUI.transform.GetChild(0).GetComponent<RectTransform>().offsetMax;
                    useUI.transform.GetChild(2).GetComponent<RectTransform>().offsetMin = useUI.transform.GetChild(0).GetComponent<RectTransform>().offsetMin;
                }
            }
        }
        else if (Input.GetButtonDown("Inventory") && !dialogueActive && !useActive) {
            if (!inventoryActive) {
                inventoryActive = true;
                movementEnabled = false;
                transform.GetChild(0).GetChild(1).GetChild(5).transform.localPosition = transform.GetChild(0).GetChild(1).GetChild(0).transform.localPosition;
                if (itemCount != 0) {
                    Debug.Log(inventory[0].GetComponent<Object_PickupItem>().itemName);
                    descriptionUI.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText(inventory[0].GetComponent<Object_PickupItem>().itemName);
                    descriptionUI.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().SetText(inventory[0].GetComponent<Object_PickupItem>().itemDescription);
                }
            }
            else {
                inventoryActive = false;
                movementEnabled = true;
            }
        }
        else if (Input.GetButtonDown("Horizontal") && inventoryActive && itemCount != 0) {
            if (!useActive) {
                if (Input.GetAxis("Horizontal") > 0) {
                    if (currentItem < itemCount - 1) {
                        currentItem++;
                    }
                    else {
                        currentItem = 0;
                    }
                }
                else if (currentItem > 0) {
                    currentItem--;
                }
                else
                    currentItem = itemCount - 1;
                transform.GetChild(0).GetChild(1).GetChild(5).transform.localPosition = transform.GetChild(0).GetChild(1).GetChild(currentItem).transform.localPosition;
                descriptionUI.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText(inventory[currentItem].GetComponent<Object_PickupItem>().itemName);
                descriptionUI.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().SetText(inventory[currentItem].GetComponent<Object_PickupItem>().itemDescription);
            }
            else {
                if (useChoice) {
                    useUI.transform.GetChild(2).GetComponent<RectTransform>().offsetMax = useUI.transform.GetChild(1).GetComponent<RectTransform>().offsetMax;
                    useUI.transform.GetChild(2).GetComponent<RectTransform>().offsetMin = useUI.transform.GetChild(1).GetComponent<RectTransform>().offsetMin;
                }
                else {
                    useUI.transform.GetChild(2).GetComponent<RectTransform>().offsetMax = useUI.transform.GetChild(0).GetComponent<RectTransform>().offsetMax;
                    useUI.transform.GetChild(2).GetComponent<RectTransform>().offsetMin = useUI.transform.GetChild(0).GetComponent<RectTransform>().offsetMin;
                }
                useChoice = !useChoice;
            }
        }
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("InteractableObject")){
			currentObject = other.gameObject;
		}
        else if (other.CompareTag("Warp")) {
            transform.SetPositionAndRotation(new Vector3(other.transform.position.x, transform.position.y, 0), other.transform.rotation);
        }
	}

	void OnTriggerExit2D(Collider2D other){
		if (currentObject == other.gameObject) {
			currentObject = null;
		}
	}

	public void AddInventory(){
		inventory[itemCount] = currentObject;
		itemCount++;
        Image[] inventoryImages = transform.GetChild(0).GetChild(1).GetComponentsInChildren<Image>();
        for (int i = 1; i <= 5; i++) {
            inventoryImages[i].enabled = false;
            if (i - 1 < itemCount) {
                inventoryImages[i].enabled = true;
                inventoryImages[i].sprite = inventory[i - 1].GetComponent<SpriteRenderer>().sprite;
            }
        }
        inventoryImages[6].enabled = true;

        Debug.Log ("r");
	}

    public void RemoveInventory() {
        inventory[currentItem] = null;
        for(int i = currentItem; i < itemCount - 1; i++) {
            inventory[i] = inventory[i + 1];
        }

        itemCount--;

        Image[] inventoryImages = transform.GetChild(0).GetChild(1).GetComponentsInChildren<Image>();

        for (int i = 1; i <= 5; i++) {
            inventoryImages[i].enabled = false;
            if (i - 1 < itemCount) {
                inventoryImages[i].enabled = true;
                inventoryImages[i].sprite = inventory[i - 1].GetComponent<SpriteRenderer>().sprite;
            }
        }


        Debug.Log(itemCount);

        if (itemCount == 0) {
            Debug.Log(itemCount + "Check");
            inventoryImages[6].enabled = false;
            descriptionUI.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText("");
            descriptionUI.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().SetText("");
        }
    }
}
