using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace baithayhoan
{

    [Transaction(TransactionMode.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            TaskDialog.Show("Revit", "Số lượng đối tượng được chọn: " + selectedIds.Count.ToString());

            ICollection<ElementId> selectedWallIds = new List<ElementId>();
            ICollection<ElementId> selectedFloor = new List<ElementId>();
            ICollection<ElementId> selectedColumn = new List<ElementId>();

            FilteredElementCollector collector = new FilteredElementCollector(uidoc.Document);
            ICollection<Element> elementColumns = collector.OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_StructuralColumns).ToElements();

            foreach (ElementId id in selectedIds)
            {
                Element elements1 = uidoc.Document.GetElement(id);
                if (elements1 is Wall)
                {
                    selectedWallIds.Add(id);
                }
                if (elements1 is Floor)
                {
                    selectedFloor.Add(id);
                }
            }
            foreach (Element column in elementColumns)
            {
                if (selectedIds.Contains(column.Id))
                {
                    selectedColumn.Add(column.Id);
                }
            }
            uidoc.Selection.SetElementIds(selectedWallIds);

            if (0 != selectedWallIds.Count)
            {
                TaskDialog.Show("Revit", selectedWallIds.Count.ToString() + " Đối tượng Tường đã được chọn!");
            }
            else
            {
                TaskDialog.Show("Revit", "Không có đối tượng tường nào được chọn!");
            }

            uidoc.Selection.SetElementIds(selectedColumn);
            if (0 != elementColumns.Count)
            {
                TaskDialog.Show("Revit", elementColumns.Count.ToString() + " Đối tượng Cột đã được chọn!");
            }
            else
            {
                TaskDialog.Show("Revit", "Không có đối tượng cột nào được chọn!");
            }

            uidoc.Selection.SetElementIds(selectedFloor);
            if (0 != selectedFloor.Count)
            {
                TaskDialog.Show("Revit", selectedFloor.Count.ToString() + " Đối tượng sàn đã được chọn!");
            }
            else
            {
                TaskDialog.Show("Revit", "Không có đối tượng sàn nào được chọn!");
            }

            return Result.Succeeded;
        }
    }
    public static class Program
    {
        [System.STAThread]
        public static void Main()
        {
        }
    }
}