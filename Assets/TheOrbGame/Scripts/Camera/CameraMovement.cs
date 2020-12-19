using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class CameraMovement : MonoBehaviour
{
    public PlayerManager playerManager;
    private Vector3 offset;
    public Camera mainCamera;
    public Material effect;

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
        Vector3 lookAt = Vector3.zero;

        if(Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            lookAt = hit.point;
        }
        else
        {
            lookAt = ray.GetPoint(1000);
        }

        playerManager.rig.transform.LookAt(lookAt);
    }

    private void LateUpdate()
    {
         transform.position = playerManager.player.transform.position + offset;
    }
}
