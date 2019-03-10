using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class FloorTransporter : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        var worldPos = eventData.pointerPressRaycast.worldPosition;

        if (NavMesh.SamplePosition(worldPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            var player = GameObject.Find("Player");
            var characterController = player.GetComponent<CharacterController>();
            var height = characterController?.height ?? 1.6f;

            GameObject.Find("Player").transform.position = hit.position + Vector3.up * height/2;
        }
    }
}
