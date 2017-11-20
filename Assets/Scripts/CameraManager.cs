using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public List<Vector3> positions;
    private List<Vector3> _positions;
    private Vector3 _currentPosToGo;
    public Vector3 lookPos = new Vector3(15, 0, 15);
    public float speed;
    public Camera playerCamera;
    Camera _thisCamera;

    private void Awake()
    {
        _positions = new List<Vector3>();

        for (int i = 0; i < positions.Count; i++) _positions.Add(positions[i]);

        _thisCamera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (playerCamera)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _thisCamera.enabled = !_thisCamera.enabled;
                playerCamera.enabled = !playerCamera.enabled;
            }
        }

        Move();
        Look();
    }

    public void AddValue()
    {
        if (positions == null) positions = new List<Vector3>();

        positions.Add(new Vector3(0,0,0));
    }

    public void Move()
    {
        if (Vector3.Distance(transform.position,_currentPosToGo) < 3f)
        {
            if (_positions.Count > 0)
            {
                _currentPosToGo = _positions[0];
                _positions.Remove(_positions[0]);
                _positions.Add(_currentPosToGo);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentPosToGo, Time.deltaTime * speed);
    }

    public void Look()
    {
        transform.LookAt(lookPos);
    }

    public void RemoveValue()
    {
        if (positions.Count > 0) positions.RemoveAt(positions.Count - 1);
    }

    public void ResetAll()
    {
        positions = new List<Vector3>();
        speed = 0;
        lookPos = Vector3.zero;
    }
}
