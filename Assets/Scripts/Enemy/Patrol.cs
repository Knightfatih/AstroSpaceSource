﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Patrol : MonoBehaviour
    {
        public float speed;
        public float startWaitTime;
        private float waitTime;

        public Transform[] moveSpots;
        private int randomSpot;

        private void Start()
        {
            waitTime = startWaitTime;
            randomSpot = Random.Range(0, moveSpots.Length);
        }
        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

