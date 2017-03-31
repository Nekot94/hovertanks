using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public float delay = 1f;
    public int playerNumber = 1;
    public Rigidbody shell;
    public Transform firePlace;
    public Slider aimSlider;
    public AudioSource shootingAudio;
    public AudioClip chargingClip;
    public AudioClip fireClip;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.7f;

    private string fireButton;
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fired;
    private float nextFireTime;

    private void OnEnable()
    {
        currentLaunchForce = minLaunchForce;
        aimSlider.value = minLaunchForce;
    }
    private void Start()
    {
        fireButton = "Fire" + playerNumber;
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }
    private void Update()
    {
        aimSlider.value = minLaunchForce;
        if (currentLaunchForce >= maxLaunchForce && !fired)
        {
            currentLaunchForce = maxLaunchForce;
            Fire(currentLaunchForce, delay);
        }
        else if (Input.GetButtonDown(fireButton))
        {
            fired = false;
            currentLaunchForce = minLaunchForce;
            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }
        else if (Input.GetButton(fireButton) && !fired)
        {
            currentLaunchForce += chargeSpeed * Time.deltaTime;
            aimSlider.value = currentLaunchForce;
        }
        else if (Input.GetButtonUp(fireButton) && !fired)
        {
            Fire(currentLaunchForce, delay);
        }
    }
    public void Fire(float launchForce, float fireRate)
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            fired = true;
            Rigidbody shellInstance = Instantiate(shell, firePlace.position,
                firePlace.rotation) as Rigidbody;
            shellInstance.velocity = launchForce * firePlace.forward;
            shootingAudio.clip = fireClip;
            shootingAudio.Play();
            currentLaunchForce = minLaunchForce;
        }
    }

}
