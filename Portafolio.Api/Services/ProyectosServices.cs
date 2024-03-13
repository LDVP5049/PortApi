using Portafolio.Api.Configurations;
using Portafolio.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Portafolio.Api.Services;
public class ProyectosServices
{
    private readonly IMongoCollection<Mproyectos> _proyectosCollection;
    public ProyectosServices(
        IOptions<DatabaseSettings> databaseSettings)
    {
        //Iniciar mi cliente de mongo
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        //Base de datos de mongo conexion
        var mongoDB =
        mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _proyectosCollection =
        mongoDB.GetCollection<Mproyectos>
            (databaseSettings.Value.CollectionName);
    }
    public async Task<List<Mproyectos>> GetAsync() =>
    await _proyectosCollection.Find(_ => true).ToListAsync();
    //OBTENER CARRERA POR ID
    public async Task<Mproyectos> GetProyectoById(string Id)
    {
        return await _proyectosCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
    }
    //INSERTAR CARRERA
    public async Task InsertProyecto(Mproyectos mproyectos)
    {
        await _proyectosCollection.InsertOneAsync(mproyectos);
    }
    //AQUI PARA EDITAR CARRERA
    public async Task UpdateProyecto(Mproyectos mproyectos)
    {
        var filter = Builders<Mproyectos>.Filter.Eq(s => s.Id, mproyectos.Id);
        await _proyectosCollection.ReplaceOneAsync(filter, mproyectos);
    }
    //BORRAR CARRERA
    public async Task DeleteProyecto(string Id)
    {
        var filter = Builders<Mproyectos>.Filter.Eq(s => s.Id, Id);
        await _proyectosCollection.DeleteOneAsync(filter);
    }
}
