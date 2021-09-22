# ChallengeBackend
Resolución del challenge backend de Alkemy.

## Tecnologías
* ASP.NET Core Web API 5 (C# 9)
* Entity Framework Core 5
* AutoMapper
* Swagger

## Instrucciones
### Visual Studio
* Instalar el SDK .NET 5
* Dirigirse a la consola del Administrador de paquetes NuGet y ejecutar `Add-Migration "SampleMigration" -o DataAccess\Migrations` y luego `Update-Database`
* Iniciar el proyecto desde **IIS Express**

### Observaciones
* La API posee datos sembrados para probarla al hacer la migración. Se encuentran en la clase **ApplicationDbContextSeed**.
* Filtros de búsqueda:
  * Generos: Nombre.
  * Películas: Título, genero (nombre), fecha de estreno (asc, desc).
  * Personajes: Nombre, película (nombre).
* La API se puede probar con Swagger, pero al momento de agregar un listado de generos y personajes en el endpoint POST api/Movie lo toma como un listado vacío
  a pesar que personalice el ModelBinder. Pero con Postman o Insomnia REST, funciona perfectamente.
* Falta el envío de emails y las pruebas unitarias.
