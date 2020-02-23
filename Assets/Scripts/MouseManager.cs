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
        RaycastHit hit;
        bool isDoor = false;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, ClickableLayer.value)) {
            isDoor = doorwayIsDectected(hit, isDoor);
        } else {
            Cursor.SetCursor(NormalPointer, Vector2.zero, CursorMode.Auto);
        }

        detectLeftMouseButtonClick(hit, isDoor);
    }

    private bool doorwayIsDectected(RaycastHit hit, bool isDoor) {
        if (hit.collider.gameObject.tag == "Doorway") {
            Cursor.SetCursor(DoorwayCursor, new Vector2(16, 16), CursorMode.Auto);
            isDoor = true;
        } else {
            Cursor.SetCursor(ClickableObjectCursor, new Vector2(16, 16), CursorMode.Auto);
        }

        return isDoor;
    }

    private void detectLeftMouseButtonClick(RaycastHit hit, bool isDoor) {
        if (Input.GetMouseButtonDown(LeftMouseButton)) {
            if(isDoor) {
                Transform doorway = hit.collider.gameObject.transform;
                OnClickEnvironment.Invoke(doorway.position);

                Debug.Log("Door");
            }
            else {
                OnClickEnvironment.Invoke(hit.point);
            }
        }
    }


}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }