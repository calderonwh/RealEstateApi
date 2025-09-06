
# RealEstateApi

API RESTful desarrollada en .NET 8 para la gestión de propiedades inmobiliarias. Permite crear, actualizar y consultar propiedades, así como gestionar sus imágenes.

---

## Tecnologías Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server 2022 Express](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
- [NUnit](https://nunit.org/) + [InMemory DB](https://learn.microsoft.com/en-us/ef/core/testing/in-memory)
- [Swagger / OpenAPI](https://swagger.io/)
- Visual Studio 2022+

---

## Requisitos

- Visual Studio 2022 (preferiblemente con workload “ASP.NET y desarrollo web”)
- SQL Server 2022 Express o superior
- .NET 8 SDK
- Git (opcional)

---

## Instalación y Ejecución

1. **Clonar el repositorio o descomprimir el .zip**

   ```bash
   git clone https://github.com/calderonwh/RealEstateApi.git
   ```

2. **Abrir la solución en Visual Studio**

   ```
   RealEstateApi.sln
   ```

3. **Configurar la cadena de conexión en `appsettings.json`**

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\SQLEXPRESS;Database=RealEstateDb;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```

4. **Restaurar paquetes NuGet y compilar el proyecto**

   Visual Studio lo hará automáticamente al abrir la solución. Si no:

   ```bash
   dotnet restore
   ```

5. **Aplicar migraciones y crear la base de datos (opcional)**

   ```bash
   dotnet ef database update
   ```

6. **Ejecutar la API**

   Presiona `F5` en Visual Studio o usa:

   ```bash
   dotnet run --project RealEstateApi
   ```

7. **Probar en Swagger (auto-generado)**

   - Navega a: [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

## Ejecutar Pruebas Unitarias

Las pruebas unitarias se encuentran en el proyecto `RealEstateApi.Tests`.

Para ejecutar:

```bash
dotnet test RealEstateApi.Tests
```

O desde Visual Studio:
- Menú superior → Prueba → Ejecutar todas las pruebas
- O abre la ventana "Explorador de pruebas"

---

## Estructura del Proyecto

```
├── RealEstateApi/
│   ├── Controllers/            // Controladores API REST
│   ├── Services/               // Lógica de negocio (PropertyService)
│   ├── Dtos/                   // DTOs de entrada/salida
│   ├── Models/                 // Entidades EF Core
│   ├── Data/                   // DbContext y configuración EF
│   └── Program.cs              // Configuración y arranque de la API
│
├── RealEstateApi.Tests/        // Proyecto NUnit
│   └── PropertyServiceTests.cs
│
└── README.md
```

---

## Funcionalidades Implementadas

- * Crear propiedad
- * Actualizar propiedad
- * Actualizar solo el precio
- * Agregar imágenes
- * Consultar propiedades con filtros (`priceMin`, `priceMax`, `address`)

---

## Entrega

Este proyecto ha sido desarrollado como parte del proceso técnico para la vacante **Senior .NET Developer** de **MillionLuxury**.


## Autor

**William Calderon**  
Ingeniero de Sistemas | Backend Developer  

---
