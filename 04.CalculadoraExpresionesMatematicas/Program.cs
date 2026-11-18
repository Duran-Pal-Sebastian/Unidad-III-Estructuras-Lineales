using System;
using System.Collections.Generic;
using System.Globalization;

namespace CalculadoraMatematicas
{
    internal class Program
    {
        static int Prioridad(char operador)
        {
            if (operador == '+' || operador == '-')
                return 1;

            if (operador == '*' || operador == '/')
                return 2;

            if (operador == '^')
                return 3;

            return 0;
        }

        static bool EsOperador(char c)
        {
            return "+-*/^".Contains(c);
        }

        static bool EsNumero(string token)
        {
            return token.Length > 1 || char.IsDigit(token[0]) || token[0] == '.';
        }

        static List<string> Tokenizar(string expresion)
        {
            List<string> tokens = new List<string>();
            int i = 0;

            while (i < expresion.Length)
            {
                char c = expresion[i];

                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                
                string ultimo = tokens.Count > 0 ? tokens[tokens.Count - 1] : null;
                bool esperaOperando = ultimo == null ||
                                      ultimo == "(" ||
                                      (ultimo.Length == 1 && EsOperador(ultimo[0]));

                bool negativo = c == '-' &&
                                esperaOperando &&
                                i + 1 < expresion.Length &&
                                (char.IsDigit(expresion[i + 1]) || expresion[i + 1] == '.');

                if (char.IsDigit(c) || c == '.' || negativo)
                {
                    int inicio = i;
                    i++;

                    while (i < expresion.Length &&
                           (char.IsDigit(expresion[i]) || expresion[i] == '.'))
                    {
                        i++;
                    }

                    tokens.Add(expresion.Substring(inicio, i - inicio));
                }
                else if (c == '(' || c == ')' || EsOperador(c))
                {
                    tokens.Add(c.ToString());
                    i++;
                }
                else
                {
                    throw new Exception($"Carácter no válido: '{c}'");
                }
            }

            return tokens;
        }

        static List<string> InfijaAPostfija(List<string> tokens)
        {
            Pila<char> pila = new Pila<char>();
            List<string> salida = new List<string>();

            foreach (string token in tokens)
            {
                char c = token[0];

                if (EsNumero(token))
                {
                    salida.Add(token);
                }
                else if (c == '(')
                {
                    pila.Apilar(c);
                }
                else if (c == ')')
                {
                    while (!pila.Vacia() && pila.tope() != '(')
                    {
                        salida.Add(pila.Desapilar().ToString());
                    }

                    if (pila.Vacia())
                        throw new Exception("Paréntesis desbalanceados: falta '('.");

                    pila.Desapilar();
                }
                else
                {
                    
                    bool derecha = c == '^';

                    while (!pila.Vacia() &&
                           pila.tope() != '(' &&
                           (Prioridad(pila.tope()) > Prioridad(c) ||
                            (Prioridad(pila.tope()) == Prioridad(c) && !derecha)))
                    {
                        salida.Add(pila.Desapilar().ToString());
                    }

                    pila.Apilar(c);
                }
            }

            while (!pila.Vacia())
            {
                char operador = pila.Desapilar();

                if (operador == '(')
                    throw new Exception("Paréntesis desbalanceados: falta ')'.");

                salida.Add(operador.ToString());
            }

            return salida;
        }

        static double Evaluar(List<string> postfija)
        {
            Pila<double> pila = new Pila<double>();

            foreach (string token in postfija)
            {
                if (EsNumero(token))
                {
                    double numero;

                    if (!double.TryParse(token, NumberStyles.Float,
                                         CultureInfo.InvariantCulture, out numero))
                    {
                        throw new Exception($"Número no válido: {token}");
                    }

                    pila.Apilar(numero);
                }
                else
                {
                    if (pila.Vacia())
                        throw new Exception("Expresión inválida.");
                    double b = pila.Desapilar();

                    if (pila.Vacia())
                        throw new Exception("Expresión inválida.");
                    double a = pila.Desapilar();

                    switch (token[0])
                    {
                        case '+':
                            pila.Apilar(a + b);
                            break;
                        case '-':
                            pila.Apilar(a - b);
                            break;
                        case '*':
                            pila.Apilar(a * b);
                            break;
                        case '/':
                            if (b == 0)
                                throw new Exception("No se puede dividir entre cero.");
                            pila.Apilar(a / b);
                            break;
                        case '^':
                            pila.Apilar(Math.Pow(a, b));
                            break;
                    }
                }
            }

            if (pila.Vacia())
                throw new Exception("Expresión inválida.");

            double resultado = pila.Desapilar();

            if (!pila.Vacia())
                throw new Exception("Expresión inválida.");

            return resultado;
        }

        static void Main()
        {
            Console.WriteLine("======== CALCULADORA ========");
            Console.WriteLine("Operadores: + - * / ^ y paréntesis. Escribe 'salir' para terminar.");

            while (true)
            {
                Console.Write("\nExpresión: ");
                string expresion = Console.ReadLine();

                if (expresion == null || expresion.Trim().ToLower() == "salir")
                    break;

                if (string.IsNullOrWhiteSpace(expresion))
                    continue;

                try
                {
                    List<string> tokens = Tokenizar(expresion);
                    List<string> postfija = InfijaAPostfija(tokens);
                    double resultado = Evaluar(postfija);

                    Console.WriteLine($"Postfija:  {string.Join(" ", postfija)}");
                    Console.WriteLine($"Resultado: {resultado.ToString(CultureInfo.InvariantCulture)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}