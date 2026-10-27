using UnityEngine;

public class Construir : MonoBehaviour
{
    public GameObject casaPrefab;
    public GameObject fabricaPrefab;
    public GameObject tiendaPrefab;
    public LayerMask capaSuelo;

    private int edificioSeleccionado = 0;

    public void SeleccionarCasa() { edificioSeleccionado = 1; }
    public void SeleccionarFabrica() { edificioSeleccionado = 2; }
    public void SeleccionarTienda() { edificioSeleccionado = 3; }

    private void Update()
    {
        if (edificioSeleccionado != 0 && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, capaSuelo))
            {
                // Toma el centro exacto del bloque de terreno que pusiste en Unity
                Vector3 posicion = hit.collider.transform.position;
                posicion.y = 0; // Evita que flote

                bool ocupado = false;
                foreach (Collider obj in Physics.OverlapSphere(posicion, 1f))
                {
                    if (obj.GetComponent<Edificios>() != null) ocupado = true;
                }

                if (!ocupado)
                {
                    if (edificioSeleccionado == 1) Instantiate(casaPrefab, posicion, Quaternion.identity);
                    if (edificioSeleccionado == 2) Instantiate(fabricaPrefab, posicion, Quaternion.identity);
                    if (edificioSeleccionado == 3) Instantiate(tiendaPrefab, posicion, Quaternion.identity);

                    edificioSeleccionado = 0;
                }
            }
        }
    }
}