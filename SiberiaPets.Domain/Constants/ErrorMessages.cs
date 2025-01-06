using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaPets.Domain.Constants
{
    public class ErrorMessages
    {
        public const string ErrorAuthentication = "Error de autenticación. Verifique sus credenciales.";
        public const string ErrorAuthorization = "No tiene permiso para realizar esta acción.";
        public const string ErrorDatabaseConnection = "Error de conexión con la base de datos.";
        public const string ErrorRegistryNotFound = "Registro no encontrado.";
        public const string ErrorRegistryAlreadyExist = "Registro ya existe.";
        public const string ErrorEmpty = "Este campo es obligatorio";
        public const string ErrorMaxCharacters = "Caracteres máximos: ";
    }
}
