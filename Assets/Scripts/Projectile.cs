using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Health target;
    [SerializeField] bool isHoming = false;
    float damage;

    private void Start()
    {
        transform.LookAt(GetAimLocation());
    }
    void Update()
    {
        if (target == null) return;
        if(isHoming &&!target.IsDead())
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }
    private Vector3 GetAimLocation()
    {
        CapsuleCollider capsuleCollider = target.transform.GetComponent<CapsuleCollider>();
       return target != null ? capsuleCollider.height/2*Vector3.up+capsuleCollider.transform.position : target.transform.position ;
    }
    public void SetTarget(Health target,float damage)
    {
        this.target = target;
        this.damage = damage;
        Destroy(gameObject, 10f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enviroment") Destroy(gameObject);
       if(other.GetComponent<Health>() == target )
       {
            if (target.IsDead())
            {
                
                return;
            }
            target.TakeDamage(damage);
            Destroy(gameObject);
       }
        
    }
}
