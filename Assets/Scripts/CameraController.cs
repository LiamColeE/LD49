using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public EndMenu endMenu;

    [Tooltip("The camera")]
    [SerializeField]
    private Transform camera;

    [Tooltip("Camera limits")]
    [SerializeField]
    private Vector2 cameraLimits;

    public float moveSpeed;
    private Vector2 moveVector;

    public float zoomScale;
    private float zoom = 1;

    private bool controlActive = true;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        zoomScale = zoomScale * 50;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        //if (DragAndDrop.instance.hasRock)
        //    return;

        //transform.position = new Vector3(transform.position.x, (int)GameManager.instance.totalHeight, transform.position.z);


        transform.Rotate(Vector3.up, -moveVector.x * Time.deltaTime * moveSpeed, Space.World);
        transform.Rotate(Vector3.right, -moveVector.y * Time.deltaTime * moveSpeed, Space.Self);

        Vector3 eulers = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(eulers.x, cameraLimits.x, cameraLimits.y), eulers.y, eulers.z));

        camera.LookAt(transform.position);

        //move camera to the zoom position
        Vector3 desiredPos = transform.position;
        desiredPos += new Vector3(0, 0, zoom);
        camera.localPosition = desiredPos;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!controlActive)
        {
            moveVector = Vector2.zero;
            return;
        }
        moveVector = context.ReadValue<Vector2>();
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        if (DragAndDrop.instance.hasRock || !controlActive)
            return;

        zoom = Mathf.Clamp(zoom + context.ReadValue<float>() * zoomScale,1,10);
    }

    public void OnToggleControls(InputAction.CallbackContext context)
    {
        if(context.started)
            controlActive = !controlActive;
    }

    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            endMenu.gameObject.SetActive(true);
            GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        }
    }

    public void OnCloseMenu(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            endMenu.gameObject.SetActive(false);
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }
    }

    public void OnActiveRotate(InputAction.CallbackContext context)
    {
        
    }
    
}
