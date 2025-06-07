using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage; 
    [SerializeField] private GameObject hasHealthBarGameObject;
    // Enemy prefab sýkýntý çýkmaz fakat player deðil? sahnede atama yapman gerekecek bunu ilerde çöz!

    private IHasHealthBar hasHealthBar; 

    private void Start()
    {

        //Interface serialize field bugu yüzünden önce gameobject sonra interface dönüþümü yaptýk
        hasHealthBar = hasHealthBarGameObject.GetComponent<IHasHealthBar>();

        //Elle iþlem yaptýðýmýz için null check
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
