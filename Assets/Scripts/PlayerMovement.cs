using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 dir = new Vector2();

    [SerializeField] private float speed, G, minG;

    [SerializeField] private bool isGround = false;

    [SerializeField] private Transform groundCheck;
    private Vector3 groundSize = new Vector3(0.15f, 0.2f, 0.15f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.localPosition.y < -10) SceneManager.LoadScene(1);

        dir = transform.right * Input.GetAxisRaw("Vertical") + transform.forward * -Input.GetAxisRaw("Horizontal");

        //dir.x = Input.GetAxisRaw("Vertical");
        //dir.z = Input.GetAxisRaw("Horizontal") * -1;

        if (dir.magnitude > 1) dir.Normalize();

        GroundCheck();
    }

    private void GroundCheck()
    {
        var objs = Physics.OverlapBox(groundCheck.position, groundSize);

        foreach (var obj in objs) 
        {
            if (obj.CompareTag("Ground"))
            {
                isGround = true;
                return;
            }
        }

        isGround = false;
    }

    private void FixedUpdate()
    {
        dir.y = isGround ? minG : G;

        rb.velocity = speed * Time.deltaTime * dir;
    }
}
