using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage; 
    [SerializeField] private GameObject hasHealthBarGameObject;
    // Enemy prefab s�k�nt� ��kmaz fakat player de�il? sahnede atama yapman gerekecek bunu ilerde ��z!

    private IHasHealthBar hasHealthBar; 

    private void Start()
    {

        //Interface serialize field bugu y�z�nden �nce gameobject sonra interface d�n���m� yapt�k
        hasHealthBar = hasHealthBarGameObject.GetComponent<IHasHealthBar>();

        //Elle i�lem yapt���m�z i�in null check
        if (hasHealthBar == null)
        {
            Debug.LogError("Game Object" + hasHealthBarGameObject + " does not have a component that implements IHasHealthBar!");
        }


        hasHealthBar.OnHealthChanged += HasHealthBar_OnHealthChanged;


    }

    private void HasHealthBar_OnHealthChanged(object sender, IHasHealthBar.OnHealthChangedEventArgs e)
    {
        barImage.fillAmount = e.healthNormalized;

    }
}
