using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public PlayerManager playerManager;
    private Vector3 offset;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerManager.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = InputSystem.GetDevice<Mouse>().position;

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(mousePos.x.ReadValue(), mousePos.y.ReadValue()));
        Debug.DrawRay(ray.origin, ray.direction * 500, Color.yellow);

        if(Physics.Raycast(ray, out RaycastHit hit, 1000) && hit.collider.tag == "Floor")
        {
            //        gunRig.transform.Rotate(new Vector3(0, lookVector.y * 4), Space.Self);
            playerManager.rig.transform.LookAt(hit.point);
        }

        Debug.Log(hit.collider);
    }

    private void LateUpdate()
    {
         transform.position = playerManager.player.transform.position + offset;
    }
}
