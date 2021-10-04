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
        zoomScale = zoomScale * 20;
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

    public void OnMove(InputValue value)
    {
        if (!controlActive)
        {
            moveVector = Vector2.zero;
            return;
        }
        moveVector = (Vector2)value.Get();
    }

    public void OnZoom(InputValue value)
    {
        if (DragAndDrop.instance.hasRock || !controlActive)
            return;

        zoom = Mathf.Clamp(zoom + (float)value.Get() * zoomScale,1,10);
    }

    public void OnToggleControls()
    {
        controlActive = !controlActive;
    }

    public void OnOpenMenu()
    {
        endMenu.gameObject.SetActive(true);
        GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
    }

    public void OnCloseMenu()
    {
        endMenu.gameObject.SetActive(false);
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
    }
    
}
