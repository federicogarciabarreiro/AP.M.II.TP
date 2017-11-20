using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Player : MonoBehaviour
{
    public ScriptableObjectConfig data;
    int _speed, _jumpSpeed;
    Rigidbody rb;

    void Start()
    {
        if (data)
        {
            //Debug.Log(data.characterName);
            //Debug.Log(data.hp);
            //Debug.Log("etc...");

            _speed = data.speed;
            _jumpSpeed = data.jumpSpeed;
        }

        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (data)
        {
            rb.AddForce(Input.GetAxis("Horizontal") * _speed * Vector3.right);
            rb.AddForce(Input.GetAxis("Jump") * _jumpSpeed * Vector3.up);
        }
    }
}
