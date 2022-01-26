using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;
    AudioSource source;

    bool transitionning = false;
    bool interrupt = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        source.clip = clips[0];
        source.Play();
    }

    public void RequestPlay(int clipId)
    {
        //Check if the clip is valid
        if (clipId >= 0 && clipId >= clips.Count)
        {
            Debug.Log("Incorrect clip id : " + clipId);
            return;
        }

        StartCoroutine(Transition(clipId));
    }

    IEnumerator Transition(int clipId)
    {
        //Force the other transition to end if there was one
        if (transitionning)
            interrupt = true;

        transitionning = true;

        //Decrease first song's volume
        do
        {
            source.volume -= Time.deltaTime;
            yield return null;

            if (interrupt)
            {
                interrupt = false;
                yield break;
            }

        } while (source.volume > 0.05f);

        //Swap the clips
        source.Stop();
        source.clip = clips[clipId];
        source.Play();

        //Increase second song's volume
        do
        {
            source.volume += Time.deltaTime;
            yield return null;

            if (interrupt)
            {
                interrupt = false;
                yield break;
            }
        } while (source.volume < 1.0f);

        transitionning = false;
    }
}
