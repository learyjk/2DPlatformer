using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    void Start() {
        if (gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;

    public void RespawnPlayer() {
        GetComponent<AudioSource>().Play();
        //yield return new WaitForSeconds(spawnDelay); //also must change method return type to IEnumerator if used
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone.gameObject, 3f);
    }

    public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
        gm.RespawnPlayer();
    }

    public static void KillEnemy(Enemy enemy) {
        Destroy(enemy.gameObject);
    }
}
