using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyRoomPlugin.Model;

namespace StudyRoomPlugin
{
	[TransactionAttribute(TransactionMode.Manual)]
	[RegenerationAttribute(RegenerationOption.Manual)]
	public class StartClassPlugin : IExternalCommand
	{
		private List<Level> allRoomLevel;

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
       