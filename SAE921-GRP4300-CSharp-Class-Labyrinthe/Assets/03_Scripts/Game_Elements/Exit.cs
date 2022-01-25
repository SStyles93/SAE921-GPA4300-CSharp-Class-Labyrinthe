using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private float exitTime = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
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

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
