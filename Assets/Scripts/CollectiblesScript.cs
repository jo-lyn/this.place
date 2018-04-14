using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesScript : MonoBehaviour
{
    public ParticleSystem CollectibleEmitter;
    public int OptionalNumber = 0;
    private float _rotateSpeed = 0.3f;
    private ParticleSystem _collectibleEmitterInstantiated;

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, _rotateSpeed);
    }

    private void OnTriggerEnter(Collider colider)
    {
        if (colider.CompareTag("Player"))
        {
            SendMessageUpwards("UpdateScore", this);
            _rotateSpeed += 10f;
            Destroy(gameObject, 1f);

            _collectibleEmitterInstantiated = Instantiate(CollectibleEmitter, transform.position, Quaternion.identity) as ParticleSystem;
            //_collectibleEmitterInstantiated.GetComponent<ParticleSystemRenderer>().material = GetComponent<Renderer>().material;
            _collectibleEmitterInstantiated.Play();
            Destroy(_collectibleEmitterInstantiated, 1.5f);
        }
    }

    public void SetCollected()
    {
        GetComponent<Renderer>().material.color = Color.grey;
    }
}
