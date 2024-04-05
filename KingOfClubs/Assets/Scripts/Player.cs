using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    private NetworkCharacterController _cc;
    [SerializeField] Transform cam;
    bool Interacting = false;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();

            //cam direction
            Vector3 camForward = cam.forward;//(get v3)
            Vector3 camRight = cam.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            //set direction
            data.direction = (camForward * data.direction.z) + (camRight * data.direction.x);

            //check if player wants to interact
            Interacting = data.Interacting;

            //move player
            _cc.Move(5 * data.direction * Runner.DeltaTime);
        }
    }
    //if player is touching another player
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");
        if (Interacting == true && collision.gameObject.tag == "Player")
            Debug.Log("touching");

        Interacting = false;
    }
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
