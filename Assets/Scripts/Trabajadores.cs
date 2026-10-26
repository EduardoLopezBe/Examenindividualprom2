using UnityEngine;
using UnityEngine.AI;

public class Trabajadores : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform homeDoor;

    public Fabricas miFabrica;
    public Tiendas miTienda;

    // 0 = yendo a fábrica, 1 = yendo a tienda, 2 = yendo a casa
    public int paso = 0;

    public void Initialize(Transform door)
    {
        agent = GetComponent<NavMeshAgent>();
        homeDoor = door;
        IrFabrica();
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (paso == 0 && miFabrica != null)
            {
                miFabrica.Trabajar(this);
                miFabrica = null;
                paso = 1;
            }
            else if (paso == 1 && miTienda != null)
            {
                miTienda.Vender(this);
                miTienda = null;
                paso = 2;
            }
            else if (paso == 2)
            {
                // Llegó a casa, se desactiva y vuelve a salir en 3 segundos
                gameObject.SetActive(false);
                Invoke("GoToFactory", 3f);
            }
        }
    }

    public void IrFabrica()
    {
        paso = 0;
        gameObject.SetActive(true);
        transform.position = homeDoor.position;

        Fabricas[] factories = Object.FindObjectsByType<Fabricas>(FindObjectsSortMode.None);
        foreach (Fabricas f in factories)
        {
            if (f.TieneAforo())
            {
                miFabrica = f;
                miFabrica.ReservarEspacio();
                agent.SetDestination(miFabrica.transform.position);
                return;
            }
            IrACasa();
            Invoke("GoToFactory", 2f);
        }
    }

    public void IrTienda()
    {
        Tiendas[] shops = Object.FindObjectsByType<Tiendas>(FindObjectsSortMode.None);
        foreach (Tiendas s in shops)
        {
            if (s.TieneAforo())
            {
                miTienda = s;
                miTienda.ReservarEspacio();
                agent.SetDestination(miTienda.transform.position);
                return;
            }
        }
        IrACasa();
    }

    public void IrACasa()
    {
        paso = 2;
        agent.SetDestination(homeDoor.position);
    }
}