using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    Camera cam;

    // NEW: We now have two separate slots for your prefabs
    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;

    public Transform pivot;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(0, 0, angle);

        // LEFT CLICK = Blue Portal
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootPortal(bluePortalPrefab, "blue portal");
        }

        // RIGHT CLICK = Orange Portal
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ShootPortal(orangePortalPrefab, "orange portal");
        }
    }

    // Helper function to handle the shooting logic for both colors
    void ShootPortal(GameObject prefabToShoot, string tagToLookFor)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector2 cursorPos = cam.ScreenToWorldPoint(mousePos);

        // Find the specific color portal and destroy it
        GameObject oldPortal = GameObject.FindGameObjectWithTag(tagToLookFor);
        if (oldPortal != null)
        {
            Destroy(oldPortal);
        }

        // Spawn the new portal
        Instantiate(prefabToShoot, new Vector3(cursorPos.x, cursorPos.y, 0), Quaternion.identity);
    }
}
