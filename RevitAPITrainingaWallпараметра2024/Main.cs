using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;

namespace RevitAPITrainingaWallпараметра2024
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration (RegenerationOption.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document ;//нам нужнен документ и указываем тип данных
           

            var walls = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements ();
            Transaction transaction = new Transaction(doc);
            transaction.Start("CopyParameters");//без имени транзакции ошибка

            foreach (Element wall in walls) 
            {
                string val = wall.LookupParameter ("Комментарии").AsString ();//получаем значение строки
                wall.LookupParameter("Марка").Set(val);//назначали
                
            }
            transaction.Commit();

            return Result.Succeeded;
        }
    }
}
