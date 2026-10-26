using System;
using UnityEngine;

public class Recursos: MonoBehaviour
{
    private static Recursos instance;

    public int dinero = 0;
    public int Trabajadores = 0;

    public event Action<int> EnDineroCambio = (dinero) => { };
    public event Action<int> EnTrabajadoresCambio = (Trabajadores) => { };

    private void Awake()
    {
        instance = this;
    }

    public static Recursos GetInstance()
    {
        return instance;
    }

    public void AgregarDinero(int amount)
    {
        dinero += amount;
        EnDineroCambio(dinero);
    }

    public void AgregarTrabajadores(int amount)
    {
        Trabajadores += amount;
        EnTrabajadoresCambio(Trabajadores);
    }
}