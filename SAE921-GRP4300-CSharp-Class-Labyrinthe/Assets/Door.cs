using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject cubeKeyPos;
    [SerializeField] private GameObject pyramidKeyPos;
    [SerializeField] private GameObject sphereKeyPos;
    [SerializeField] private Vector3 openOffset;
    private Vector3 openPos;

    [SerializeField] private List<Key> keys;

    bool open = false;

    void Start()
    {
        openPos = transform.position + openOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            transform.position = Vector3.Lerp(transform.position, openPos, Time.deltaTime);
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
                keys.Add(key);
                key.IsSet = true;

                if (keys.Count == 3)
                    OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        open = true;
    }
}
