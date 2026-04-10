using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    Camera cam;

    // NEW: We now have two separate slots for your prefabs
    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;
    public LayerMask layer;

    public Transform pivot;
    public GameObject truc;

    PlayerInputAction playerInputAction;

    void Start()
    {
        cam = Camera.main;
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = playerInputAction.InGame.mousePos.ReadValue<Vector2>();
        Vector2 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(0, 0, angle);

        if (truc != null)
        {
            truc.transform.position = mousePos;
        }

        // LEFT CLICK = Blue Portal
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos, Mathf.Infinity, layer);
            if (Physics2D.Raycast(transform.position, mousePos, Mathf.Infinity, layer))
            {
                Instantiate(bluePortalPrefab, hit.point, Quaternion.identity);
                var width = hit.collider.gameObject.transform.localScale.x;
                Instantiate(orangePortalPrefab, hit.point + Vector2.right * width, Quaternion.identity);
            }
            //ShootPortal(bluePortalPrefab, "blue portal");
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
