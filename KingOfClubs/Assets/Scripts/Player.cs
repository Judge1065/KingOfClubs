using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    private NetworkCharacterController _cc;
    [SerializeField] Transform cam;

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

            data.direction.z = data.direction.z * camForward.z;
            data.direction.x = data.direction.x * camRight.x;

            Debug.Log(data.direction);


            _cc.Move(5 * data.direction * Runner.DeltaTime); //(x cam v3 )
        } 
    }
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
