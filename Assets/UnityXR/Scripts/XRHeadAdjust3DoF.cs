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
        TrackedPoseDriver tpd = null;
        
        Transform transformee = null;
        public float playerHeight = 1.6f;

        private void OnEnable()
        {
            if (tpd == null)
                tpd = GetComponent<TrackedPoseDriver>();

            if (transformee == null)
                transformee = GetComponent<Transform>();    

            Debug.Log("VR Device: " + XRSettings.loadedDeviceName);
                        
            if (XRSettings.loadedDeviceName == "daydream" || XRSettings.loadedDeviceName == "cardboard")
            {
                transformee.Translate(Vector3.up * playerHeight);
            }
        }
    }
}
