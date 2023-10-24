using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyRoomPlugin
{
	class RoomComparerByNum : IComparer<Room>
	{
		public int Compare(Room x, Room y)
		{
			if (Convert.ToDouble(x.Number) > Convert.ToDouble(y.Number))
				return 1;
			else if (Convert.ToDouble(x.Number) < Convert.ToDouble(y.Number))
				return -1;
			else
				return 0;
		}
	}
}
