﻿using System;
using System.Xml.Linq;
using gestionHotel.core.coreClientes;
using gestionHotel.core.coreHabitaciones;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace gestionHotel.core.coreReservas.IO
{
    public class XmlRegistroReservas
    {
        public static string TAG_ROOT="rerservas";
        private static string TAG_RESERVA="reserva";
        private static string TAG_ID="ID_reserva";
        private static string TAG_TIPO="tipo";
        private static string TAG_CLIENTE="cliente";
        private static string TAG_FECHA_ENTRADA="fecha_entrada";
        private static string TAG_FECHA_SALIDA="fecha_salida";
        private static string TAG_GARAJE="garaje";
        private static string TAG_IVA="IVA";
        private static string TAG_IMPORTE_DIA="importe_dia";
        private static string TAG_NUM_HABITACION = "habitacion";
        private static string DEFAULT_FILE_RESERVAS = "reservas.xml";

        private RegistroReservas r;
       
        public XmlRegistroReservas(RegistroReservas x)
        {
            this.r = x;
            
        }

        public static RegistroReservas RecuperarXML(string nf,RegistroHabitaciones h, RegistroClientes c)
        {
            XDocument doc=XDocument.Load(nf);
            return cargarXML(doc.Root,h, c);
        }
        public static RegistroReservas RecuperarXML(RegistroHabitaciones h, RegistroClientes c)
        {
            XDocument doc=XDocument.Load(XmlRegistroReservas.DEFAULT_FILE_RESERVAS);
            return cargarXML(doc.Root,h, c);
        }

        public static RegistroReservas cargarXML(XElement root, RegistroHabitaciones h, RegistroClientes c)
        {
            string rootTag=root?.Name.ToString() ?? "";
            RegistroReservas toret = new RegistroReservas();
            
            if (root != null && rootTag == XmlRegistroReservas.TAG_ROOT)
            {
                var elems = root.Elements(TAG_RESERVA);
                Reserva toAdd;
                foreach (var reserva in elems)
                {
                    
                    int numHabitacion=Int32.Parse(reserva.Element(TAG_NUM_HABITACION).Value); 
                    
                    string dniCliente = reserva.Element(TAG_CLIENTE).Value; 

                    string tipo = reserva.Element(TAG_TIPO).Value;
                    DateTime fechaEntrada=Convert.ToDateTime(reserva.Element(TAG_FECHA_ENTRADA).Value);
                    DateTime fechaSalida=Convert.ToDateTime(reserva.Element(TAG_FECHA_SALIDA).Value);
                    bool garaje=Boolean.Parse(reserva.Element(TAG_GARAJE).Value);
                    int IVA=Int32.Parse(reserva.Element(TAG_IVA).Value);;
                    double importePorDia=Double.Parse(reserva.Element(TAG_IMPORTE_DIA).Value);;
                    
                    
                    Habitacion habitacion = h.BuscarHabitacion(numHabitacion);
                    Cliente cliente = c.BuscarPorDni(dniCliente);
                    
                    toAdd = new Reserva(cliente,habitacion,fechaEntrada,fechaSalida,IVA,garaje,importePorDia,tipo);
                    toret.Add(toAdd);
                }
            }

            return toret;
        }

        public void Guardar(string nf)
        {
            var doc = new XDocument();
            doc.Add(this.ToXML());
            doc.Save(nf);
        }
        public XElement ToXML()
        {
            XElement toret=new XElement(TAG_ROOT);
            
            foreach(var reserva in this.r)
            {
                XElement book = new XElement(TAG_RESERVA);
                
                XElement id = new XElement(TAG_RESERVA,reserva.IdReserva);
                book.Add(id);
                XElement iva = new XElement(TAG_IVA,reserva.IVA);
                book.Add(iva);
                XElement cliente = new XElement(TAG_CLIENTE,reserva.Cliente.Dni);
                book.Add(cliente);
                XElement habitacion = new XElement(TAG_NUM_HABITACION,reserva.Habitacion.Id);
                book.Add(habitacion);
                XElement importaDia = new XElement(TAG_IMPORTE_DIA,reserva.PrecioPorDia);
                book.Add(importaDia);
                XElement garaje = new XElement(TAG_GARAJE,reserva.UsaGaraje);
                book.Add(garaje);
                XElement fechEntrada = new XElement(TAG_FECHA_ENTRADA, reserva.FechaEntrada);
                book.Add(fechEntrada);
                XElement fechaSalida = new XElement(TAG_FECHA_SALIDA, reserva.FechaSalida);
                book.Add(fechaSalida);
                XElement tipo = new XElement(TAG_TIPO, reserva.Tipo);
                book.Add(tipo);
                
                toret.Add(book);
            }
            return toret;
        }
    }

    
}


