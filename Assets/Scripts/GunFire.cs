using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{

    [SerializeField]
    [Range(0f, 1f)] float fireRate = 0.5f;

    [SerializeField]
    [Range(1, 10)] int damage = 1;

    [SerializeField] private Transform firePoint;

    public ParticleSystem muzzleFire;

    public AudioSource shootingSound;

    [SerializeField]
    [Range(0f, 2f)] float shootingParticleDuration = 1f;

    private float timePassedSinceShot;

    bool isFiring;
    void Start()
    {
        
    }

    void Update()
    {
        timePassedSinceShot += Time.deltaTime;
        if (timePassedSinceShot >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                timePassedSinceShot = 0f;
                FireGun();
                muzzleFire.Play();
            }
            

        }
    }

    private IEnumerator PlayParticleEffect()
    {
        muzzleFire.Play();
        yield return new WaitForSeconds(shootingParticleDuration);
        muzzleFire.Stop();
    
    }

    private void FireGun()
    {
        Debug.DrawRay(firePoint.position, transform.forward * 100, Color.red, 2f);
          StartCoroutine(PlayParticleEffect());
        
        shootingSound.Play();
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            var health = hitInfo.collider.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(damage);

            }
        }
    }
}
