using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperCyptography
    {
        //CREAMOS UN STRING PARA EL SALT
        public static string Salt { get; set; }
        //MÉTODO PARA GENERAR UN SALT ALEATORIO
        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt="";
            for (int i = 0; i < 17; i++)
            {
                //GENEREMOS UN ALEATORIO
                int num = random.Next(1, 255);
                char letra = Convert.ToChar(num);
                salt += letra;
            }
            return salt;

        }

        public static string EncriptartextoConSalt(string contenido,bool comparar)
        {
            if (comparar == false)
            {
                //NO QUREMOS COMPARAR,SOLO CIFRADO
                //CREAMOS UN NUEVO SALT
                Salt = GenerateSalt();
            }
            //REALIZAMOS EL CIFRADO
            string contenidoSalt = contenido + Salt;
            //UTILIZAMOS EL OBJETO GRANDE PARA CIFRAR
            SHA512 managed = SHA512.Create();
            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] salida;
            salida = encoding.GetBytes(contenidoSalt);

            //REALIZAR n ITERACIONES SOBRE EL PROPIO CIFRADO

            for (int i = 0; i < 77; i++)
            {
                //CIFRADO SOBRE CIFRADO
                salida = managed.ComputeHash(salida);
            }
            //DEBEMOS LIBERAR LA MEMORIA
            managed.Clear();
            string resultado = encoding.GetString(salida);

            return resultado;

        }



        //CREAMOS LOS MÉTODOS DE TIPO ESTÁTICO
        //SIMPLEMENTE VAMOS A DEVOLVER UN TEXTO CIFRADO
        public static string EncriptarTextoBasico(string contenido)
        {
            //EL CIFRADO SE REALIZA A NIVEL DE BYTES
            //DEBEMOS CONVERTIR EL TEXTO DE ENTRADA A BYTES[]
            byte[] entrada;
            //DESPUÉS DE CIFRAR LOS BYTES NOS DARÁ UNA SALIDA DE BYTES[]
            byte[] salida;
            //NECESITAMOS UNA CLASE PARA CONVERTIR DE BYTE A STRING Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS UN OBJETO PARA CIFRAR EL CONTENIDO
            SHA1 managed = SHA1.Create();
            //CONVERTIMOS EL TEXTO A BYTES
            entrada = encoding.GetBytes(contenido);
            //LOS OBJETOS DE CIFRADO TIENEN UN MÉTODO LLAMADO 
            //ComputeHash que reciben un array de bytes,realizan
            //acciones internar y devuelven el array de bytes cifrado
            salida = managed.ComputeHash(entrada);
            //CONVERTIMOS LOS BYTES A TEXTO
            string resultado = encoding.GetString(salida);

            return resultado;
        }








    }
}
