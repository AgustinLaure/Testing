using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody rb;

    private EventBus eventBus;

    private ForceMode jumpForceMode = ForceMode.Impulse;

    private Vector3 axis = Vector3.zero;

    private bool shouldJump = false;

    private void Start()
    {
        eventBus = ServiceLocator.Instance.GetService<EventBus>();
    }

    private void Update()
    {
        axis.x = Input.GetAxisRaw("Horizontal");
        axis.z = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            shouldJump = true;
        }

        if (axis.x != 0f || axis.z != 0f)
        {
            eventBus.Raise<Events.OnPlayerMove>();
        }

        transform.position = transform.position + axis * (speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (shouldJump)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), jumpForceMode);
            shouldJump = false;

            eventBus.Raise<Events.OnPlayerJump>(jumpForce);
        }
    }
}
