using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class GameManager : MonoBehaviour
{
    public GameObject spawnAreaPoint;
    public GameObject gameOverScreen;
    public TextMeshProUGUI score;
    public Slider Slider;
    public float scatter;
    public float startObjCooldown;
    public float minObjCooldown;
    public float healChance = 0.1f;
    public GameObject[] enemies;
    public GameObject[] heals;
    public float difficultyIncrement;
    public float playerHp = 3;

    [SerializeField] private float _objTimer;
    private float _currentObjCooldown;
    private bool _over = false;
    public int playerScore = 0;


    private void Start()
    {
        _objTimer = startObjCooldown;
        _currentObjCooldown = startObjCooldown;
    }


    private void Update()
    {
        if (_over)
        {
            return;
        }

        _objTimer -= Time.deltaTime;


        if (_objTimer < 0)
        {
            SpawnObject();
            _objTimer = _currentObjCooldown;
            if (_currentObjCooldown > minObjCooldown)
                _currentObjCooldown *= difficultyIncrement;
        }
    }

    private void SpawnObject()
    {
        GameObject obj = null;
        if (Random.Range(0f, 1.1f) > healChance)
        {
            obj = enemies[Random.Range(0, enemies.Length)];
        }
        else
        {
            obj = heals[Random.Range(0, heals.Length)];
        }

        Vector3 position = spawnAreaPoint.transform.position;
        position.x += Random.Range(-scatter, scatter);
        GameObject spawned = Instantiate(obj, position, Quaternion.identity);
        spawned.GetComponentInChildren<Rigidbody2D>().AddTorque(Random.Range(-20f,20f));
    }

    public void IncreaseScore(int x)
    {
        playerScore += x;
        score.text = "Score" + playerScore;
    }

    public void ChangeHp(int x)
    {
        if (x > 0 && playerHp > 2)
        {
            return;
        }

        playerHp += x;


        UpdateHp();
        if (playerHp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _over = true;
        gameOverScreen.SetActive(true);
    }

    private void UpdateHp()
    {
        Slider.value = playerHp;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}