using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject cubeKeyPos;
    [SerializeField] private GameObject pyramidKeyPos;
    [SerializeField] private GameObject sphereKeyPos;

    [SerializeField] private List<Key> keys;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Key>())
        {
            Key key = other.GetComponent<Key>();
            if(key.IsHeld == false)
            {
                key.GetComponent<Rigidbody>().isKinematic = true;

                switch (key.KeyType)
                {
                    case Key.KeyTypes.Cube:
                        key.gameObject.transform.position = Vector3.Lerp(key.transform.position, cubeKeyPos.transform.position, Time.deltaTime);
                        key.gameObject.transform.rotation = Quaternion.Lerp(key.transform.rotation, cubeKeyPos.transform.rotation, Time.deltaTime);
                        key.DeleteText();
                        break;

                    case Key.KeyTypes.Pyramid:
                        key.gameObject.transform.position = Vector3.Lerp(key.transform.position, pyramidKeyPos.transform.position, Time.deltaTime);
                        key.gameObject.transform.rotation = Quaternion.Lerp(key.transform.rotation, pyramidKeyPos.transform.rotation, Time.deltaTime);
                        key.DeleteText();
                        break;

                    case Key.KeyTypes.Sphere:
                        key.gameObject.transform.position = Vector3.Lerp(key.transform.position, sphereKeyPos.transform.position, Time.deltaTime);
                        key.gameObject.transform.rotation = Quaternion.Lerp(key.transform.rotation, sphereKeyPos.transform.rotation, Time.deltaTime);
                        key.DeleteText();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
