using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanguageU
{

    /// <summary>
    /// Funcion que devuelve un diccionario (Ej: <spa, diccionario<ID_Play, Jugar>>)
    /// </summary>
    /// <param name="source"> Si vino desde la descarga de internet o desde el disco </param>
    /// <param name="sheet"> El excel </param>
    /// <returns></returns>
    public static Dictionary<Language, Dictionary<string, string>> LoadCodexFromString(string source, string sheet)
    {
        //Creamos una variable del mismo tipo a devolver
        var codex = new Dictionary<Language, Dictionary<string, string>>();

        //Un contador de lineas para saber en donde fallo
        int lineNum = 0;

        //Cortamos en renglones cada vez que hay un salto de linea
        string[] rows = sheet.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        //Bool para saber si estamos en la primer fila asi la ignoramos
        bool first = true;

        //Diccionario para saber la posicion que esta determinada columna
        //Ej: <Idioma, 0> - <ID, 1> - <Texto, 2>
        var columnToIndex = new Dictionary<string, int>();

        foreach (var row in rows)
        {
            //Sumamos para saber que estamos en la primer fila
            lineNum++;

            //Separamos por columna al encontrar un ';'
            string[] cells = row.Split(';');

            //Si es la primer fila
            if (first)
            {
                //Ya sabemos que la siguiente fila no es la primera
                first = false;

                for (int i = 0; i < cells.Length; i++)
                {
                    //Guardamos el indice en donde se encuentra esa columna que tiene un nombre (en nuestro caso es ID, Idioma, Texto)
                    columnToIndex[cells[i]] = i;
                }
                continue;
            }

            //Si detectamos que hay una diferencia en las columnas siguientes a la primera
            //Avisamos en console para que sepamos que algo fallo
            if (cells.Length != columnToIndex.Count)
            {
                Debug.Log(string.Format("Parsing CSV file {2} at line {0}, column {1}, should be {3}", lineNum, cells.Length, source, columnToIndex.Count));
                continue;
            }

            //Le decimos que tome el valor de la columna que se encuentra en Idioma
            string langName = cells[columnToIndex["Idioma"]];

            Language lang;

            //Intentamos castear el idioma de la celda al idioma del Enum (Language)
            //Si hay un problema lo sabemos en el catch
            try
            {
                lang = (Language)Enum.Parse(typeof(Language), langName);
            }
            catch(Exception e)
            {
                Debug.Log(string.Format("Parsing CSV file {2}, at line {0}, invalid language {1}", lineNum, langName, source));
                Debug.Log(e.ToString());
                continue;
            }

            string idName = cells[columnToIndex["ID"]]; //Le decimos que tome el ID correspondiente
            string text = cells[columnToIndex["Texto"]]; //Le decimos que tome el Texto correspondiente

            //Si el diccionario que vamos a devolver no contiene ese idioma
            if (!codex.ContainsKey(lang))
            {
                //Lo creamos
                codex[lang] = new Dictionary<string, string>();
            }

            //Le decimos al diccionario que en "X" lenguaje, utilizando "Y" ID, vamos a guardar "Z" texto
            codex[lang][idName] = text;

        }

        return codex;
    }
}
