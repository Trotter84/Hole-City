using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody playerRb;
    float verticalInput;
    float horizontalInput;

    [Header("Attributes")]
    [SerializeField] float speed;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        playerRb.AddForce(transform.forward * verticalInput * speed);
        playerRb.AddForce(transform.right * horizontalInput * speed);
    }
}
