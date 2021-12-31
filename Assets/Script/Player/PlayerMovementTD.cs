
using UnityEngine;

public class PlayerMovementTD : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform graphics;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;
    private Vector2 mousePos;

    private Camera cam;
    
    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        //graphics.localRotation = Quaternion.Euler(0f, 0f, angle);
        rb.rotation = angle;
    }
}
