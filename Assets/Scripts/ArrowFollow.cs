using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFollow : MonoBehaviour
{
    public Transform carTransform;
    Vector3 initialArrowPosition;
    Vector3 initialCarPosition;
    Vector3 absoluteInitArrowPosition;
    public MeshRenderer Arrow;

    void Start()
    {
        initialArrowPosition = gameObject.transform.position;
        initialCarPosition = carTransform.position;
        absoluteInitArrowPosition = initialArrowPosition - initialCarPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var go = GameObject.FindWithTag("Client");
        Vector3 _lookDirection = Vector3.zero;
        if (go != null)
        {
            Transform Client = go.transform;
            Arrow.enabled = true;
            _lookDirection = Client.position - transform.position;
        }
        else
        {
            var go2 = GameObject.FindWithTag("Destination");
            if (go2 != null)
            {
                Transform Destination = go2.transform;
                Arrow.enabled = true;
                _lookDirection = (new Vector3(Destination.position.x, Destination.position.y, Destination.position.z)) - transform.position;
            }
            else
            {
                Arrow.enabled = false;
            }
        }

        transform.rotation = Quaternion.LookRotation(_lookDirection.normalized, transform.up);
        transform.position = absoluteInitArrowPosition + carTransform.transform.position;

    }
}
