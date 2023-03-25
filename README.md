# GrootBox
Minimal spreadsheet like program in (when totally stripped down) less than 100 lines


**Sample code**

`  
using System;  
using System.IO;  
using System.Collections.Generic;  

namespace groot  
{  
    public class Program  
    {  
        public static void Main()  
        {  
            GrootDocument gd = new GrootDocument();  
            GrootSheet gs = new GrootSheet("sheet1");  
            gd.GrootSheets.Add(gs.Key, gs);  
                                                                    //   A    B  
            GrootPosition p1 = new GrootPosition(0,0);              //1  [p1][ ]  
            GrootPosition p2 = new GrootPosition(1,0);              //2  [p2][ ]  
            GrootPosition p3 = new GrootPosition(2,0);              //3  [p3][ ]  
            GrootPosition p4 = new GrootPosition(3,0);              //4  [p4][ ]  
  
            //Ranges .. not even needed. but left in   
            /*  
            GrootRange r1 = new GrootRange(p1,p1);  
            GrootRange r2 = new GrootRange(p2,p2);  
            GrootRange r3 = new GrootRange(p3,p3);  
            GrootRange r4 = new GrootRange(p4,p4);  
            */  
 
            GrootObject<int> a1 = new GrootObject<int>();  
            GrootObject<int> a2 = new GrootObject<int>();  
            GrootObject<int> a3 = new GrootObject<int>();  
            GrootObject<int> a4 = new GrootObject<int>();  

            //Setting values AND functions (excel formula's) on the objects  
            a1.Value = () => 5;     
            a2.Value = () => 7; //value
            a3.Value = () => a1.Value() + a2.Value();  //formula
            Console.WriteLine(a3.Value());  
            a1.Value = () => 10;  
            Console.WriteLine(a3.Value());            
            a4.Value = () => a3.Value() + a3.Value();  //formula
            Console.WriteLine(a4.Value());   
            a2.Value = () => 1;  
            Console.WriteLine(a4.Value());  

            //Setting & reading values as we typically would do it with an 'excel' like API  
            gd.GrootSheets[gs.Key].GrootCells[p1] = a1;  
            gd.GrootSheets[gs.Key].GrootCells[p2] = a2;  
            gd.GrootSheets[gs.Key].GrootCells[p3] = a3;  
            gd.GrootSheets[gs.Key].GrootCells[p4] = a4;  

            //Should be 22
            Console.WriteLine(gd.GrootSheets[gs.Key].GrootCells[p4].Value());  
        }
    }
}
`
