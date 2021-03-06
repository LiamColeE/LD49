using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    public static DragAndDrop instance;
    public GameObject rockplosion;
    private bool _mouseState;
    private GameObject target;
    public Vector3 screenSpace;
    public Vector3 offset;
    public bool hasRock;
    private Vector2 mousePos;
    public float depth;
    private Vector2 rotationVector;

    private bool controlActive = false;

    // Use this for initialization
    void Start()
    {
        instance = this;


    }

    // Update is called once per frame
    void Update()
    {
        if (_mouseState)
        {
            RaycastHit hit;
            depth = GetDistanceToNonRockObject(out hit);
            //keep track of the mouse position
            var curScreenSpace = new Vector3(mousePos.x, mousePos.y, depth);

            //convert the screen mouse position to world point and adjust with offset
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;


            //update the position of the object in the world
            target.transform.position = curPosition;
            //target.transform.Rotate(new Vector3(rotationVector.y, rotationVector.x, 0) * Time.deltaTime * 40, Space.World);

            if(rotationVector.x != 0)
                target.transform.rotation = Quaternion.AngleAxis(-rotationVector.normalized.x, Camera.main.transform.forward) * target.transform.rotation;
            
            if(rotationVector.y != 0)
                target.transform.rotation = Quaternion.AngleAxis(rotationVector.normalized.y, Camera.main.transform.right) * target.transform.rotation;
        }
    }


    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        LayerMask mask = LayerMask.GetMask("Rock");
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, 100, mask))
        {
            target = hit.collider.gameObject;
        }

        return target;
    }

    float GetDistanceToNonRockObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        LayerMask mask = LayerMask.GetMask("Rock");
        if (Physics.Raycast(ray.origin, ray.direction, out hit, depth, ~mask))
        {
            Debug.Log("object hit" + hit.transform.gameObject.name);
            float distance = Vector3.Distance(Camera.main.transform.position, hit.point);
            return distance - GetFarSideOfRock(ray.direction);
        }
        return depth;
    }

    float GetFarSideOfRock(Vector3 rayDirection)
    {
        RaycastHit hit;
        Vector3 testPoint = target.transform.position + rayDirection * 3;
        Vector3 flippedDirection = rayDirection * -1;
        LayerMask mask = LayerMask.GetMask("Rock");
        if (Physics.Raycast(testPoint, flippedDirection, out hit, 3, mask))
        {
            Debug.Log("object hit" + hit.transform.gameObject.name);
            float distance = Vector3.Distance(target.transform.position, hit.point);
            return distance;
        }
        return 0;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RaycastHit hitInfo;
            target = GetClickedObject(out hitInfo);
            if (target != null)
            {
                try
                {
                    _mouseState = true;
                    screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                    offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, screenSpace.z));
                    depth = screenSpace.z;

                    Rigidbody rockrb = target.GetComponent<Rigidbody>();

                    rockrb.useGravity = false;
                    rockrb.isKinematic = true;
                    hasRock = true;
                }
                catch { }
              
            }
        }
        else
        {
            try
            {
                Rigidbody rockrb = target.GetComponent<Rigidbody>();
                rockrb.useGravity = true;
                rockrb.isKinematic = false;
            }
            catch { }

            _mouseState = false;
            hasRock = false;
        }
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        //Debug.Log(value.Get<Vector2>());
        mousePos = context.ReadValue<Vector2>();
    }

    public void OnZoom(InputAction.CallbackContext context)
    {

#if UNITY_WEBGL && !UNITY_EDITOR
        depth = Mathf.Clamp(depth + context.ReadValue<float>()* 0.05f, 1, 15);
#else
        depth = Mathf.Clamp(depth + context.ReadValue<float>() * 0.001f, 1, 15);
#endif
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!controlActive)
        {
            rotationVector = Vector2.zero;
            return;
        }
        rotationVector = context.ReadValue<Vector2>();
    }

    public void OnToggleControls(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            controlActive = !controlActive;
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            RaycastHit hitInfo;
            target = GetClickedObject(out hitInfo);
            if (target != null)
            {
                GameObject clone = Instantiate(rockplosion);
                clone.transform.position = target.transform.position;
                Destroy(target.gameObject);
            }
        }
    }

    public void OnActiveRotate(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            controlActive = true;
        }

        if(context.canceled)
        {
            controlActive = false;
        }
    }
}