using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.SpatialTracking;


namespace AliceReadsGibson {
    public class XRNodeHandManager : MonoBehaviour
    {
        public XRNode nodeType;
        public GameObject containerToDisable;

        [Tooltip("If left null, will attempt to use GetComponent on self")]
        public TrackedPoseDriver tpd = null;

        private Transform m_Transform;

        public float up = 1.0f;
        public float right = 0.3f;
        public float forward = 0.3f;

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

            if (XRSettings.loadedDeviceName == "cardboard") // Force remove both hands on cardboard
            {
                containerToDisable.SetActive(false);
            }
            else if (XRSettings.loadedDeviceName == "daydream")
            {
                m_Transform.localPosition = new Vector3(right, up, forward);
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
                if (nodeState.nodeType == nodeType && nodeState.tracked)
                {
                    setActive = true;
                }
            }
            containerToDisable.SetActive(setActive);
        }
    }
}
