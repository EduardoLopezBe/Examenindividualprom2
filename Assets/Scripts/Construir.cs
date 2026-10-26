using UnityEngine;

public class Construir : MonoBehaviour
{
    [Header("Referencias")]
    public GestorEspacios gestorEspacios;

    [Header("Prefabs de los Edificios")]
    public GameObject casaPrefab;
    public GameObject fabricaPrefab;
    public GameObject tiendaPrefab;

    [Header("Configuración de Terreno")]
    public LayerMask capaSuelo;

    [Header("Costos de Construcción")]
    public int costoCasa = 50;
    public int costoFabrica = 100;
    public int costoTienda = 80;

    private int edificioSeleccionado = 0;

    public void SeleccionarCasa() { edificioSeleccionado = 1; }
    public void SeleccionarFabrica() { edificioSeleccionado = 2; }
    public void SeleccionarTienda() { edificioSeleccionado = 3; }

    private void Update()
    {
        if (edificioSeleccionado != 0 && Input.GetMouseButtonDown(0))
        {
            ConstruirEdificio();
        }
    }

    private void ConstruirEdificio()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, capaSuelo))
        {
            Vector3 posicionAlineada = gestorEspacios.AjustarACuadricula(hit.point);

            if (!gestorEspacios.EstaOcupado(posicionAlineada))
            {
                int dineroActual = Recursos.GetInstance().dinero;

                if (edificioSeleccionado == 1 && dineroActual >= costoCasa)
                {
                    Instantiate(casaPrefab, posicionAlineada, Quaternion.identity);
                    Recursos.GetInstance().AgregarDinero(-costoCasa);
                    edificioSeleccionado = 0;
                }
                else if (edificioSeleccionado == 2 && dineroActual >= costoFabrica)
                {
                    Instantiate(fabricaPrefab, posicionAlineada, Quaternion.identity);
                    Recursos.GetInstance().AgregarDinero(-costoFabrica);
                    edificioSeleccionado = 0;
                }
                else if (edificioSeleccionado == 3 && dineroActual >= costoTienda)
                {
                    Instantiate(tiendaPrefab, posicionAlineada, Quaternion.identity);
                    Recursos.GetInstance().AgregarDinero(-costoTienda);
                    edificioSeleccionado = 0;
                }
            }
            else
            {
                Debug.Log("Espacio ocupado en esta cuadrícula.");
            }
        }
    }
}