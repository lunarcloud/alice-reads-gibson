using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IPointerClickHandler
{
    public string nextScene;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadSceneAsync(nextScene);
    }
    
}
