using UnityEngine;

public abstract class Edificios : MonoBehaviour
{
    public string nombreEdificio;

    // Al ser abstracto, obliga a todos los edificios hijos a programar qué hacen aquí
    public abstract void Operate();
}