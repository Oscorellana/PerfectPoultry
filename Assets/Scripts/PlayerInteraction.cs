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

            // Safety in case someone forgets to add a Rigidbody later
            if (heldRb == null)
            {
                heldRb = heldObject.AddComponent<Rigidbody>();
            }

            heldRb.useGravity = false;
            heldRb.isKinematic = true;

            heldObject.transform.position = holdPoint.position;
            heldObject.transform.rotation = holdPoint.rotation;
            heldObject.transform.parent = holdPoint;
        }
    }

    void DropObject()
    {
        heldRb.useGravity = true;
        heldRb.isKinematic = false;

        heldObject.transform.parent = null;

        heldObject = null;
        heldRb = null;
    }

    void InteractWithHeldItem()
    {
        switch (heldObject.tag)
        {
            case "SaltShaker":
                Debug.Log("You shake salt!");
                GameManager.Instance.TryStep("Salt");
                break;

            case "PepperShaker":
                Debug.Log("You sprinkle pepper!");
                GameManager.Instance.TryStep("Pepper");
                break;

            case "WaterCup":
                Debug.Log("You pour water!");
                GameManager.Instance.TryStep("Water");
                break;

            case "Turkey":
                Debug.Log("You inspect the turkey.");
                GameManager.Instance.TryStep("Turkey");
                break;

            default:
                Debug.Log("This item cannot be used.");
                break;
        }
    }

}
