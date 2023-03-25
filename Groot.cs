/*
Author: Maurice Marinus
Company: Symbolic Computing
Position: Symbolic Architect
Project: GrootBox - Minimal spreadsheet like program in (when totally stripped down) - 100 lines
Description: To show how to create a minimal spreadsheet like architecture
Notes: Whats the difference between this and excel type packages (e.g EPPLUS)? Excel type packages focus on interoparability with Excel. 
       This project does NOT do Excel. This project does a generic spreadsheet core architecture minus the UI.
       Hence it can double up for genuine analytical work on dataset . ala Machine learning (i.e operations on datasets - for programmers - e.g. import csv files and do some machine learning style functions on the dataset)
       Users can add their own 'built' in stuff like sorting, filtering etc. 
WARNING: GrootObject. Default values for null are created only for types with parameterless constructors (e.g. int, byte, char) . Code can be modified to include types like string etc
Null values not catered for. (However .. it would have been super cool if my way to handle null was catered for as it would be perfect fit: http://0x0.co.za/nulldrop.html )

Date Started: 23 March 2023
*/

using System;
using System.Collections.Generic;

namespace groot
{
    //The collection of sheets
    public class GrootDocument
    {
        private Dictionary<Guid, GrootSheet> grootSheets;
        public GrootDocument()
        {
            this.grootSheets = new Dictionary<Guid, GrootSheet>();
        }
              
        public Dictionary<Guid, GrootSheet> GrootSheets { get => grootSheets; set => grootSheets = value; }
    }

    /// <summary>
    /// The collection of cells
    /// </summary>
    public class GrootSheet
    {
        private Guid key;
        private string name;
        private Dictionary<GrootPosition, dynamic> grootCells;

        public GrootSheet(string name)
        {
            this.key = Guid.NewGuid();
            this.name = name;
            this.GrootCells = new Dictionary<GrootPosition, dynamic>();
        }

        public Guid Key {get => this.key;}
        public string Name { get => name; set => name = value; }
        public Dictionary<GrootPosition, dynamic> GrootCells { get => grootCells; set => grootCells = value; }
    }

    /// <summary>
    /// The cell position e.g. Cell[x,y] = Cell[1,1] = Cell[second row, second column]
    /// </summary>
    public struct GrootPosition
    {
        private uint x;
        private uint y;

        public GrootPosition()
        {
            x = 0; 
            y = 0;
        }

        public GrootPosition(uint x, uint y)
        {
            this.x = x; 
            this.y = y;
        }

        public uint X { get => x; set => x = value; }
        public uint Y { get => y; set => y = value; }
    }

    public class GrootObject<T>
    {
        private Func<T>? value;

        private static T ResolveType<T>()
        {
            return Activator.CreateInstance<T>();
        }

        private static object ResolveType(Type type)
        {
            return Activator.CreateInstance(type);
        }
        public Func<T>? Value 
        { 
            get
            {
                if (this.value == null)
                { 
                    //WARNING. This only works on types that has a parameterless constructor
                    var arg = typeof(T);
                    return new Func<T>(() => (T)ResolveType(arg));
                }
                return this.value;
            } 
            set
            {
                this.value = value;
            }
        }
    }

    /// <summary>
    /// Not really needed. A range of cells. It can be single vertical column, single horizontal row or combination of more than 1 row or 1 column 
    /// </summary>
    public class GrootRange
    {
        private GrootPosition start;
        private GrootPosition end;

        public GrootRange()
        {
            start = new GrootPosition();
            end = new GrootPosition();
        }

        public GrootRange(GrootPosition start, GrootPosition end)
        {
            this.start = start;
            this.end = end;
        }

        public GrootPosition Start { get => start; set => start = value; }
        public GrootPosition End { get => end; set => end = value; }
    }

    /// <summary>
    /// Not really needed for this. Its just to show how styles are be created especially in existing spreadsheet applications. 
    /// Styles should be created separately and applied to a range and not to a value. Its more for user interface purposes
    /// </summary>
    public class GrootStyle
    {      
        public bool Bold {get;set;}
        public bool Italic {get;set;}
        public uint Size {get;set;}
        public ConsoleColor Color {get;set;}
        public GrootRange Range {get;set;}
        public bool CellsMerged {get;set;}
        public VerticalAlignment VerticalAlignment {get;set;}
        public HorizontalAlignment HorizontalAlignment {get;set;}
    }

    /// <summary>
    /// More fluff to support the ui stuff
    /// </summary>
    public enum VerticalAlignment
    {
        Center=0,
        Middle=1,
        Bottom=2
    }
     /// <summary>
    /// More fluff to support the ui stuff
    /// </summary>
    public enum HorizontalAlignment
    {
        Center=0,
        Left=1,
        Right=2
    }
}