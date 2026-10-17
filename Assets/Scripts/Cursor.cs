using UnityEngine;

public class EnableCursor : MonoBehaviour
{
    void Start()
    {
        // Unlock the mouse cursor and make it visible for the UI buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}