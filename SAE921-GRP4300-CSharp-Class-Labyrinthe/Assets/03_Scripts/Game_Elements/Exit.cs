using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private float exitTime = 5.0f;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private Transform cameraTargetTransform;

    [SerializeField] private MusicManager musicManager;

    private bool gameOver = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMover>())
        {
            ActivateExit();
        }
    }

    private void Update()
    {
        //Lerp the camera to it's final position
        if (gameOver)
        {
            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, cameraTargetTransform.position, Time.deltaTime);
            cameraObject.transform.rotation = Quaternion.Lerp(cameraObject.transform.rotation, cameraTargetTransform.rotation, Time.deltaTime);
        }
    }

    //Apply visual effects to the exit and change the scene 
    void ActivateExit()
    {
        cameraObject.transform.SetParent(null);

        gameOver = true;

        //Play a win music
        musicManager.RequestPlay(2);

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
