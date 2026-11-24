using System;
using NexusSimulation;

namespace ConsoleApp9
{
    public class Enemigo2D
    {
        public Personaje Datos { get; }
        public float X { get; set; }
        public float Y { get; set; }
        public float MinX { get; }
        public float MaxX { get; }
        public bool Derecha { get; set; } = true;
        public float Velocidad { get; }

        public Enemigo2D(Personaje datos, float x, float y,
            float minX, float maxX, float velocidad = 2f)
        {
            Datos = datos;
            X = x;
            Y = y;
            MinX = minX;
            MaxX = maxX;
            Velocidad = velocidad;
        }

        public void Actualizar()
        {
            if (Datos.Salud <= 0)
                return;

            X += Derecha ? Velocidad : -Velocidad;

            if (X >= MaxX)
            {
                X = MaxX;
                Derecha = false;
            }
            else if (X <= MinX)
            {
                X = MinX;
                Derecha = true;
            }
        }
    }
}