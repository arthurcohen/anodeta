using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPowerController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] powers;
    public float chanceOfGeneratePower = 0.05f;
    public float secondsToImprovePowerRate = 2f;
    public float powerImprovementAmount = 0.01f;
    private List<int> powersIndexes = new List<int>();
    private float powerRateTimeTracker = 0f;
    private float powerBooster = 0f;

    void Start() {
        for(int i = 0; i < powers.Length; i++) {
            GameObject gameObject = powers[i];
            for (int j = 0; j < gameObject.GetComponent<Power>().weight; j++) 
                powersIndexes.Add(i);
        }
    }

    void Update() {
        checkPowerImprovement();
    }

    private void checkPowerImprovement() {
        powerRateTimeTracker += Time.deltaTime;

        if (powerRateTimeTracker >= secondsToImprovePowerRate) {
            powerRateTimeTracker = 0f;
            powerBooster += powerImprovementAmount;
            Debug.Log("Total power chance = " + (chanceOfGeneratePower + powerBooster));
        }
    }

    public bool canSpawnPower() {
        return UnityEngine.Random.Range(0f, 1f) <= chanceOfGeneratePower + powerBooster;
    }

    public void spawnPower(Vector2 position) {
        int powerIndex = UnityEngine.Random.Range(0, powersIndexes.Count);
        GameObject.Instantiate(powers[powersIndexes[powerIndex]], position, Quaternion.identity);
    }
}
