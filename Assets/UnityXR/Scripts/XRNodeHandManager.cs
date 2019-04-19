using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.SpatialTracking;

public class XRNodeHandManager : MonoBehaviour
{
    public XRNode nodeType;
    public GameObject containerToDisable;

    [Tooltip("If left null, will attempt to use GetComponent on self")]
    public TrackedPoseDriver tpd = null;

    public float playerHeight = 1.6f;

    private Transform m_Transform;

    private void Start()
    {
        if (tpd == null)
            tpd = GetComponent<TrackedPoseDriver>();

        m_Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowOrHide();

        if (XRSettings.loadedDeviceName == "daydream")
        {
            tpd.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
            m_Transform.Translate(Vector3.up * playerHeight);
        }
        else
        {
            tpd.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
        }
    }

    void ShowOrHide()
    {
        List<XRNodeState> nodeStates = new List<XRNodeState>();
        InputTracking.GetNodeStates(nodeStates);

        bool setActive = false;
        foreach (XRNodeState nodeState in nodeStates)
        {
            if (nodeState.nodeType == nodeType)
            {
                setActive = true;
            }
        }
        containerToDisable.SetActive(setActive);
    }
}
