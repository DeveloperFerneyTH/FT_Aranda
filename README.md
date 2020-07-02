# FT
## Prueba para Aranda Software

### Configuración
* Ejecutar el script **1. Create Database.sql** en la carpeta **SQL** en motor de datos **SQL Server**, Version 2012+. 
* Ejecutar el script **2. Objects and initial Data.sql** en la carpeta **SQL** en motor de datos **SQL Server**, Version 2012+.
* Cambiar la cadena de conexión en el archivo **appsettings.json** del proyecto **FT_Aranda.API**, por la conexión a la base de datos creada anteriormente.
* Se debe dejar como proyectos de inicio **FT_Aranda.API** y **FT_Aranda.WEB**.
* Se debe configurar la cadena de conexión en el archivo **appsettings.json** del proyecto **FT_Aranda.API** en la llave llamada **FTArandaCNN**.
* Se debe configurar la ruta donde se almacenarán los logs de las peticiones apis en el archivo **appsettings.json** del proyecto **FT_Aranda.API** en la llave llamada **PathLog**. 
* Se debe configurar la llave que se utilizara para generar el token en el archivo **appsettings.json** del proyecto **FT_Aranda.API** en la llave llamada **SecretKey**.
* Se debe configurar la url base de los APIs **appsettings.json** del proyecto **FT_Aranda.WEB** en la llave llamada **BaseUrlAPI**.
* Todas las rutas de los metodos apis se almacenan en el archivo de recursos en el proyecto **FT_Aranda.WEB**.
