using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

/// <summary>
/// Toggle between the direct and ray interactor if the direct interactor isn't touching any objects
/// Should be placed on a ray interactor
/// </summary>
[RequireComponent(typeof(XRRayInteractor))]
public class ToggleRayTeleportAndUI : MonoBehaviour
{
    [Tooltip("Switch even if an object is selected")]
    public bool forceToggle = false;

    [Tooltip("The direct interactor that's switched to")]
    public XRDirectInteractor directInteractor = null;

    [Tooltip("The Line Renderer component to show when interacting with a teleportation anchor or area")]
    public LineRenderer lineRenderer = null;

    private XRRayInteractor rayInteractor = null;
    private bool isSwitched = false;

    private void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        SwitchInteractors(false);
    }

    public void ActivateRay()
    {
        if (!TouchingObject() || forceToggle)
        {
            SwitchInteractors(true);
            if (IsInteractingWithTeleportAnchorOrArea() || IsInteractingWithUI())
            {
                lineRenderer.enabled = true;
            }
        }
    }

    public void DeactivateRay()
    {
        if (isSwitched)
        {
            SwitchInteractors(false);
            lineRenderer.enabled = false;
        }
    }

    private bool TouchingObject()
    {
        List<IXRInteractable> targets = new List<IXRInteractable>();
        directInteractor.GetValidTargets(targets);
        return (targets.Count > 0);
    }

    private void SwitchInteractors(bool value)
    {
        isSwitched = value;
        rayInteractor.enabled = value;
        directInteractor.enabled = !value;
    }

    private bool IsInteractingWithTeleportAnchorOrArea()
    {
        RaycastHit hitInfo;
        bool hit = rayInteractor.TryGetCurrent3DRaycastHit(out hitInfo);
        if (hit)
        {
            TeleportationAnchor teleportAnchor = hitInfo.collider.GetComponent<TeleportationAnchor>();
            TeleportationArea teleportArea = hitInfo.collider.GetComponent<TeleportationArea>();
            return (teleportAnchor != null || teleportArea != null);
        }
        return false;
    }

    private bool IsInteractingWithUI()
    {
        RaycastHit hitInfo;
        bool hit = rayInteractor.TryGetCurrent3DRaycastHit(out hitInfo);
        if (hit)
        {
            GraphicRaycaster graphicRaycaster = hitInfo.collider.GetComponentInParent<GraphicRaycaster>();
            return (graphicRaycaster != null);
        }
        return false;
    }
}
