# DPEV6
Digitalización de Procesos Electorales y Votaciones (Proyecto Grupo 6)


## *1. Motivo*

VotUca, app multiplataforma de votaciones desarrollada por el grupo 6 en proyectos informáticos, tiene como objetivo controlar las distintas gestiones relacionadas con los sistemas electorales de la UCA. El objetivo de esta aplicación es automatizar los procesos
electorales de la UCA con el objetivo de añadir comodidad y velocidad a dichos procesos.



## *2. App*

Se ha realizado un diseño visual sencillo y funcional para facilitar la usabilidad para el usuario, con tan solo tres botones:
 -Votaciones, para ver y poder votar en las votaciones activas.
 -Votaciones Futuras, para ver las votaciones que están por venir y que el usuario
pueda organizarse.
 -Resultados, para ver el resultado de las votaciones, así como gráficas de tablas, donuts, etc, con los datos.

A continuación podemos ver las capturas del menú de la aplicación con las distintas opciones,
ya sea para un usuario normal o para un usuario con permisos que puede crear votaciones.
![FOTO](https://fotos.subefotos.com/8508537c22ff1495a209ee5e2e386f58o.jpg) 
![FOTO](https://fotos.subefotos.com/fd5d21f114e7573fb88bacf35aa59b19o.jpg)
![FOTO](https://fotos.subefotos.com/fd5d21f114e7573fb88bacf35aa59b19o.jpg)

En la siguiente captura puede verse un ejemplo de la información que se ofrece al usuario
acerca de las votaciones en las que estuvo implicado.

![FOTO](https://fotos.subefotos.com/8fe9627dd0afd856be16b12d489158b6o.jpg)

## *3. Tecnologías*

Trata de una APP multiplataforma con una combinación de frameworks de Microsoft: Xamarin + ASP.NET.
La aplicación desarrollada en Xamarin estará apoyada en un servidor desarrollado en ASP.NET en el cual
se realiza una conexión LDAP con el servidor de la UCA para la autorización y autenticación de los usuarios.
También es donde tienen lugar las operaciones de recuento de votos y demás funcionalidades de la aplicación.
Este servidor a su vez es el que esta conectado con la base de datos relacional que almacenará
toda la información de la aplicación.

![Aqui deberia haber una foto](https://fotos.subefotos.com/6a93c4f3a6374833b8de7f5d08b4e04do.jpg)


## *4. Instalación*

Descargar el apk en nuestro teléfono, o en una máquina virtual. Clickar en ella, y confirmar
la instalación (VotUca no requiere de ningún permiso especial para funcionar).


## *5. FAQs*

*-¿Para qué plataformas existe VotUca?*
Votuca se establece como un sistema multiplataforma, por lo tanto está enfocado a:

	+Windows
	+Android
	+iOS

Aunque a día de hoy (18/01/2020 - GTM+1), su mayor funcionalidad se establece en el sistema Android.
	
*-¿Es posible participar como desarrollador en el proyecto VotUca?*
No, a día de hoy se trata de un proyecto cerrado a un pequeño equipo. Aún así, podéis enviar vuestras sugerencias al correo de soporte: votucaspprt@gmail.com . Estaremos encantados de recibir todo el feedback posible.

*-No puedo abrir el manual de usuario, ¿a qué se debe?*
Es necesario tener vinculado a vuestro dispositivo Android, una cuenta de Gmail para el acceso al manual de usuario, el cual se encuentra establecido en Google Drive.

*-¿Cómo puedo crear una cuenta?*
Ahora mismo la aplicación se encuentra enlazada al servidor de la UCA (18/01/2020 - GTM+1); y por lo tanto, solo aquellos usuarios miembros de la Universidad de Cádiz, pueden acceder a la app.