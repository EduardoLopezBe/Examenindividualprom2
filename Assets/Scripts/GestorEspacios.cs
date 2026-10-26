using UnityEngine;

public class GestorEspacios : MonoBehaviour
{
    [Header("Configuración de la Ciudad")]
    public float tamañoCuadricula = 5f;
    public float radioComprobacion = 2f;

    public Vector3 AjustarACuadricula(Vector3 posicionHit)
    {
        float x = Mathf.Round(posicionHit.x / tamañoCuadricula) * tamañoCuadricula;
        float z = Mathf.Round(posicionHit.z / tamañoCuadricula) * tamañoCuadricula;

        return new Vector3(x, posicionHit.y, z);
    }

    public bool EstaOcupado(Vector3 posicion)
    {
        Collider[] objetosCercanos = Physics.OverlapSphere(posicion, radioComprobacion);

        foreach (Collider obj in objetosCercanos)
        {
            if (obj.GetComponent<Edificios>() != null)
            {
                return true;
            }
        }
        return false;
    }
}