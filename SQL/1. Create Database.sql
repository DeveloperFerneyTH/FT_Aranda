print '--****** Creación de base de datos para la prueba de Aranda. Realizada por Ferney Talero --******'
print '***** Inicio de ejecución ' + Convert(varchar(50), getdate()) + ' *****'
If not Exists(SELECT name FROM master.sys.databases WHERE name = 'FTArandaDB') Begin
	print '**** Creando la base de Datos ****'
	Create database FTArandaDB
End
print '***** Fin de ejecución ' + Convert(varchar(50), getdate()) + ' *****'