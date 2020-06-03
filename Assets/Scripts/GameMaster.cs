using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    private static int _remainingLives = 3;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    void Awake() {
        if (gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;
    public string spawnSoundName;

    public CameraShake cameraShake;

    [SerializeField]
    private GameObject gameOverUI;

    //cache
    private AudioManager audioManager;

    void Start() 
    {


        if (cameraShake == null) {
            Debug.LogError("No Camera shake refrenced.");
        }

        //cache
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No Audiomanager found in the sceen.");
        }
    }

    public void EndGame()
    {
        Debug.Log("Game over");
        gameOverUI.SetActive(true);
        _remainingLives = 3;
    }
    
    public void _RespawnPlayer() {
        audioManager.PlaySound(spawnSoundName);
        //yield return new WaitForSeconds(spawnDelay); //also must change method return type to IEnumerator if used
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone.gameObject, 3f);
    }

    public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        } 
        else
        {
            gm._RespawnPlayer();
        }
        
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
