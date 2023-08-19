using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoosterManager : MonoBehaviour
{
    int spawnMinTime = 5;
    int spawnMaxTime = 10;
    float activeTime = 5f;
    float noticeTime = 2f;
    float boosterOnTime = 5f;

    [SerializeField] float boosterSpeed = 12f;
    [SerializeField] float originalSpeed;

    PlayerController playerController;
    bool isBoosterExists = false;
    bool cycleFinished = false;
    Coroutine spawnCoroutine;

    public GameObject noticeImage;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        originalSpeed = playerController.MoveSpeed;
    }
    private void Update()
    {
        spawnBooster();
    }
    public void spawnBooster()
    {
        //booster does not exists and spawnCoroutine not start yet
        if (!isBoosterExists && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(boosterContinuously(spawnMinTime, spawnMaxTime, activeTime, noticeTime));
        }
        if (cycleFinished)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
            cycleFinished = false;
        }

    }
    IEnumerator boosterContinuously(int spawnMin, int spawnMax, float activeDelay, float noticeDuration)
    {
        int spawnRand = Random.Range(spawnMin, spawnMax);
        yield return new WaitForSeconds(spawnRand);
        isBoosterExists = true;
        noticeImage.SetActive(true);

        yield return new WaitForSeconds(activeDelay);
        noticeImage.SetActive(false);
        isBoosterExists = false;
        cycleFinished = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isBoosterExists)
        {
            playerController.MoveSpeed = boosterSpeed;
            isBoosterExists = false;
            StartCoroutine(playerBoosted(boosterOnTime));
        }
    }
    IEnumerator playerBoosted(float boosterDuration)
    {
        yield return new WaitForSeconds(boosterDuration);
        playerController.MoveSpeed = originalSpeed;
    }
}
