using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHealthBar
{
    // Bu eventin imzas�n� ta��nlar, eventle arg�manlar�n� buradan ta��y�p HealthBarUI prefab�na iletecek. 

    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs : EventArgs
    {
        public float healthNormalized;
    }


}
