using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetA, _targetB;   
    [SerializeField]
    private float _speed = 1.0f;
    private bool _switching = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlatForm();


    }

    private void movePlatForm() {
        if (transform.position.x <= _targetA.position.x) {
            _switching = true;
        } 
        else if (transform.position.x >= _targetB.position.x) {
            _switching = false;
        }

        if (_switching) {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        } 
        else {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            other.transform.parent = null;
        }
    }

}
