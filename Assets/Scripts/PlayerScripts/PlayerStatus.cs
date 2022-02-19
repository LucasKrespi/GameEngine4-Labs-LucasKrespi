using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private HealthBarController healthBar;

    private int Health;
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.updateHealthBarValeu(Health);

    }

    private void OnEnable()
    {
        PlayerEvents.OnTakeDamage += OntakeDamage;

        PlayerEvents.OnUsePot += OnUsePot;
    }

    private void OnDisable()
    {
        PlayerEvents.OnTakeDamage -= OntakeDamage;


        PlayerEvents.OnUsePot -= OnUsePot;
    }

    public void OntakeDamage(int damage)
    {
        Health -= damage;
    }

    public void OnUsePot(int life)
    {
        Health += life;
    }


}
