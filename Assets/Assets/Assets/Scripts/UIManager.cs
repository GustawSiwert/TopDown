using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private HealtManager healthManager;
    public Slider healthBar;
    void Start()
    {
        healthManager = FindFirstObjectByType<HealtManager>();
    }
    void Update()
    {
        healthBar.maxValue = healthManager.maxHealth;
        healthBar.value = healthManager.currentHealth;
    }
}
