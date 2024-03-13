using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Portafolio.Api.Models;

public class Mproyectos
{
    [BsonId] //SE OBTIENE EL ID
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("NombreP")]
    public string NombreP { get; set; } = string.Empty;
    [BsonElement("Descripcion")]
    public string Descripcion { get; set;} = string.Empty;
    [BsonElement("Tecnologias")]
    public string Tecnologias {get; set;} = string.Empty;
}
