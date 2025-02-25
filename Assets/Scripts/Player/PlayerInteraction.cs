using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactionDist = 2f;
    [SerializeField] LayerMask interactionMask;
    [SerializeField] Transform itemDropPoint;

    Camera cam;

    private void OnValidate()
    {
        cam = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction();
        }
    }

    void HandleInteraction()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactionDist, interactionMask))
        {
            if (hit.transform.GetComponent<IInteractable>() != null)
            {
                hit.transform.GetComponent<IInteractable>().OnInteract();
            }

        }
    }

    public void DropItem(ItemData item)
    {
        if (item.itemPrefab != null)
            Instantiate(item.itemPrefab, itemDropPoint.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * interactionDist);
    }
}
