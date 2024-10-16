using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Scattering Prefab")]
    [SerializeField] private Rigidbody _prefabToScatter;
    [SerializeField] private int _numberOfInstances = 10;
    [SerializeField] private float _scatterRadius = 2f;
    [SerializeField] private float _scatterForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionPoint = collision.contacts[0].point;
        Vector3 playerPosition = FindObjectOfType<PlayerController>().transform.position;
        Vector3 directionToPlayer = (playerPosition - collisionPoint).normalized;

        for (int i = 0; i < _numberOfInstances; i++) {

            Vector3 randomOffset = new Vector3(
                Random.Range(-_scatterRadius, _scatterRadius),
                Random.Range(-_scatterRadius, _scatterRadius),
                Random.Range(0,_scatterRadius)
            );

            Vector3 scatterPositon = collisionPoint + randomOffset + directionToPlayer * _scatterRadius * 0.5f;

            Rigidbody scatteredInstance = Instantiate(_prefabToScatter, scatterPositon, Quaternion.identity);

            Vector3 scatterDirection = (playerPosition - scatterPositon).normalized + Random.insideUnitSphere * 0.1f;
            scatteredInstance.AddForce(scatterDirection * _scatterForce, ForceMode.Impulse);

            Destroy(scatteredInstance.gameObject, 5.0f);


        }

        

    }

}
