using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Components")]
    public Camera mainCamera;

    [Header("Attributes")]
    [SerializeField] float speed;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        ScreenRestraints();

        float horizontalInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, horizontalInput * speed);
    }

    void ScreenRestraints()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(mainCamera.transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.x = Mathf.Clamp01(pos.y);
        mainCamera.transform.position = mainCamera.ViewportToWorldPoint(pos);
    }

}
