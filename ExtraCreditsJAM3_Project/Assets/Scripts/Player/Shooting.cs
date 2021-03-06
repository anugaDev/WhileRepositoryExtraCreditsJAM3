﻿using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private int shootButton;
    [SerializeField] private PlayerRecorder recorder;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private bool isReloaded;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBetweenShoots ;
    [SerializeField] private float shootingOffset;
    [SerializeField] private AudioClip shootingPlayer;

    private AudioSource playerShootSource;
    private Animator animator;
    private void OnEnable()
    {
        isReloaded = true;
    }
    private void Start()
    {
        playerShootSource = GetComponent<AudioSource>();
        gameCamera = gameCamera == null ? Camera.main : gameCamera;
        isReloaded = true;
        recorder = GetComponent<PlayerRecorder>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(shootButton) && isReloaded)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        playerShootSource.clip = shootingPlayer;
        playerShootSource.Play();
        var bullet = Instantiate(bulletPrefab, transform.position + (GetForwardDir() * shootingOffset), transform.rotation);
        var bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
        bulletBehaviour.SetBullet(bulletSpeed, GetForwardDir(),bullet.GetComponent<Rigidbody2D>());
        recorder.AddShootingRecord();
        StartCoroutine(Reload());
    }
    private Vector3 GetForwardDir()
    {
        var direction = transform.right;
        return direction;
    }
    private IEnumerator Reload()
    {
        isReloaded = false;
        yield return new WaitForSeconds(timeBetweenShoots);
        isReloaded = true;
    }
}
