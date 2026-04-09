using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class UI : MonoBehaviour
{
    //public GameObject PauseMenu, MainMenu, LevelController, Level0;
    public Slider HealthBar, ManaBar, ExpBar;
    public TMP_Text HealthText, ManaText, ExpText; 

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float health, float MaxHp)
    {
        HealthText.text = "HP: " + health + " / " + MaxHp;
        HealthBar.value = health;
        HealthBar.maxValue = MaxHp;
    }

    public void UpdateMana(float mana, float MaxMana)
    {
        ManaText.text = "Mana: " + mana + " / " + MaxMana;
        ManaBar.value = mana;
        ManaBar.maxValue = MaxMana;
    }

    public void UpdateExp(float exp, float expRequired)
    {
        ExpText.text = "Exp: " + exp + " / " + expRequired;
        ExpBar.value = exp;
        ExpBar.maxValue = expRequired;
    }
}