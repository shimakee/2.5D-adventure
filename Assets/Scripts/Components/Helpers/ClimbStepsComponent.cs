using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ICastCollisionChecker), typeof(Rigidbody))]
public class ClimbStepsComponent : MonoBehaviour
{
    [Range(0, 200)][SerializeField] float upForce = 15;
    [Range(0, 200)][SerializeField] float downForce = 15;

    ICastCollisionChecker _castCollisionCheck;
    Rigidbody _rb;
    private void Awake()
    {
        _castCollisionCheck = GetComponent<ICastCollisionChecker>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_castCollisionCheck.TopChecker && _castCollisionCheck.BottomChecker)
        {
            Debug.Log("Up");
            _rb.position += Vector3.up * upForce * Time.deltaTime;
            //_rb.AddForce(Vector3.up * upForceToStep);
        }
        else
        {
            if(!_castCollisionCheck.UnderChecker && !_castCollisionCheck.BottomChecker)
                _rb.position += Vector3.down * downForce * Time.deltaTime;
        }
        //else if (_castCollisionCheck.UnderChecker)
        //{
        //    Debug.Log("Down");
        //    //_rb.AddForce(Vector3.down * upForceToStep);

        //    _rb.position += Vector3.down * upForce * Time.deltaTime;

        //    RaycastHit hitInfo;
        //    var isHit = Physics.Raycast(_rb.position, Vector3.down, out hitInfo, 1, mask);

        //    if (isHit)
        //    {
        //        //_rb.position += (hitInfo.point - _rb.position);
        //        _rb.AddForce(hitInfo.point - _rb.position);
        //    }

        //}
        //_rb.MovePosition( _rb.position + new Vector3(0, upForceToStep, 0));
        //_rb.velocity += Vector3.up * upForceToStep;

    }
}
