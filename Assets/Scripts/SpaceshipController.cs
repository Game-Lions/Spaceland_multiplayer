using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class SpaceshipController : NetworkBehaviour
{
    [Tooltip("Thrust strength")]
    [SerializeField] float ThrustStrength = 1f;
    [Tooltip("Left and Right Torque strength")]
    [SerializeField] float TorqueStrength = 1f;

    //[SerializeField]
    //InputAction move = new InputAction(type: InputActionType.Value, expectedControlType: nameof(Vector2));

    private Rigidbody2D rb;
    public GameObject Thust;
    public GameObject RightThrust;
    public GameObject LeftThrust;

    //void OnEnable()
    //{
    //    move.Enable();
    //}

    //void OnDisable()
    //{
    //    move.Disable();
    //}

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Thust.SetActive(false);
        RightThrust.SetActive(false);
        LeftThrust.SetActive(false);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            Vector2 force = inputData.moveActionValue;

            // Bottom Thrust
            Thust.SetActive(force.y > 0);

            // Right Thrust
            RightThrust.SetActive(force.x > 0);

            // Left Thrust
            LeftThrust.SetActive(force.x < 0);

            // Apply forces and torque
            if (force.y != 0)
            {
                rb.AddForce(transform.up * ThrustStrength * force.y);
            }

            if (force.x != 0)
            {
                rb.AddTorque(TorqueStrength * -force.x);
            }
        }
    }
    //void FixedUpdate()  // FixedupdateNetwork
    //{
    //    Vector2 force = move.ReadValue<Vector2>();

    //    // Bottom Thrust
    //    Thust.SetActive(force.y > 0);

    //    // Right Thrust
    //    RightThrust.SetActive(force.x > 0);

    //    // Left Thrust
    //    LeftThrust.SetActive(force.x < 0);

    //    // Apply forces and torque
    //    if (force.y != 0)
    //    {
    //        rb.AddForce(transform.up * ThrustStrength * force.y);
    //    }

    //    if (force.x != 0)
    //    {
    //        rb.AddTorque(TorqueStrength * -force.x);
    //    }
    //}
}
