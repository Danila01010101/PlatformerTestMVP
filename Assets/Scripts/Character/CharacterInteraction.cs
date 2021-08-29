using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField]
    public LayerMask _whatIsInteractable;
    [SerializeField]
    private float _interactRadius = 1;
    private void Start()
    {
    }
    private void Update()
    {

    }
    public void FindInteractableObjects()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, _interactRadius, _whatIsInteractable);
        foreach (Collider2D collider in detectedObjects)
        {
                collider.transform.parent.SendMessage("Interact");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _interactRadius);
    }
}
