
using System;
using System.IO;
using ElementLib.Services;

var filename = Path.Combine("Data", "TableOfElements.json");

var elements = 
    ElementLib.ElementLoader.Load(filename);

Console.WriteLine();

var parser = new ElementParser();
parser.Init(elements);

var m = parser.Parse("H2O");

Console.WriteLine();
