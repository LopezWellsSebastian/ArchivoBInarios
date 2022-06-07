using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class Program
    {
        public class ArchivosBinariosEmpleados
        {
            //declaracion de flujos
            BinaryWriter bw = null;//Flujo de salida
            BinaryReader br = null;//Flujo de entrada

            //campos de la clase
            string Nombre, Direccion;
            long telefono;
            int NumEmp, DiasTrabjados;
            float SalarioDiario;

            public void CrearArchivos(string Archivo)
            {
                //variable local al metodo
                char resp;

                try
                {
                    //creacion del flujo para escribirdatos del archivo
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                    //CAPTURA DE DATOS 

                    do
                    {
                        Console.Clear();
                        Console.Write("Numero del Empleado: ");
                        NumEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del Empleado: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Direccion del Empleado: ");
                        Direccion = Console.ReadLine();
                        Console.Write("Telefono del Empleado: ");
                        telefono = long.Parse(Console.ReadLine());
                        Console.Write("Dias Trabajados del Empleado: ");
                        DiasTrabjados = Int32.Parse(Console.ReadLine());
                        Console.Write("Salario Diario del Empleado: ");
                        SalarioDiario = Int32.Parse(Console.ReadLine());


                        //Escribe los datos del archivo
                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Direccion);
                        bw.Write(telefono);
                        bw.Write(DiasTrabjados);
                        bw.Write(SalarioDiario);
                        Console.Write("\n\nDeseas Almacenar otro Registro (s/n)");
                        resp = Char.Parse(Console.ReadLine());
                        
                    } while ((resp =='s')||(resp =='S'));
                }
                catch(IOException e)
                {
                    Console.WriteLine("\nError : " + e.Message);
                    Console.WriteLine("\nRuta :" + e.StackTrace);
                }
                finally
                {
                    if (bw != null) bw.Close(); //Cierra el flujo - escritura
                    Console.Write("\nPresione <enter> para terminar la Escritura de Datos y regresar al Menu");
                    Console.ReadKey();

                }
            }

            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    //verfica si existe el archivo

                    if (File.Exists(Archivo))
                    {

                        //Creacion flujo para leer datos del archivo
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        //Despliegue de datos en la pantalla
                        do
                        {
                            //lectura de registros mientras no llegue a EndOfFile
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            telefono = br.ReadInt64();
                            DiasTrabjados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();

                            //Muestra los datos
                            Console.WriteLine("Numero del empleado                    : " + NumEmp);
                            Console.WriteLine("Nombre del empleado                    : " + Nombre);
                            Console.WriteLine("Direccion del empleado                    : " + Direccion);
                            Console.WriteLine("Telefono del empleado                    : " + telefono);
                            Console.WriteLine("Dias trabajados del empleado                  : " + DiasTrabjados);
                            Console.WriteLine("Salario Diario del empleado                    : " + SalarioDiario);
                            Console.WriteLine("SUELDO TOTAL del empleado                    : {0:C}" + (DiasTrabjados * SalarioDiario));
                        } while (true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n Archivo" + Archivo + "No Existe en el Disco: !");
                        Console.WriteLine("\nPresione <enter> para Continuar...");
                        Console.ReadKey();

                    }
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del Listado de Empleados");
                    Console.Write("\nPresione <enter> para continuar...");
                    Console.ReadKey();
                }
                finally
                {
                    if (br != null) br.Close();
                    Console.WriteLine("\nPresione <Enter> terminar la lectura de datos");
                    Console.ReadKey();

                }
            }
        }
        static void Main(string[] args)
        {
            //Declaración variables auxiliares
            string Arch = null;
            int opcion;

            //Creacion del objeto
            ArchivosBinariosEmpleados Al = new ArchivosBinariosEmpleados();

            //Menú de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n ARCHIVO BINARIO EMPLEADOS ");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo");
                Console.WriteLine("3.- Salida del Programa");
                Console.Write("\nQué opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");
                            Arch = Console.ReadLine();

                            //Verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!!, Deseas Sobreescribirlo (s/n)? ");
                                resp = char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                            {
                                Al.CrearArchivos(Arch);
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque de lectura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas leer: ");
                            Arch = Console.ReadLine();
                            Al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <ENTER> para Salir del Programa...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opción no Existe !!, Presione <ENTER> para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }

    }
 }
    

