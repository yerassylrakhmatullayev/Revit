using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using StudyRoomPlugin.Model;
using System;
using System.Collections.Generic;

namespace StudyRoomPlugin
{
	[Regeneration(RegenerationOption.Manual), Transaction(TransactionMode.Manual)]
	public class StartClassPluginBase1
	{

		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements, WorkClass workClass)
		{
			Document doc = commandData.Application.ActiveUIDocument.Document;

			Tuple<Room[], List<Level>> result = WorkClass.GetAllRooms(doc);
			Room[] allRoomsArray = WorkClass.GetAllRooms(doc).Item1;
			List<Level> allRoomLevel = result.Item2;

			UserWindRoom userWind = new UserWindRoom(allRoomsArray, allRoomLevel, doc);
			userWind.ShowDialog();
			return Result.Succeeded;
		}
	}
}