<h1>C#
Gestor de presupuesto</h1>

<i> Esta aplicación ha sido desarrollada para aplicar al programa de aceleración que
brinda alkemy.org a desarrolladores, con el fin de impulsar una carrera profesional en el
mercado laboral. 
<br>Como resultado final, fui seleccionado por el staff de Alkemy para unirme a dicho programa.</i>

<br><h3>Objetivo</h3>
Desarrollar una aplicación para administración de presupuesto personal. La misma debe
permitir crear y editar ingresos y egresos de dinero, y mostrar un balance resultante de las
operaciones registradas.

<br><h3>Requerimientos técnicos</h3>
Deberás utilizar Framework MVC. Los datos deben ser persistidos en una base de datos
relacional. El esquema de la base puede armarse según se considere apropiado en base a
los requerimientos del negocio. El trabajo realizado se subirá a un repositorio.
Pueden utilizarse plantillas para el frontend. La estructura de las vistas debe estar
modularizada en layouts y secciones.

<br><h3>Secciones</h3>
<h4>Objetivo</h4>
La pantalla de inicio deberá mostrar el balance actual, es decir, el resultante de los ingresos y
egresos de dinero cargados, y un listado de los últimos 10 registrados.

<h4>ABM de operaciones (ingresos y egresos)</h4>
La aplicación deberá contener:
<br>● Formulario de registro de operación. El mismo deberá contener:
<br>&#8195;    ○ Concepto
<br>&#8195;    ○ Monto
<br>&#8195;    ○ Fecha
<br>&#8195;    ○ Tipo (ingreso o egreso)
<br>● Listado de operaciones registradas según su tipo (ingreso o egreso).
<br>● Desde el listado, se debe poder modificar o eliminar una operación registrada
  previamente. No debe ser posible modificar el tipo de operación (ingreso o egreso)
  una vez creada.

<br><h3>Bonus</h3>
De forma adicional, podrías agregarle:

<br><h4>Autenticación de usuarios</h4>
Agregar un formulario de registro y login para permitir identificar al usuario que utiliza la
aplicación, y vincular las operaciones registradas al usuario autenticado en el sistema, tanto
para el listado y creación de nuevos registros. Los datos indispensables para permitir el
ingreso deben ser un email y contraseña, pudiendo agregar los que se deseen.

<br><h4>Categorías de operaciones</h4>
Agregar la funcionalidad de categorizar las operaciones registradas en el gestor, como por
ejemplo, una categoría “comida” para categorizar egresos. Adicionalmente, agregar la
posibilidad de listar operaciones por categoría.

<br><h4>Utilización de JavaScript</h4>
Utilizar JavaScript para la comunicación entre las vistas y los Views

<br><h4>Utilización de Patrones de Diseño</h4>
Utilizar de posible el patrón de diseño Unit to Work para los repositorios.

<br><i>Importante: no te preocupes si no podés completar todo, envialo de todas formas que te
tendremos en cuenta igual.</i>

<br><h4>Criterios a evaluar</h4>
  ● El diseño debe ser responsive, pudiendo utilizarse CSS puro o algún framework de Frontend
<br>  ● Código limpio, buenas prácticas de programación, en idioma inglés
<br>  ● Correcto diseño de la base de datos
<br>  ● Buenas prácticas de GIT: Commits declarativos y atomizados
<br>  ● Buenas prácticas para el nombre de rutas
