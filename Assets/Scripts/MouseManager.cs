using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    //Know what objects are clickable
    public LayerMask ClickableLayer;
    //Swap cursor per object
    public Texture2D NormalPointer;
    public Texture2D ClickableObjectCursor;
    public Texture2D DoorwayCursor;
    public Texture2D CombatCursor;

    public EventVector3 OnClickEnvironment;

    private int LeftMouseButton = 0;
    // Update is called once per frame
    void Update() {
        handleDetectionOfGameObjects();
    }

    private void handleDetectionOfGameObjects() {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, ClickableLayer.value)) {
            var isDoor = false;
            var isItem = false;
            var isEnemy = false;

            if (hit.collider.gameObject.tag == "Doorway") {
                isDoor = doorwayIsDectected(hit, isDoor);
            } else if (hit.collider.gameObject.tag == "Item") {
                isItem = itemIsDectected(hit, isItem);
            } else {
                Cursor.SetCursor(ClickableObjectCursor, Vector2.zero, CursorMode.Auto);
            }

            detectLeftMouseButtonClick(hit, isDoor, isItem);
        }
    }

    private bool doorwayIsDectected(RaycastHit hit, bool isDoor) {
        Cursor.SetCursor(DoorwayCursor, new Vector2(16, 16), CursorMode.Auto);
        return true;
    }

    private bool itemIsDectected(RaycastHit hit, bool isItem) {
        Cursor.SetCursor(CombatCursor, new Vector2(16, 16), CursorMode.Auto);
        return true;
    }

    private void detectLeftMouseButtonClick(RaycastHit hit, bool isDoor, bool isItem) {
        if (Input.GetMouseButtonDown(LeftMouseButton)) {
            if(isDoor) {
                handleDoorAction(hit, isDoor);
            } 
            else if(isItem) {
                handleItemAction(hit, isItem);
            } else {
                OnClickEnvironment.Invoke(hit.point);
            }
        }
    }

    private void handleItemAction(RaycastHit hit, bool isItem) {
        moveToItem(hit);
    }

    private void handleDoorAction(RaycastHit hit, bool isDoor) {
        moveToDoor(hit);
    }

    private void moveToDoor(RaycastHit hit) {
        Transform doorway = hit.collider.gameObject.transform;

        OnClickEnvironment.Invoke(doorway.position);
        Debug.Log("Door");
    }

    private void moveToItem(RaycastHit hit) {
        Transform item = hit.collider.gameObject.transform;

        OnClickEnvironment.Invoke(item.position);
        Debug.Log("Item");
    }


}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }