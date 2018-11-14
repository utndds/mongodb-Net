using ejemploMongo.modelo;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejemploMongo
{
    class Program
    {
        static void Main(string[] args)
        {
               
            // Declaro clases que quiero que se serializen antes de usarlas
            BsonClassMap.RegisterClassMap<Usuario>();
            BsonClassMap.RegisterClassMap<Log>();


            // Conexion con MongoDB y eleccion de DB
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("dbEjemplo");


            // Creo y agrego un usuario
            registrarUsuario(database, "probando", "probando");
            Console.WriteLine("Usuario creado");

            // Inicio sesion como usuario
            iniciarSesion(database, "probando", "probando");
            Console.WriteLine("Usuario inicio sesion");

            // Evita fin de ejecucion del programa
            Console.ReadKey();

        }

        public static void registrarUsuario(IMongoDatabase database, string _usuario, string _password){

            // Traigo la coleccion
            var usuarios = database.GetCollection<Usuario>("usuarios");

            // Creo un usuario y lo inserto
            var usuario = new Usuario(_usuario, _password);
            usuarios.InsertOne(usuario);

        }

        public static void iniciarSesion(IMongoDatabase database, string _usuario, string _password)
        {

            // Construyo filtro de busqueda
            var builder = Builders<Usuario>.Filter;
            var filter = builder.Eq("username", _usuario) & builder.Eq("password", _password);

            // Traigo la coleccion
            var usuarios = database.GetCollection<Usuario>("usuarios");

            // Busco usando el filtro y convierto los resultados a lista
            var usuariosEncontrados = usuarios.Find<Usuario>(filter);
            var listaUsuarios = usuariosEncontrados.ToList<Usuario>();
            var usuario = listaUsuarios[0];

            usuario.login();
            
            // Actualizo el objeto en MongoDB
            usuarios.ReplaceOne(filter, usuario);

        }
    }
}
