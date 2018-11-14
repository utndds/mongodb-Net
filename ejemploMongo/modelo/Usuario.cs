using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejemploMongo.modelo
{
    class Usuario{
        
        [BsonId]
        public ObjectId ID { get; set; }

        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("password")]
        public string password { get; set; }

        [BsonElement("logs")]
        public List<Log> logs = new List<Log>();

        public Usuario(string username, string password) {
            this.username = username;
            this.password = password;
            this.logs.Add(new Log("Se ha registrado", DateTime.UtcNow));
        }

        public void login()
        {
            this.logs.Add(new Log("Se ha loggeado", DateTime.UtcNow));
        }


    }
}
