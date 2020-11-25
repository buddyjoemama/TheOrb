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
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
        Debug.DrawRay(Vector3.zero, ray.direction * 500, Color.blue);
        Debug.Log(ray.direction);
        Vector3 lookAt = Vector3.zero;

        if(Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            lookAt = hit.point;
        }
        else
        {
            // pythag.
            //Mathf.Sqrt(transform.position.y^2, )

            lookAt = ray.GetPoint(1000);
        }

        playerManager.rig.transform.LookAt(lookAt);

        Debug.DrawLine(ray.origin, lookAt, Color.red);
    }

    private void LateUpdate()
    {
         transform.position = playerManager.player.transform.position + offset;
    }
}
