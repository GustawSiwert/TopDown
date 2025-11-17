using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public void HurtPlayer(int damageToGive)
    {
        currentHealth -= damageToGive;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            //Här kan vi lägga in annan logik såsom att starta om scenen eller få fram en death/lose UI
        }
    }
}
