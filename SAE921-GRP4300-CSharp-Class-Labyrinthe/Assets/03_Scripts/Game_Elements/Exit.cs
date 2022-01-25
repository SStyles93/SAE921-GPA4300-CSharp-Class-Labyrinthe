using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private float exitTime = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMover>())
        {
            ActivateExit();
        }
    }

    //Apply visual effects to the exit and change the scene 
    void ActivateExit()
    {
        StartCoroutine(DelayedChangeScene());
    }

    //Change the scene after a short delay
    IEnumerator DelayedChangeScene()
    {
        yield return new WaitForSecondsRealtime(exitTime);

        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
