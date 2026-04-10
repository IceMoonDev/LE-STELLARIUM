using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    Camera cam;

    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;
    public LayerMask layer;
    public Transform pivot;

    public float epaisseurMaxMur = 5f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 direction = mouseWorldPosition - (Vector2)pivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(0, 0, angle);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootDoublePortal(bluePortalPrefab, "blue portal", orangePortalPrefab, "orange portal", direction);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ShootDoublePortal(orangePortalPrefab, "orange portal", bluePortalPrefab, "blue portal", direction);
        }
    }

    void ShootDoublePortal(GameObject frontPrefab, string frontTag, GameObject backPrefab, string backTag, Vector2 direction)
    {
        RaycastHit2D hitFront = Physics2D.Raycast(pivot.position, direction.normalized, Mathf.Infinity, layer);

        if (hitFront.collider != null)
        {
            GameObject oldFrontPortal = GameObject.FindGameObjectWithTag(frontTag);
            if (oldFrontPortal != null) Destroy(oldFrontPortal);

            Quaternion frontRotation = Quaternion.FromToRotation(Vector2.right, hitFront.normal);
            Instantiate(frontPrefab, hitFront.point, frontRotation);

            Vector2 pointDerriereMur = hitFront.point - (hitFront.normal * epaisseurMaxMur);

            RaycastHit2D hitBack = Physics2D.Raycast(pointDerriereMur, hitFront.normal, epaisseurMaxMur, layer);

            if (hitBack.collider != null)
            {
                GameObject oldBackPortal = GameObject.FindGameObjectWithTag(backTag);
                if (oldBackPortal != null) Destroy(oldBackPortal);

                Quaternion backRotation = Quaternion.FromToRotation(Vector2.right, hitBack.normal);
                Instantiate(backPrefab, hitBack.point, backRotation);
            }
        }
    }
}
