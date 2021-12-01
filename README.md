# Challenge Backend
Resolución del challenge backend de Alkemy.

## Tecnologías
* ASP.NET Core Web API 5 (C# 9)
* Entity Framework Core 5
* JWT, Identity Entity Framework Core
* AutoMapper
* Swagger

## Migración y ejecución
* Ejecutar en la terminal: 
  * `dotnet ef migrations add "Initial" --project WebAPI -o DataAccess/Migrations`
  * `dotnet ef database update --project WebAPI`
* Iniciar el proyecto desde **IIS Express** para poder ver las imágenes alojadas en la carpeta _**wwwroot**_.

## Observaciones
* La API posee datos sembrados para probarla al hacer la migración. Se encuentran en la clase _**ApplicationDbContextSeed**_.
* La API se puede probar con Swagger, pero al momento de agregar un listado de generos y personajes en el endpoint POST api/Movies lo toma como un listado vacío
  a pesar que personalice el ModelBinder. Pero con Postman, Insomnia u otro cliente HTTP funciona perfectamente.
* Falta el envío de emails y las pruebas unitarias.
