using System.Resources;
using UnityEngine;
using UnityEngine.UIElements;

public class Casas : Edificios
{
    [SerializeField] private GameObject trabajadoresPrefab;
    [SerializeField] private Transform doorSpawnPoint;
    [SerializeField] private int trabajadoresCreados = 4;

    public override void Operate()
    {
        for (int i = 0; i < trabajadoresCreados; i++)
        {
            GameObject trabajadorObj = Instantiate(trabajadoresPrefab, doorSpawnPoint.position, Quaternion.identity);
            Trabajadores trabajador = trabajadorObj.GetComponent<Trabajadores>();
            trabajador.Initialize(doorSpawnPoint);
        }

        Recursos.GetInstance().AgregarTrabajadores(trabajadoresCreados);
    }

    private void Start()
    {
        Operate();
    }
}