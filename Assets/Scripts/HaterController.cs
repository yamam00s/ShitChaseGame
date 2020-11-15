﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (MortionController))]
// [RequireComponent(typeof (StartGameController))]
public class HaterController : MonoBehaviour {
    private GameObject player;
    private MortionController Mortion;
    private float spanTime;
    private float currentTime;

    void Start() {
        player = GameObject.Find("Player");
        Mortion = GetComponent<MortionController>();
        spanTime = 3f;
        currentTime = 0f;
        // 難易度Easyの場合はHaterを表示しない
        if (StartGameController.GetDifficulty() == 0) {
            gameObject.SetActive(false);
        }

        // 難易度で敵の速度変更
        if (StartGameController.GetDifficulty() == 2) {
            spanTime = 2f;
        } else {
            spanTime = 3f;
        }
    }

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
        Mortion.MoveDirection(x, y);

        // 一定時間ごとにプレイヤーに近づく
        currentTime += Time.deltaTime;
        if (currentTime > spanTime) {
            currentTime = 0f;
            Vector2 playerPosition = player.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (collisionInfo.gameObject.tag != "Player") {
            return;
        }

        MainController.setIsGameClear(false);
        SceneManager.LoadScene("Result");
    }
}
