using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    public static DragAndDrop instance;
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
            //keep track of the mouse position
            var curScreenSpace = new Vector3(mousePos.x, mousePos.y, depth);

            //convert the screen mouse position to world point and adjust with offset
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;


            //update the position of the object in the world
            target.transform.position = curPosition;
            target.transform.Rotate(rotationVector);
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

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            RaycastHit hitInfo;
            target = GetClickedObject(out hitInfo);
            if (target != null)
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
        }
        else
        {
            Rigidbody rockrb = target.GetComponent<Rigidbody>();
            rockrb.useGravity = true;
            rockrb.isKinematic = false;

            _mouseState = false;
            hasRock = false;
        }
    }

    public void OnMousePos(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
        mousePos = value.Get<Vector2>();
    }

    public void OnZoom(InputValue value)
    {
        if (!controlActive)
        {
            return;
        }
        depth = Mathf.Clamp(depth - (float)value.Get() * 0.001f, 1, 15);
    }

    public void OnMove(InputValue value)
    {
        if (!controlActive)
        {
            rotationVector = Vector2.zero;
            return;
        }
        rotationVector = value.Get<Vector2>();
    }

    public void OnToggleControls()
    {
        controlActive = !controlActive;
    }
}