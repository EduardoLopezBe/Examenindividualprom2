using System.Resources;
using UnityEngine;
using UnityEngine.UIElements;

public class Tiendas: Edificios
{
    [SerializeField] private int aforo = 8;
    private int clientesActuales = 0;
    private int StockActual = 100;

    public override void Operate()
    {
        if (StockActual <= 0)
        {
            StockActual = 100;
        }
    }

    public bool TieneAforo()
    {
        return clientesActuales < aforo && StockActual > 0;
    }

    public void ReservarEspacio()
    {
        clientesActuales++;
    }

    public void Vender(Trabajadores trabajador)
    {
        Operate(); // Verifica stock antes de vender

        if (StockActual > 0)
        {
            StockActual--;
            Recursos.GetInstance().AgregarDinero(5); // Gana dinero por la venta
        }

        // Libera el espacio de la tienda
        clientesActuales--;

        // Lo manda a su casa
        trabajador.IrACasa();
    }
}