# Estructuras de Datos

## Datos del alumno

Nombre: Sebastián Durán Palma

Número de control: 25410172

Carrera: Ingeniería en Sistemas Computacionales

Materia: Estructuras de Datos

Docente: Myrna Villegas Gaytán

Semestre: 3°


## Proyecto 01

### Nombre

Sistema de Atención en Clínica (Cola con Prioridad)

### Descripción

Programa desarrollado en C# que gestiona el registro y la atención de pacientes en una clínica mediante una estructura de datos lineal dinámica (cola con prioridad implementada con nodos enlazados). Permite registrar pacientes asignándoles expediente, nombre, edad y nivel de prioridad, ordenarlos automáticamente para atender primero a los de mayor prioridad, desencolar al siguiente paciente en turno y visualizar la lista de espera actual desde un menú en consola.

### Lenguaje

C#

### IDE

Visual Studio

### Conceptos utilizados

- Estructuras de datos lineales (Colas y Colas con Prioridad)
- Nodos y referencias enlazadas (`frente` y `final`)
- Programación Orientada a Objetos (Clases `Paciente`, `Nodo`, `Cola`)
- Sobreescritura de métodos (`ToString`)
- Estructuras de control (`do-while`, `while`, `switch`, condicionales)
- Validación de entrada de datos (`int.TryParse`)

## Proyecto 02

### Nombre

Asignación de Mesas en Restaurante (Cola Simple / FIFO)

### Descripción

Programa desarrollado en C# que administra la lista de espera de clientes en un restaurante mediante una estructura de datos lineal dinámica de tipo cola simple (FIFO - First In, First Out) implementada con nodos enlazados. Permite registrar clientes indicando su nombre y la cantidad de personas, asignar mesas respetando estrictamente el orden de llegada y visualizar la fila de espera actual desde un menú en consola.

### Lenguaje

C#

### IDE

Visual Studio

### Conceptos utilizados

- Estructuras de datos lineales (Colas simples / FIFO)
- Nodos y referencias enlazadas (`frente` y `final`)
- Programación Orientada a Objetos (Clases `Cliente`, `Nodo`, `Cola`)
- Sobreescritura de métodos (`ToString`)
- Estructuras de control (`do-while`, `while`, `switch`, condicionales)
- Validación de entrada de datos (`int.TryParse` y validación de cantidad positiva)

## Proyecto 03

### Nombre

Simulador de Llamadas en Espera (Pila / LIFO)

### Descripción

Programa desarrollado en C# que simula la gestión de llamadas telefónicas activas y en espera mediante una estructura de datos lineal dinámica de tipo pila (LIFO - Last In, First Out) implementada con nodos enlazados. Permite iniciar nuevas llamadas colocando automáticamente la llamada en curso en espera, colgar la llamada activa calculando su duración exacta y reanudando la llamada anterior, consultar la llamada actual junto con el total en espera y visualizar toda la pila desde un menú en consola.

### Lenguaje

C#

### IDE

Visual Studio

### Conceptos utilizados

- Estructuras de datos lineales (Pilas / LIFO)
- Nodos y referencias enlazadas (`tope`)
- Operaciones fundamentales de pila (`Push`, `Pop`, `Peek`)
- Programación Orientada a Objetos (Clases `Llamada`, `Nodo`, `Pila`)
- Manejo de fechas y cálculo de duración (`DateTime` y `TimeSpan`)
- Sobreescritura de métodos (`ToString`)
- Validación de entrada de datos (`int.TryParse` y `string.IsNullOrWhiteSpace`)

## Proyecto 04

### Nombre

Calculadora de Expresiones Matemáticas (Notación Infija a Postfija)

### Descripción

Programa desarrollado en C# que analiza, convierte y evalúa expresiones matemáticas mediante una estructura de datos lineal dinámica de tipo pila genérica (`Pila<T>`) implementada con nodos enlazados. El sistema tokeniza la expresión ingresada (soportando números decimales, negativos, paréntesis y los operadores `+`, `-`, `*`, `/`, `^`), la transforma de notación infija a notación postfija respetando la jerarquía y asociatividad de operadores, y finalmente calcula el resultado exacto controlando errores como paréntesis desbalanceados o división entre cero.

### Lenguaje

C#

### IDE

Visual Studio

### Conceptos utilizados

- Estructuras de datos lineales (Pilas genéricas `Pila<T>`)
- Genéricos en C# y nodos privados anidados (`cima`)
- Operaciones fundamentales de pila (`Apilar`, `Desapilar`, `tope`)
- Conversión de expresiones (Notación infija a postfija)
- Evaluación de expresiones en notación postfija y precedencia de operadores
- Tokenización de cadenas de texto y colecciones (`List<string>`)
- Manejo de excepciones (`try-catch` y `throw`) y cultura invariante (`CultureInfo.InvariantCulture`)

## Proyecto 05

### Nombre

Simulador de Historial de Navegador Web (Doble Pila)

### Descripción

Programa desarrollado en C# que simula la navegación web y el historial de páginas visitadas mediante el uso simultáneo de dos estructuras de datos lineales dinámicas de tipo pila (`atras` y `adelante`) implementadas con nodos enlazados. Partiendo desde una página inicial (`Inicio`), permite visitar nuevas direcciones web guardando la página actual en el historial previo, retroceder a páginas anteriores y avanzar nuevamente entre ellas desde un menú interactivo en consola.

### Lenguaje

C#

### IDE

Visual Studio

### Conceptos utilizados

- Estructuras de datos lineales (Pilas / LIFO)
- Gestión simultánea de múltiples pilas (`atras` y `adelante`)
- Nodos privados anidados y referencias enlazadas (`tope`)
- Operaciones fundamentales de pila (`Push` y `Pop`)
- Programación Orientada a Objetos (Clase `Pila` en el espacio de nombres `ProyectoNavegador`)
- Estructuras de control (`do-while`, `switch`, condicionales)
- Entrada y salida de datos por consola (`Console.ReadLine` e `int.Parse`)
