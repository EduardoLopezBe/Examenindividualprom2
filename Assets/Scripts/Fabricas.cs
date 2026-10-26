using System;
using System.Resources;
using UnityEngine;
using UnityEngine.UIElements;

public class Fabricas : Edificios
{
    [SerializeField] private int Aforo = 12;
    private int TrabajadoresActuales = 0;

    public override void Operate()
    {
        // El operate base puede estar vacío o tener efectos visuales
    }

    public bool TieneAforo()
    {
        return TrabajadoresActuales < Aforo;
    }

    public void ReservarEspacio()
    {
        TrabajadoresActuales++;
    }

    public void Trabajar(Trabajadores trabajador)
    {
        // Func delegado para calcular el pago
        Func<int, int> calculatePay = (w) => 100;
        int paga = calculatePay(1);

        Recursos.GetInstance().AgregarDinero(paga);

        // Libera el espacio en la fábrica
        TrabajadoresActuales--;

        // Manda al trabajador a comprar
        trabajador.IrTienda();
    }
}