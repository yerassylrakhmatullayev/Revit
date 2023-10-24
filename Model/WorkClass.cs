using Autodesk.Revit.Creation;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Document = Autodesk.Revit.DB.Document;


namespace StudyRoomPlugin.Model
{
	class WorkClass
	{
		public static Tuple<Room[], List<Level>> GetAllRooms(Document doc)
		{
			FilteredElementCollector newRoomFilter = new FilteredElementCollector(doc);
			ICollection<Element> allRooms = newRoomFilter.OfCategory(BuiltInCategory.OST_Rooms).WhereElementIsNotElementType().ToElements();

			List<Level> allRoomLevel = new List<Level>();
			List<string> levelNames = new List<string>();

			List<Room> allRoomsList = new List<Room>();


			foreach (Element roomEl in allRooms)
			{
				Room room = roomEl as Room;
				allRoomsList.Add(room);
				Level level = room.Level;


				if (!levelNames.Contains(level.Name))
				{
					levelNames.Contains(level.Name);
					allRoomLevel.Add(level);
				}



			}
			Room[] allRoomsArray = allRoomsList.ToArray();

			Array.Sort(allRoomsArray, new RoomComparerByNum());
			return Tuple.Create(allRoomsArray, allRoomLevel);
		}
	}
}
