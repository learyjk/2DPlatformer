using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    void Awake() {
        if (gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;

    public CameraShake cameraShake;

    void Start() {
        if (cameraShake == null) {
            Debug.LogError("No Camera shake refrenced.");
        }
    }
    
    public void _RespawnPlayer() {
        GetComponent<AudioSource>().Play();
        //yield return new WaitForSeconds(spawnDelay); //also must change method return type to IEnumerator if used
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone.gameObject, 3f);
    }

    public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
        gm._RespawnPlayer();
    }

    public static void KillEnemy(Enemy enemy) {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        GameObject clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity).gameObject;
        Destroy(clone, 5f);
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }
}
