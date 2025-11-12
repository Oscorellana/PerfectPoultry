using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform holdPoint;
    public float pickupRange = 3f;
    public LayerMask pickupLayer;

    private GameObject heldObject;
    private Rigidbody heldRb;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
                TryPickup();
            else
                DropObject();
        }

        if (heldObject != null && Input.GetKeyDown(KeyCode.I))
        {
            InteractWithHeldItem();
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
        {
            heldObject = hit.collider.gameObject;
            heldRb = heldObject.GetComponent<Rigidbody>();
            heldRb.useGravity = true;
            heldRb.isKinematic = true;
            heldObject.transform.position = holdPoint.position;
            heldObject.transform.parent = holdPoint;
        }
    }

    void DropObject()
    {
        heldRb.useGravity = false;
        heldRb.isKinematic = false;
        heldObject.transform.parent = null;
        heldObject = null;
        heldRb = null;
    }

    void InteractWithHeldItem()
    {
        // Example interactions
        if (heldObject.CompareTag("SaltShaker"))
        {
            Debug.Log("You shake some salt!");
        }
        else if (heldObject.CompareTag("WaterCup"))
        {
            Debug.Log("You pour some water!");
        }
        else
        {
            Debug.Log("This item can't be used.");
        }
    }
}
