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

        private TrackedPoseDriver tpd = null;

        private Transform m_Transform;

        public float up = 1.2f;
        public float right = 0.3f;
        public float forward = 0.5f;

        public bool hideIfNotTracked = true;

        private void Start()
        {
            tpd = GetComponent<TrackedPoseDriver>();
            nodeType = tpd.poseSource == TrackedPoseDriver.TrackedPose.RightPose ? XRNode.RightHand : XRNode.LeftHand ;

            m_Transform = GetComponent<Transform>();

            if (XRSettings.loadedDeviceName == "daydream") 
                m_Transform.localPosition = new Vector3(right, up, forward);
        }

        // Update is called once per frame
        void Update()
        {
            if (hideIfNotTracked) {
                ShowOrHide();
            }
        }

        bool ShowOrHide()
        {
            List<XRNodeState> nodeStates = new List<XRNodeState>();
            InputTracking.GetNodeStates(nodeStates);

            bool setActive = false;
            foreach (XRNodeState nodeState in nodeStates) {
                if (nodeState.nodeType == nodeType) {
                    setActive = nodeState.tracked;
                }
            }
            containerToDisable.SetActive(setActive);
            return setActive;
        }
    }
}
