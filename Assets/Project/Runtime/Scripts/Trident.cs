using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody tridentBody;
    private BoxCollider capsuleCollider;

    public void Start()
    {      
        tridentBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<BoxCollider>();
        tridentBody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Bread")
        {
            tridentBody.isKinematic = true;
            tridentBody.velocity = Vector3.zero;
            capsuleCollider.enabled = false;
        }
    }
}
