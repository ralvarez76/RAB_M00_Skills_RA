#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace RAB_M00_Skills_RA
{
    [Transaction(TransactionMode.Manual)]
    public class Module01Challenge : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            // Declare a number variable and set it to 250
            int numberInt1 = 10;

            // Declare a starting elevation variable and set it to 0
            double elevationStarting1 = 0;

            // Declare a floor height variable and set it to 15
            double floorHeight1 = 15;

            // level creation transaction
            Transaction createLevelsAndViews1 = new Transaction(doc);
            createLevelsAndViews1.Start("Creating Levels and Views");

            // Loop through the number 1 to the number variable
            for (int i = 0; i <= numberInt1; i++)
            {
                // Create a level for each number
                Level newLevels1 = Level.Create(doc, (i * floorHeight1) + elevationStarting1);
                newLevels1.Name = "LEVEL" + " " + i.ToString();

                List<Level> levelsCollector1 = new List<Level> {newLevels1};

                Level newFilteredLevels1 = null;
                foreach (Level currentLevels1 in levelsCollector1)
                {
                    if (currentLevels1.Elevation % 3 == 0)
                    {
                        newFilteredLevels1 = currentLevels1;
                    }
                        FilteredElementCollector floorPlanCollector1 = new FilteredElementCollector(doc);
                        floorPlanCollector1.OfClass(typeof(ViewFamilyType));

                        ViewFamilyType floorPlanViewFamilyTypes1 = null;
                        foreach (ViewFamilyType currentViewFamilyTypes1 in floorPlanCollector1)
                        {
                            if (currentViewFamilyTypes1.ViewFamily == ViewFamily.FloorPlan)
                            {
                                floorPlanViewFamilyTypes1 = currentViewFamilyTypes1;
                            }
                        ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanViewFamilyTypes1.Id, newFilteredLevels1.Id);
                        String strFloorPlan = newFloorPlan.Name;
                        newFloorPlan.Name = "FIZZ_" + strFloorPlan.Remove(0, 6);
                        }

                }

                Level newFilteredLevels2 = null;
                foreach (Level currentLevels2 in levelsCollector1)
                {
                    if (currentLevels2.Elevation % 5 == 0)
                    {
                        newFilteredLevels2 = currentLevels2;
                    }
                    FilteredElementCollector ceilingPlanCollector1 = new FilteredElementCollector(doc);
                    ceilingPlanCollector1.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType ceilingPlanViewFamilyTypes1 = null;
                    foreach (ViewFamilyType currentViewFamilyTypes2 in ceilingPlanCollector1)
                    {
                        if (currentViewFamilyTypes2.ViewFamily == ViewFamily.CeilingPlan)
                        {
                            ceilingPlanViewFamilyTypes1 = currentViewFamilyTypes2;
                        }
                        ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanViewFamilyTypes1.Id, newFilteredLevels2.Id);
                        String strCeilingPlan = newCeilingPlan.Name;
                        newCeilingPlan.Name = "BUZZ_" + strCeilingPlan.Remove(0, 6);
                    }
                }
            }

            createLevelsAndViews1.Commit();
            createLevelsAndViews1.Dispose();

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnCommand1";
            string buttonTitle = "Button 1";

            ButtonDataClass myButtonData1 = new ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Blue_32,
                Properties.Resources.Blue_16,
                "This is a tooltip for Button 1");

            return myButtonData1.Data;
        }
    }
}
