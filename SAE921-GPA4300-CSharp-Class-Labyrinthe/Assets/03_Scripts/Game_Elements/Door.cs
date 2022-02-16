using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private MusicManager musicManager;

    [SerializeField] private GameObject cubeKeyPos;
    [SerializeField] private GameObject pyramidKeyPos;
    [SerializeField] private GameObject sphereKeyPos;
    [SerializeField] private Vector3 openOffset;

    private Vector3 openPos;
    private Transform doorPanel;

    [SerializeField] private List<Key> keys;

    bool open = false;

    private void Awake()
    {
        doorPanel = transform.GetChild(0);
    }

    void Start()
    {
        openPos = doorPanel.position + openOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            doorPanel.position = Vector3.Lerp(doorPanel.position, openPos, Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Key key = other.GetComponent<Key>();

        if (key && !key.IsHeld)
        {
            key.GetComponent<Rigidbody>().isKinematic = true;

            switch (key.KeyType)
            {
                case Key.KeyTypes.Cube:
                    key.gameObject.transform.position = Vector3.Lerp(key.transform.position, cubeKeyPos.transform.position, Time.deltaTime);
                    key.gameObject.transform.rotation = Quaternion.Lerp(key.transform.rotation, cubeKeyPos.transform.rotation, Time.deltaTime);
                    break;

                case Key.KeyTypes.Pyramid:
                    key.gameObject.transform.position = Vector3.Lerp(key.transform.position, pyramidKeyPos.transform.position, Time.deltaTime);
                    key.gameObject.transform.rotation = Quaternion.Lerp(key.transform.rotation, pyramidKeyPos.transform.rotation, Time.deltaTime);
                    break;

                case Key.KeyTypes.Sphere:
                    key.gameObject.transform.position = Vector3.Lerp(key.transform.position, sphereKeyPos.transform.position, Time.deltaTime);
                    key.gameObject.transform.rotation = Quaternion.Lerp(key.transform.rotation, sphereKeyPos.transform.rotation, Time.deltaTime);
                    break;

                default:
                    break;
            }

            key.DeactivateText();

            if (!key.IsSet)
            {
                //Mark the key as set
                keys.Add(key);
                key.IsSet = true;

                //Play a nice sound effect
                GetComponent<AudioSource>().Play();

                //Open the door if enough keys are placed
                if (keys.Count == 3)
                    OpenDoor();
                else
                {
                    //Stop the music if there's more key to find
                    musicManager.RequestPlay(0);
                }
            }
        }
    }

    void OpenDoor()
    {
        open = true;
    }
}
