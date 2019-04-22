using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.SpatialTracking;


namespace AliceReadsGibson {
    public class XRHeadAdjust3DoF : MonoBehaviour
    {
        private XRNode nodeType = XRNode.Head;

        [Tooltip("If left null, will attempt to use GetComponent on self")]
        public TrackedPoseDriver tpd = null;

        public float playerHeight = 1.6f;

        private Transform m_Transform;

        private void Start()
        {
            if (tpd == null)
                tpd = GetComponent<TrackedPoseDriver>();

            m_Transform = GetComponent<Transform>();
            
            if (XRSettings.loadedDeviceName == "daydream"
            || XRSettings.loadedDeviceName == "cardboard")
            {
                m_Transform.Translate(Vector3.up * playerHeight);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
